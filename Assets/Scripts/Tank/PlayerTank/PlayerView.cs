using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Color healthStartColor = Color.green;
    [SerializeField] private Color healthDamageColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;

    private ParticleSystem explosionEffect;
    private PlayerController playerController;
    private TankScriptableObject playerObject;
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
        SetHealthUI(playerObject.maxHealth);
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
            playerController.Rotate(turnInput);
        }
    }
    public void TakeDamage(float amount)
    {
        playerController.TakeDamage(amount);
    }
    private void Move()
    {
        movementInput = Input.GetAxis("Vertical");
        if(movementInput!=0)
        {
            playerController.Movement(movementInput);
        }
    }
    void SetColour()
    {
        Transform tankTurret = gameObject.transform.Find("TankRenderers/TankTurret");
        Transform tankChassis = gameObject.transform.Find("TankRenderers/TankChassis");
        tankTurret.gameObject.GetComponent<Renderer>().material.color = playerObject.tankColor;
        tankChassis.gameObject.GetComponent<Renderer>().material.color = playerObject.tankColor;
    }
    public void SetHealthUI(float _health)
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = _health;
        healthSliderFillImage.color = Color.Lerp(healthDamageColor, healthStartColor, _health /playerObject.maxHealth);
        Invoke(nameof(SetUiInactive), playerObject.healthSliderTimer);
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
        gameObject.SetActive(false);
    }
    public void SetController(PlayerController _controller)
    {
        playerController = _controller;
    }
    void SetTankObject()
    {
        playerObject = playerController.GetPlayerModel().GetTankObject();
    }
    public Rigidbody GetRigidBody()
    {
        return GetComponent<Rigidbody>();
    }

    public TankScriptableObject GetTankObject()
    {
        return playerObject;
    }

}
