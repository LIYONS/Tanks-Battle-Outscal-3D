using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public LayerMask tankLayer;
    public float maxDamage;
    public float explosionRadius;
    public float explosionForce;
    public float maxLifeTime;

    public ParticleSystem shellExplosionParticle;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankLayer);
        for(int i=0;i<colliders.Length;i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            if(!rb)
            {
                continue;
            }
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            TankHealth tankHealth = colliders[i].GetComponent<TankHealth>();
            if(!tankHealth)
            {
                continue;
            }
            float damage = CalculateDamage(rb.position);
            tankHealth.TakeDamage(damage);
        }
        shellExplosionParticle.transform.parent = null;

        shellExplosionParticle.Play();

        Destroy(shellExplosionParticle.gameObject, shellExplosionParticle.main.duration);
        Destroy(gameObject);

    }

    float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionMagnitude = explosionToTarget.magnitude;

        float relativeDamage = (explosionRadius - explosionMagnitude) / explosionRadius;

        float damage = relativeDamage * maxDamage;

        damage = Mathf.Max(0, damage);
        return damage;
    }


}
