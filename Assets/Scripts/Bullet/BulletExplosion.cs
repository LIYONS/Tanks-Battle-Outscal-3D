using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField] private LayerMask tankLayer;
    [SerializeField] private ParticleSystem shellExplosionParticle;

    private BulletServicePool bulletServicePool;
    private BulletScriptableObject bulletObject;
    private GameObject parent;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("TankLayer"))
        {
            ExplosionEffect();
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, bulletObject.explosionRadius, tankLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            if (!rb || rb.gameObject == parent || Vector3.Distance(transform.position, rb.transform.position) > 2f)
            {
                continue;
            }
            else
            {
                Explode(rb);
            }
        }
        
    }
    private void Explode(Rigidbody rb)
    {
        if (rb.gameObject.tag == "Player")
        {
            PlayerView playerTankView = rb.gameObject.GetComponent<PlayerView>();
            playerTankView.TakeDamage(CalculateDamage(rb.position));
        }
        EnemyTankView enemyTankView =rb.GetComponent<EnemyTankView>();
        if (enemyTankView)
        {
            float damage = CalculateDamage(rb.position);
            enemyTankView.TakeDamage(damage);
        }
         ExplosionEffect();
    }

    private void ExplosionEffect()
    {
        ParticleSystem particleSystem = Instantiate(shellExplosionParticle, this.transform).GetComponent<ParticleSystem>();
        particleSystem.transform.parent = null;
        particleSystem.Play();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);
        ReturnToPool();
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

    private void ReturnToPool()
    {
        bulletServicePool.ReturnItem(this);
        this.gameObject.SetActive(false);
    }
    public void SetComponents(BulletScriptableObject _object,GameObject _parent,BulletServicePool _bulletServicePool)
    {
        bulletObject = _object;
        parent = _parent;
        bulletServicePool = _bulletServicePool;
    }
}
