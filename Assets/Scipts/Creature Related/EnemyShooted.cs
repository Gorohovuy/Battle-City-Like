using UnityEngine;

public class EnemyShooted : MonoBehaviour, IShotable
{
    public int value;
    public virtual void GetShot()
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().EnemyDie(value);
    }
}
