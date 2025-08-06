using UnityEngine;

public class FlipController : MonoBehaviour
{
    private float lastPosX;
    private float currentPosX;
    private Vector3 currentLocalScale;

    void Start()
    {
        lastPosX = transform.position.x;
        currentLocalScale = transform.localScale;
    }
    void Update()
    {
        currentPosX = transform.position.x;

        if (currentPosX > lastPosX)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentLocalScale.x), currentLocalScale.y, currentLocalScale.z);
        }
        else if (currentPosX < lastPosX)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentLocalScale.x), currentLocalScale.y, currentLocalScale.z);
        }
        lastPosX = currentPosX;
    }
}