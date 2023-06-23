using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    ObjectPool<Bullet> Pool;
    // Start is called before the first frame update
    void Start()
    {
        Pool = new ObjectPool<Bullet>(
            CreatePoolItem,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject,
            maxSize: 30
        );
    }

    public Bullet GetBullet()
    {
        return Pool.Get();
    }

    #region Pool
    Bullet CreatePoolItem()
    {
        GameObject tmp = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        tmp.transform.parent = transform;
        Bullet bullet = tmp.GetComponent<Bullet>();
        bullet.Pool = Pool;
        return bullet;
    }

    void OnReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    #endregion
}
