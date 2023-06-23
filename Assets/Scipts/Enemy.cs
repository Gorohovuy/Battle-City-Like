using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public float calcPathPause;
    public float nextWaypointDistance;
    public float speed;
    public float shootCD;
    public Transform target;

    bool canShoot = true;
    BulletPool bulletPool;

    // For pathfinding
    Path path;
    int currentWayPoint;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerBase>().transform;
        bulletPool = FindObjectOfType<BulletPool>();

        InvokeRepeating("UpdatePath", 0f, calcPathPause);
    }

    void OnPathComplite(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplite);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.up);
        if (raycast.transform.GetComponent<EnemyTagetLabel>() && canShoot)
        {
            Transform bulletTransform = bulletPool.GetBullet().transform;
            bulletTransform.position = transform.position + transform.up * .6f;
            bulletTransform.rotation = transform.rotation;
            canShoot = false;
            StartCoroutine(ResetCD());
        }
        else
            Move();
    }

    private void Move()
    {
        if (path == null) return;

        Vector2 direction = (Vector2)path.vectorPath[currentWayPoint] - rb.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }
        
        direction.Normalize();

        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * speed);
        Rotate(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }

    private void Rotate(Vector3 direction)
    {
        switch (direction)
        {
            case Vector3 v when v.Equals(Vector3.up):
            transform.eulerAngles = new Vector3(0, 0, 0);
            break;

            case Vector3 v when v.Equals(Vector3.right):
            transform.eulerAngles = new Vector3(0, 0, 270);
            break;

            case Vector3 v when v.Equals(Vector3.down):
            transform.eulerAngles = new Vector3(0, 0, 180);
            break;

            case Vector3 v when v.Equals(Vector3.left):
            transform.eulerAngles = new Vector3(0, 0, 90);
            break;
        }
    }

    IEnumerator ResetCD()
    {
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
}
