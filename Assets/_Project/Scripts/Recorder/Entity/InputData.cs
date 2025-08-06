using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputData
{
    public float time;
    public string type;
    public Vector2 position;
    public Vector2 endPosition; // chỉ dùng cho drag
    public float duration;
    public int fingerId;
}

[System.Serializable]
public class InputEventList
{
    public List<InputData> events = new();
}