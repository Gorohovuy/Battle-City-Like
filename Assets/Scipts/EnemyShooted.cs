using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooted : MonoBehaviour, IShotable
{
    public int value;
    public void GetShot()
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().EnemyDie(value);
    }
}
