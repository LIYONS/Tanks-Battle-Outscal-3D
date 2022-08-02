using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Image fillImage;
    public Color startColor=Color.green;
    public Color damageColor=Color.red;
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject explosionPrefab;

    private ParticleSystem explosionEffect;
    private AudioSource explosionAudio;
    private bool isDead;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionEffect.gameObject.GetComponent<AudioSource>();
        explosionEffect.gameObject.SetActive(false);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        SetHealthUI();

    }

    private void SetHealthUI()
    {
        healthSlider.value = currentHealth;

        fillImage.color = Color.Lerp(damageColor,startColor, currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        SetHealthUI();
        explosionEffect.gameObject.transform.position = transform.position;
        explosionEffect.Play();
        explosionAudio.Play();
        if(currentHealth<=0 && !isDead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
