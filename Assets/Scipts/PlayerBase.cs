using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IShotable
{
    public void GetShot()
    {
        Destroy(gameObject);
        FindObjectOfType<GameManager>().EndGame(false); 
    }

}
