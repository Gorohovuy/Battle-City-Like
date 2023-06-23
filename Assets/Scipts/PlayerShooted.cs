
using UnityEngine;

public class PlayerShooted : MonoBehaviour, IShotable
{
    private int hp = 3;
    private Vector3 spawn;
    private GameManager gameManager;
    void Start()
    {
        spawn = transform.position;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.SetPlayerHP(hp);
    }
    public void GetShot()
    {
        hp--;
        gameManager.SetPlayerHP(hp);
        transform.position = spawn;
        if (hp == 0)
        {
            gameManager.EndGame(false);
        }
    }
}
