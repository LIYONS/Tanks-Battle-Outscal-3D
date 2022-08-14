using UnityEngine;
using UnityEngine.UI;

public class PlayerTankView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Color healthStartColor = Color.green;
    [SerializeField] private Color healthDamageColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;

    private ParticleSystem explosionEffect;
    private PlayerTankController tankController;
    private TankScriptableObject tankObject;
    private float movementInput;
    private float turnInput;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionEffect.gameObject.SetActive(false);
    }
    private void Start()
    {
        SetTankObject();
        SetColour();
        SetHealthUI(tankObject.maxHealth);
    }
    void FixedUpdate()
    {
        Move();
        Turn();
    }
    private void Turn()
    {
        turnInput = Input.GetAxis("Horizontal");
        if(turnInput!=0)
        {
            tankController.Rotate(turnInput);
        }
    }
    public void TakeDamage(float amount)
    {
        tankController.TakeDamage(amount);
    }
    private void Move()
    {
        movementInput = Input.GetAxis("Vertical");
        if(movementInput!=0)
        {
            tankController.Movement(movementInput);
        }
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
        healthSliderFillImage.color = Color.Lerp(healthDamageColor, healthStartColor, _health /tankObject.maxHealth);
        Invoke("SetUiInactive", tankObject.healthSliderTimer);
    }

    void SetUiInactive()
    {
        healthSlider.gameObject.SetActive(false);
        explosionEffect.gameObject.SetActive(false);
    }
    public void OnDeath()
    {
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.gameObject.transform.position = transform.position;
        explosionEffect.Play();
    }
    public void SetComponents(PlayerTankController _controller)
    {
        tankController = _controller;
    }
    void SetTankObject()
    {
        tankObject = tankController.GetTankModel().GetTankObject();
    }
    public Rigidbody GetRigidBody()
    {
        return GetComponent<Rigidbody>();
    }

    public TankScriptableObject GetTankObject()
    {
        return tankObject;
    }
}
