using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed;

    public ObjectPool<Bullet> Pool {get; set;}

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<IShotable>(out IShotable target))
        {
            target.GetShot();
            Pool.Release(this);
            return;
        }
        
        if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Pool.Release(this);
            return;
        }
    }
}
