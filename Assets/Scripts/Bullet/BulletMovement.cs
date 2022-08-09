using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody shell;
    [SerializeField] private Slider aimSlider;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private BulletScriptableObject bulletObject;
    private float chargingSpeed;
    private float fireTimer;
    private float currentLaunchForce;
    

    private void OnEnable()
    {
        currentLaunchForce =bulletObject.minLaunchForce;
        aimSlider.value = currentLaunchForce;
    }
    private void Start()
    {
        fireTimer = 0;
        chargingSpeed = (bulletObject.maxLaunchForce - bulletObject.minLaunchForce) / bulletObject.maxChargeTime;
    }


    private void Update()
    {
        aimSlider.value = currentLaunchForce;
        FireCheck();
    }
    void FireCheck()
    {
        if (fireTimer < Time.time)
        {
            if (currentLaunchForce > bulletObject.maxLaunchForce)
            {
                currentLaunchForce = bulletObject.maxLaunchForce;
                Fire();
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                currentLaunchForce += chargingSpeed * Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Fire();
            }
        }
    }
 
    void Fire()
    {
        fireTimer = Time.time + bulletObject.nextFireDelay;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.GetComponent<BulletExplosion>().SetBulletObject(bulletObject);
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;
        currentLaunchForce = bulletObject.minLaunchForce;
    }
}
