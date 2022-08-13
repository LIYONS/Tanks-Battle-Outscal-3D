using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public LayerMask tankLayer;
    public ParticleSystem shellExplosionParticle;

    private BulletScriptableObject bulletObject;

    private void Start()
    {
        Destroy(gameObject, bulletObject.maxLifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bulletObject.explosionRadius, tankLayer);
        for(int i=0;i<colliders.Length;i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            if(!rb)
            {
                continue;
            }
            rb.AddExplosionForce(bulletObject.explosionForce, transform.position, bulletObject.explosionRadius);


            if(rb.gameObject.tag=="Player")
            {
                PlayerTankView playerTankView = colliders[i].gameObject.GetComponent<PlayerTankView>();
                playerTankView.TakeDamage(CalculateDamage(rb.position));
            }
            EnemyTankView enemyTankView = colliders[i].GetComponent<EnemyTankView>();
            if(enemyTankView)
            {
                float damage = CalculateDamage(rb.position);
                enemyTankView.TakeDamage(damage);
            }
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

        float relativeDamage = (bulletObject.explosionRadius - explosionMagnitude) / bulletObject.explosionRadius;

        float damage = relativeDamage * bulletObject.maxDamage;

        damage = Mathf.Max(0, damage);
        return damage;
    }

    public void SetBulletObject(BulletScriptableObject _object)
    {
        bulletObject = _object;
    }
}
