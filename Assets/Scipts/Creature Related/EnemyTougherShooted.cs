using UnityEngine;

public class EnemyTougherShooted : MonoBehaviour, IShotable
{
    public int value;
    int hp = 2;
    public void GetShot()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().EnemyDie(value);
        }
    }
}
