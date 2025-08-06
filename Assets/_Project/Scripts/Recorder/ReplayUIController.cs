using UnityEngine;

public class ReplayUIController : MonoBehaviour
{
    public InputReplayer inputReplayer;

    public void StartReplay()
    {
        inputReplayer.LoadAndReplay();
    }
}