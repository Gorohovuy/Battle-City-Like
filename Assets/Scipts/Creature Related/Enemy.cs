using UnityEngine;
using Pathfinding;

public class Enemy : Creature
{
    public float calcPathPause;
    public float nextWaypointDistance;
    public float speed;
    public Transform target; 

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
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.up, 16);
        if (raycast.transform.GetComponent<EnemyTagetLabel>())
        {
            Shoot();
        }
        else
            Move();
    }

    void Move()
    {
        if (path == null) return;

        Vector2 direction = (Vector2)path.vectorPath[currentWayPoint] - rb.position;

        // Allow move only in up, right, bottom or left direction
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
}
