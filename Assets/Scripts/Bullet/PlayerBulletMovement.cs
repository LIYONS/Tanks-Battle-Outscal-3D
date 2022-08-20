using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerBulletMovement : MonoBehaviour
{
    [SerializeField] private BulletExplosion bulletPrefab;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private BulletScriptableObject bulletObject;

    private BulletServicePool bulletServicePool;
    private float chargingSpeed;
    private float fireTimer;
    private float currentLaunchForce;
    bool fired;
    private int bulletCount;

    private void Start()
    {
        currentLaunchForce = bulletObject.minLaunchForce;
        bulletServicePool = GetComponent<BulletServicePool>();
        aimSlider.value = currentLaunchForce;
        fired = false;
        fireTimer = 0;
        chargingSpeed = (bulletObject.maxLaunchForce - bulletObject.minLaunchForce) / bulletObject.maxChargeTime;
    }


    private void Update()
    {
        aimSlider.value = bulletObject.minLaunchForce;
        FireCheck();
    }
    void FireCheck()
    {
        if (fireTimer < Time.time)
        {
            if (currentLaunchForce >= bulletObject.maxLaunchForce && !fired)
            {
                currentLaunchForce = bulletObject.maxLaunchForce;
                Fire();
            }
            else if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                fired = false;
                currentLaunchForce = bulletObject.minLaunchForce;
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
 
    void Fire()
    {
        fired = true;
        bulletCount++;
        CheckAchievement();
        fireTimer = Time.time + bulletObject.nextFireDelay;
        ConfigureBullet();
    }

    private void ConfigureBullet()
    {
        BulletExplosion bullletInstance = bulletServicePool.GetBullet(bulletPrefab,fireTransform);
        bullletInstance.GetComponent<Rigidbody>(). velocity = currentLaunchForce * fireTransform.forward;
        bullletInstance.GetComponent<BulletExplosion>().SetComponents(bulletObject, this.gameObject,bulletServicePool);
        currentLaunchForce = bulletObject.minLaunchForce;
    }
    private void CheckAchievement()
    {
        if(bulletCount==10 || bulletCount==25 || bulletCount==50)
        {
            EventHandler.Instance.InvokeBulletAchievement(bulletCount);
        }
    }
}
