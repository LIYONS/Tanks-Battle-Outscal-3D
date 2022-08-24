using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthSliderFillImage;
    [SerializeField] private Color healthStartColor = Color.green;
    [SerializeField] private Color healthDamageColor = Color.red;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject[] tankBody;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private ShellObject shellObject;

    private ParticleSystem explosionEffect;
    private PlayerController playerController;
    private TankScriptableObject playerObject;
    private float movementInput;
    private float turnInput;

    private ShellServicePool bulletServicePool;
    private float chargingSpeed;
    private float fireTimer=0f;
    private float currentLaunchForce;
    private bool fired=false;
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
        currentLaunchForce = shellObject.minLaunchForce;
        bulletServicePool = GetComponent<ShellServicePool>();
        aimSlider.value = currentLaunchForce;
        chargingSpeed = (shellObject.maxLaunchForce - shellObject.minLaunchForce) / shellObject.maxChargeTime;
        PlayGameSound(SoundType.TankIdle);
        CameraControl.Instance.AddCameraTargetPosition(transform);
    }
    private void Update()
    {
        aimSlider.value = shellObject.minLaunchForce;
        FireCheck();
    }
    void FireCheck()
    {
        if (fireTimer < Time.time)
        {
            if (currentLaunchForce >= shellObject.maxLaunchForce && !fired)
            {
                currentLaunchForce = shellObject.maxLaunchForce;
                Fire();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                fired = false;
                currentLaunchForce = shellObject.minLaunchForce;
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                currentLaunchForce += chargingSpeed * Time.deltaTime;
                aimSlider.value = currentLaunchForce;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && !fired)
            {
                Fire();
            }
        }
    }
    

    private void Fire()
    {
        Vector3 velocity = currentLaunchForce * fireTransform.forward;
        playerController.Fire(velocity);
        fired = true;
        fireTimer = Time.time + shellObject.nextFireDelay;
        currentLaunchForce =shellObject.minLaunchForce;
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

    private void PlayGameSound(SoundType type)
    {
        var instance = AudioManager.Instance;
        if (instance)
        {
            instance.PlayGameSound(type);
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
        for (int i = 0; i < tankBody.Length; i++)
        {
            tankBody[i].GetComponent<Renderer>().material.color = playerObject.tankColor;
        }
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
    public Rigidbody GetRigidBody { get { return GetComponent<Rigidbody>(); } }
  

    public ShellObject GetShellObject { get { return shellObject; } }

    public Transform GetFireTransform { get { return fireTransform; } }

    public TankScriptableObject GetTankObject()
    {
        return playerObject;
    }
}
