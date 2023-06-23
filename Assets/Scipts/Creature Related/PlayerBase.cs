using UnityEngine;

public class PlayerBase : MonoBehaviour, IShotable
{
    public void GetShot()
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().EndGame(false); 
    }

}
