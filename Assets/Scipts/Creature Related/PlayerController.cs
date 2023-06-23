using UnityEngine;

public class PlayerController : Creature
{
    public float speed; // Movement speed
    Rigidbody2D rb;
    Vector2 direction;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletPool = FindObjectOfType<BulletPool>();
        gm = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.IsGameEnd) return;

        direction = Vector2.zero;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Allow only up, right, down, left directions
        if (verticalInput !=0 )
        {
            direction.y += verticalInput;
        }
        else if (horizontalInput != 0)
        {
            direction.x += horizontalInput;
        }

        Rotate(direction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
}
