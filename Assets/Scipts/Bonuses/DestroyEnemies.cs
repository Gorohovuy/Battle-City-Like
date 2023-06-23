using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour, IPickable
{
    public float timeToLive; // Bonus can live only some seconds

    void Start()
    {
        Destroy(gameObject,timeToLive);
    }
    public void Activate()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
            gm.EnemyDie(enemy.GetComponent<EnemyShooted>().value);
        }
        Destroy(gameObject);
    }
}
