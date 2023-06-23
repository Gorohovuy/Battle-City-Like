using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Movement speed
    private Rigidbody2D rb;
    private Vector2 direction;
    private BulletPool bulletPool;
    private GameManager gm;

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
            
            Transform bulletTransform = bulletPool.GetBullet().transform;
            bulletTransform.position = transform.position + transform.up * .6f;
            bulletTransform.rotation = transform.rotation;
        }
    }

    /// <summary>
    /// Rotates the object so that it faces in the right direction
    /// </summary>
    /// <param name="direction">Must be equals to up, right, down or left</param>
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
}
