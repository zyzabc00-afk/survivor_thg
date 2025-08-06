using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class InputReplayer : MonoBehaviour
{
    private List<InputData> interactions;
    private float startTime;
    public GameObject replayObject;

    void Update()
    {
        string path = Path.Combine(Application.persistentDataPath, "input_record.json");

        Button replayButton = replayObject.GetComponent<Button>();

        replayButton.interactable = File.Exists(path);
    }

    public void LoadAndReplay()
    {
        string path = Application.persistentDataPath + "/input_record.json";
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        InputEventList eventList = JsonUtility.FromJson<InputEventList>(json);
        interactions = eventList.events;

        if (interactions == null || interactions.Count == 0)
        {
            Debug.LogError("No input events to replay.");
            return;
        }

        StartCoroutine(ReplayCoroutine());
    }


    private IEnumerator ReplayCoroutine()
    {
        startTime = Time.time;

        foreach (var evt in interactions)
        {
            float delay = evt.time - (Time.time - startTime);
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            InputMarker.Instance.Play(evt);
        }
    }
}