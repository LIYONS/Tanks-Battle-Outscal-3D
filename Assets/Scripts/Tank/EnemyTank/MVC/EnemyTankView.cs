using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class EnemyTankView : MonoBehaviour
{          
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Color healthStartColor = Color.green;
    [SerializeField] private Color healthDamageColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TankState defaultState;

    private ParticleSystem explosionEffect;
    private Transform[] wayPoints;
    private EnemyTankController controller;
    private TankScriptableObject tankObject;
    private TankState currentState;

    //Event
    public static event Action OnEnemyDeath;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionEffect.gameObject.SetActive(false);
    }
    private void Start()
    {
        controller.SetTankView(this);
        SetColour();
        SetHealthUI(tankObject.maxHealth);
        currentState = defaultState;
        currentState.OnEnterState();
    }
    public void TakeDamage(float amount)
    {
        controller.TakeDamage(amount);
    }
    void SetColour()
    {
        Transform tankTurret = gameObject.transform.Find("TankRenderers/TankTurret");
        Transform tankChassis = gameObject.transform.Find("TankRenderers/TankChassis");
        tankTurret.gameObject.GetComponent<Renderer>().material.color = tankObject.tankColor;
        tankChassis.gameObject.GetComponent<Renderer>().material.color = tankObject.tankColor;
    }
    public void SetHealthUI(float _health)
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = _health;
        healthSliderFillImage.color = Color.Lerp(healthDamageColor, healthStartColor, _health / tankObject.maxHealth);
        Invoke("SetUiInactive", tankObject.healthSliderTimer);
    }

    public void ChangeState(TankState newState)
    {
        currentState.OnExitState();
        newState.OnEnterState();
        currentState = newState;
    }
    void SetUiInactive()
    {
        healthSlider.gameObject.SetActive(false);
        explosionEffect.gameObject.SetActive(false);
    }
    public void OnDeath()
    {
        OnEnemyDeath?.Invoke();
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.gameObject.transform.position = transform.position;
        explosionEffect.Play();
    }
    public void SetComponents( EnemyTankController _controller, TankScriptableObject _tank, Transform[] _points)
    {
        controller = _controller;
        tankObject = _tank;
        wayPoints = _points;
    }
    public Transform[] GetWayPoints() 
    {
        return wayPoints;  
    }
    public Rigidbody GetRigidbody { get { return GetComponent<Rigidbody>(); } }
}
