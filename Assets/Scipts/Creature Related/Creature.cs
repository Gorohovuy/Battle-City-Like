using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for tank like creature
/// </summary>
public class Creature : MonoBehaviour
{
    public float shootCD;
    protected bool canShoot = true;
    protected BulletPool bulletPool;

    protected void Shoot()
    {
        if (canShoot)
        {
            Transform bulletTransform = bulletPool.GetBullet().transform;
            bulletTransform.position = transform.position + transform.up * .6f;
            bulletTransform.rotation = transform.rotation;
            canShoot = false;
            StartCoroutine(ResetCD());
        }
    }

    /// <summary>
    /// Rotates the object so that it faces in the right direction
    /// </summary>
    /// <param name="direction">Must be equals to up, right, down or left</param>
    protected void Rotate(Vector3 direction)
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
