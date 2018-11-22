using UnityEngine;

public class Checkpoint : MonoBehaviour, ICheckpoint
{
    Transform ICheckpoint.Checkpoint()
    {
        return transform;
    }
}
