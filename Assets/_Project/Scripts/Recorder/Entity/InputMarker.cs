using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputMarker : MonoBehaviour
{
    public static InputMarker Instance;

    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject markerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(InputData data)
    {
        StartCoroutine(ReplayRoutine(data));
    }

    private IEnumerator ReplayRoutine(InputData data)
    {
        // Tạo marker cho debug (hoặc bỏ nếu không cần)
        if (markerPrefab != null)
        {
            var marker = Instantiate(markerPrefab, transform);
            marker.transform.position = data.position;
            Destroy(marker, 1f);
        }

        // Chờ tới đúng thời gian của thao tác
        yield return new WaitForSeconds(data.time);

        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = data.position,
            pointerId = data.fingerId
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        if (results.Count == 0)
        {
            yield break; // Không có đối tượng nào dưới touch point
        }

        GameObject target = results[0].gameObject;

        // Mô phỏng sự kiện theo loại thao tác
        switch (data.type)
        {
            case "tap":
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerClickHandler);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerUpHandler);
                break;

            case "hold":
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerDownHandler);
                yield return new WaitForSeconds(data.duration);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerUpHandler);
                break;

            case "drag":
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerDownHandler);
                pointer.position = Vector2.Lerp(data.position, data.endPosition, 0.5f);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.beginDragHandler);
                pointer.position = data.endPosition;
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.dragHandler);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.endDragHandler);
                ExecuteEvents.Execute(target, pointer, ExecuteEvents.pointerUpHandler);
                break;

            default:
                Debug.LogWarning($"Unrecognized input type: {data.type}");
                break;
        }
    }
}
