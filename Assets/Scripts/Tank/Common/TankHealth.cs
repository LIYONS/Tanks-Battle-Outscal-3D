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
    public GameObject explosionPrefab;

    private float currentHealth;
    private ParticleSystem explosionEffect;
    private bool isDead;
    public float sliderTimer;

    private void Awake()
    {
        explosionEffect = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
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
        Invoke("SetInactive", 2f);
        healthSlider.gameObject.SetActive(true);
        if(currentHealth<=0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }

    public void OnDeath()
    {
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.gameObject.transform.position = transform.position;
        explosionEffect.Play();
        if (gameObject.tag == "Player")
        {
            StartCoroutine(GameOver());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator GameOver()
    {
        yield return StartCoroutine(DestroyAllEnemies());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(DestroyLevel());
        gameObject.SetActive(false);
    }
    IEnumerator DestroyLevel()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            yield return new WaitForSeconds(.2f);
            Destroy(gameObjects[i]);
        }
    }
    IEnumerator DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0;i<enemies.Length;i++)
        {
            TankHealth healthScript = enemies[i].GetComponent<TankHealth>();
            healthScript.TakeDamage(healthScript.maxHealth);
        }
        yield return null;
    }
    void SetInactive()
    {
        healthSlider.gameObject.SetActive(false);
        explosionEffect.gameObject.SetActive(false);
    }
}
