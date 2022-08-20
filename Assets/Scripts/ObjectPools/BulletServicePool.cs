using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletServicePool : GenericPool<BulletExplosion>
{
    private BulletExplosion bulletPrefab;
    private Transform fireTransform;
    public BulletExplosion GetBullet(BulletExplosion _bulletPrefab,Transform _fireTransform)
    {
        bulletPrefab = _bulletPrefab;
        fireTransform = _fireTransform;
        BulletExplosion bulletInstance = GetItem();
        bulletInstance.transform.SetPositionAndRotation(fireTransform.position, fireTransform.rotation);
        bulletInstance.gameObject.SetActive(true);
        return bulletInstance;
    }
    public override BulletExplosion CreateItem()
    {
        return Instantiate(bulletPrefab).GetComponent<BulletExplosion>();
    }
}
