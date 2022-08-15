using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public LayerMask tankLayer;
    public ParticleSystem shellExplosionParticle;

    private BulletScriptableObject bulletObject;
    private GameObject parent;
    private bool doesExplode;
    private void Start()
    {
        doesExplode = false;
        Destroy(gameObject, bulletObject.maxLifeTime);
    }
    private void OnTriggerStay(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, bulletObject.explosionRadius, tankLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            if (!rb || rb.gameObject == parent || Vector3.Distance(transform.position, rb.transform.position) > 2f)
            {
                continue;
            }
            else if (!doesExplode)
            {
                doesExplode = true;
                Explode(rb);
            }
        }
    }
    private void Explode(Rigidbody rb)
    {
        if (rb.gameObject.tag == "Player")
        {
            PlayerTankView playerTankView = rb.gameObject.GetComponent<PlayerTankView>();
            playerTankView.TakeDamage(CalculateDamage(rb.position));
        }
        EnemyTankView enemyTankView =rb.GetComponent<EnemyTankView>();
        if (enemyTankView)
        {
            float damage = CalculateDamage(rb.position);
            enemyTankView.TakeDamage(damage);
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

    public void SetComponents(BulletScriptableObject _object,GameObject _parent)
    {
        bulletObject = _object;
        parent = _parent;
    }
}
