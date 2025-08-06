using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputRecorder : MonoBehaviour
{
    private List<InputData> recordedInputs = new();
    private float startTime;
    private bool isRecording = false;

    void Update()
    {
        if (!isRecording) return;

        // Touch (hoạt động cả trên mobile và Editor nếu bạn mô phỏng)
        foreach (var touch in Touchscreen.current.touches)
        {
            if (!touch.press.isPressed) continue;

            Vector2 pos = touch.position.ReadValue();
            if (IsPointerOverUI(pos))
            {
                // Ta chỉ log khi mới chạm vào
                if (touch.press.wasPressedThisFrame)
                {
                    AddEvent("tap", pos, Vector2.zero, 0f, touch.touchId.ReadValue());
                }
            }
        }

        // Mouse (dành cho Editor)
#if UNITY_EDITOR
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            if (IsPointerOverUI(mousePos))
            {
                AddEvent("tap", mousePos, Vector2.zero, 0f, -1);
            }
        }
#endif
    }

    public void StartRecording()
    {
        recordedInputs.Clear();
        startTime = Time.time;
        isRecording = true;
    }

    public void StopAndSave()
    {
        isRecording = false;
        string json = JsonUtility.ToJson(new InputEventList { events = recordedInputs }, true);
        File.WriteAllText(Application.persistentDataPath + "/input_record.json", json);
        Debug.Log("Saved to: " + Application.persistentDataPath + "/input_record.json");
    }

    private void AddEvent(string type, Vector2 pos, Vector2 endPos, float duration, int fingerId)
    {
        recordedInputs.Add(new InputData
        {
            time = Time.time - startTime,
            type = type,
            position = pos,
            endPosition = endPos,
            duration = duration,
            fingerId = fingerId
        });
    }

    private bool IsPointerOverUI(Vector2 position)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = position
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count > 0;
    }
}