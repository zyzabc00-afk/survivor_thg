using UnityEngine;

public class RecordingUIController : MonoBehaviour
{
    public InputRecorder recorder;

    public void StartRecording()
    {
        recorder.StartRecording();
    }

    public void StopRecording()
    {
        recorder.StopAndSave();
    }
}
