public class EnemyTougherShooted : EnemyShooted
{
    int hp = 2;
    public override void GetShot()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().EnemyDie(value);
        }
    }
}
