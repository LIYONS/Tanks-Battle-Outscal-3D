using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellView : MonoBehaviour
{

    [SerializeField] private LayerMask tankLayer;
    [SerializeField] private ParticleSystem shellExplosionParticle;

    private GameObject parent;
    private ShellController shellController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("TankLayer"))
        {
            ExplosionEffect();
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, shellController.GetShellModel.GetShellObject.explosionRadius, tankLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            if (!rb || rb.gameObject == parent || Vector3.Distance(transform.position, rb.transform.position) > 2f)
            {
                continue;
            }
            else
            {
                shellController.Explode(rb);
                ExplosionEffect();
            }
        }
    }
    public void SetParent(GameObject _parent)
    {
        parent = _parent;
    }

    public void ExplosionEffect()
    {
        ParticleSystem particleSystem = Instantiate(shellExplosionParticle, this.transform).GetComponent<ParticleSystem>();
        particleSystem.transform.parent = null;
        particleSystem.Play();
        Destroy(particleSystem.gameObject, particleSystem.main.duration);
        ShellService.Instance.ReturnToPool(shellController);
        gameObject.SetActive(false);
    }
    public void SetShellController(ShellController _controller)
    {
        shellController = _controller;
    }
}
