using UnityEngine;
using UnityEngine.UI;
using TankGame.GameManagers;
using TankGame.CameraService;
using System.Collections.Generic;

namespace TankGame.Tanks.EnemyServices
{
    public enum StateType
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    [System.Serializable]
    public struct State
    {
        public StateType stateType;
        public TankState tankState;
    }
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image healthSliderFillImage;
        [SerializeField] private Color healthStartColor = Color.green;
        [SerializeField] private Color healthDamageColor = Color.red;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private StateType defaultState;
        [SerializeField] private GameObject[] tankBody;
        [SerializeField] private List<State> states;

        private ParticleSystem explosionEffect;
        private EnemyController enemyController;
        private TankScriptableObject enemyObject;
        private TankState currentState;
        private StateType currentStateType;
        private bool isAssigned = false;


        private void Start()
        {
            enemyController.SetTankView(this);
            enemyObject = enemyController.GetEnemyModel().GetTankObject();
            SetColour();
            SetHealthUI(enemyObject.maxHealth);
            ChangeState(defaultState);
            explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
            explosionEffect.gameObject.SetActive(false);
        }
        public void TakeDamage(float amount)
        {
            enemyController.TakeDamage(amount);
        }
        private void SetColour()
        {
            for (int i = 0; i < tankBody.Length; i++)
            {
                tankBody[i].GetComponent<Renderer>().material.color = enemyObject.tankColor;
            }
        }
        public void SetHealthUI(float _health)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = _health;
            healthSliderFillImage.color = Color.Lerp(healthDamageColor, healthStartColor, _health / enemyObject.maxHealth);
            Invoke(nameof(SetUiInactive), enemyObject.healthSliderTimer);
        }

        public void ChangeState(StateType type)
        {
            State state = GetState(type);
            if(state.tankState!=null)
            {
                if(currentState!=null)
                {
                    currentState.OnExitState();
                }
                state.tankState.OnEnterState();
                currentState = state.tankState;
                currentStateType = state.stateType;
            }
            
            
            if ((currentStateType == StateType.Chase || currentStateType ==StateType.Attack) && !isAssigned)
            {
                isAssigned = true;
                CameraControl.Instance.AddCameraTargetPosition(this.transform);
            }
            if ((currentStateType != StateType.Chase && currentStateType != StateType.Attack) && isAssigned)
            {
                isAssigned = false;
                CameraControl.Instance.RemoveCameraTargetPosition(transform);
            }
        }

        private State GetState(StateType type)
        {
            return states.Find(i => i.stateType == type);
        }
        private void SetUiInactive()
        {
            healthSlider.gameObject.SetActive(false);
            explosionEffect.gameObject.SetActive(false);
        }
        public void OnDeath()
        {
            EventManager.Instance.InvokeEnemyDeath();
            if (isAssigned)
            {
                CameraControl.Instance.RemoveCameraTargetPosition(transform);
            }
            if(explosionEffect)
            {
                explosionEffect.gameObject.SetActive(true);
                explosionEffect.gameObject.transform.position = transform.position;
                explosionEffect.Play();
            }
            PlayDeathSound();
            Destroy(gameObject);
        }

        private void PlayDeathSound()
        {
            AudioManager audioManager = AudioManager.Instance;
            if (audioManager)
            {
                audioManager.PlaySound(SoundType.TankExplode);
            }
        }
        public void SetController(EnemyController _controller)
        {
            enemyController = _controller;
        }
    }
}
