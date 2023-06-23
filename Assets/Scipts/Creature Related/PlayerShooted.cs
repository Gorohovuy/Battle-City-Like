
using UnityEngine;

public class PlayerShooted : MonoBehaviour, IShotable
{
    int hp = 3;
    Vector3 spawn; // Point to respawn after collision with bullet
    GameManager gameManager;
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
