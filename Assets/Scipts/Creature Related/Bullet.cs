using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed; // Flying speed

    public ObjectPool<Bullet> Pool {get; set;}

    // Update is called once per frame
    void Update()
    {
        // Move in direction, in which was spawned
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
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
