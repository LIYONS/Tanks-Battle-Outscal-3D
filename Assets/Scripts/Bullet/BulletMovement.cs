using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMovement : MonoBehaviour
{
    public Rigidbody shell;
    public Slider aimSlider;
    public Transform fireTransform;
    public float maxChargeTime=.75f;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float nextFireDelay;

    float chargingSpeed;
    float fireTimer;
    float currentLaunchForce;
    

    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = currentLaunchForce;
    }
    private void Start()
    {
        fireTimer = 0;
        chargingSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }


    private void Update()
    {
        aimSlider.value = currentLaunchForce;
        if(fireTimer<Time.time)
        {
            if (currentLaunchForce > maxLaunchForce)
            {
                currentLaunchForce = maxLaunchForce;
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
        fireTimer = Time.time + nextFireDelay;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;
        currentLaunchForce = minLaunchForce;
    }
}
