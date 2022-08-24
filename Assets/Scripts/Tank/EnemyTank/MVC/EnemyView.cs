using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyView : MonoBehaviour
{          
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Color healthStartColor = Color.green;
    [SerializeField] private Color healthDamageColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TankState defaultState;
    [SerializeField] GameObject[] tankBody;

    private ParticleSystem explosionEffect;
    private EnemyController enemyController;
    private TankScriptableObject enemyObject;
    private TankState currentState;
    private bool isAssigned = false;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionEffect.gameObject.SetActive(false);
    }

    private void Start()
    {
        enemyController.SetTankView(this);
        enemyObject = enemyController.GetEnemyModel().GetTankObject();
        SetColour();
        SetHealthUI(enemyObject.maxHealth);
        currentState = defaultState;
        currentState.OnEnterState();
    }
    public void TakeDamage(float amount)
    {
        enemyController.TakeDamage(amount);
    }
    void SetColour()
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

    public void ChangeState(TankState newState)
    {
        currentState.OnExitState();
        newState.OnEnterState();
        currentState = newState;
        if((currentState==GetComponent<TankChaseState>() || currentState==GetComponent<TankAttackState>()) && !isAssigned)
        {
            isAssigned = true;
            CameraControl.Instance.AddCameraTargetPosition(this.transform);
        }
        if((currentState != GetComponent<TankChaseState>() && currentState != GetComponent<TankAttackState>()) && isAssigned)
        {
            isAssigned = false;
            CameraControl.Instance.RemoveCameraTargetPosition(transform);
        }
    }
    void SetUiInactive()
    {
        healthSlider.gameObject.SetActive(false);
        explosionEffect.gameObject.SetActive(false);
    }
    public void OnDeath()
    {
        EventHandler.Instance.InvokeEnemyDeath();
        if(isAssigned)
        {
            CameraControl.Instance.RemoveCameraTargetPosition(transform);
        }
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.gameObject.transform.position = transform.position;
        explosionEffect.Play();
        PlayDeathSound();
        Destroy(gameObject);
    }

    private void PlayDeathSound()
    {
        var instance = AudioManager.Instance;
        if (instance)
        {
            instance.PlaySfx(SoundType.TankExplode);
        }
    }
    public void SetController( EnemyController _controller)
    {
        enemyController = _controller;
    }
}
