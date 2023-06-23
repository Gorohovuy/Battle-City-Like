using UnityEngine;
using Pathfinding;

/// <summary>
/// Attached GameObject will be oneshoted
/// </summary>
public class WeakWall : MonoBehaviour, IShotable
{
    public void GetShot()
    {
        Destroy(gameObject);
        AstarData.active.Scan();
    }
}
