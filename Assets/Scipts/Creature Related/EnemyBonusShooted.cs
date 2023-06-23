using UnityEngine;

public class EnemyBonusShooted : EnemyShooted
{
    public GameObject bonusPrefab;

    public override void GetShot()
    {
        Instantiate(bonusPrefab, transform.position, Quaternion.identity);
        base.GetShot();
    }
}
