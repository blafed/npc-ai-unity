using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    private void Update()
    {
        transform.position = target.position;
    }
}