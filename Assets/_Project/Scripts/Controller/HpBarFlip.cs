using UnityEngine;

public class HpBarFlip : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x = target.localScale.x < 0 ? -1 : 1;
        transform.localScale = scale;
    }
}