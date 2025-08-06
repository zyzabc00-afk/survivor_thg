using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minPos, maxPos;

    public void Start()
    {
        var box2D = GameObject.Find("MovementBounds").GetComponent<BoxCollider2D>().bounds;
        minPos = box2D.min;
        maxPos = box2D.max;
    }

    public void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPos = transform.position;

        newPos.x = Mathf.Clamp(target.position.x, minPos.x, maxPos.y);
        newPos.y = Mathf.Clamp(target.position.y, minPos.y, maxPos.x);
        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
