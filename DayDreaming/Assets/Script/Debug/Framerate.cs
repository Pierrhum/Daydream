using UnityEngine;
using UnityEngine.UI;

public class Framerate : MonoBehaviour
{
    private Text textArea;
    private int deltaFrames;
    private int targetFramerate;
    [SerializeField] private int newTargetFramerate;

    void Start()
    {
        textArea = GetComponentInChildren<Text>();
        deltaFrames = 0;
        UpdateFramerate();
    }

    void Update()
    {
        if(newTargetFramerate != targetFramerate)
        {
            UpdateFramerate();
        }

        if (deltaFrames == 10)
        {
            textArea.text = (1.0 / Time.smoothDeltaTime).ToString("0") + "fps";
            deltaFrames = 0;
        }
        else deltaFrames++;
    }

    void UpdateFramerate()
    {
        Application.targetFrameRate = newTargetFramerate;
        targetFramerate = newTargetFramerate;
    }
}
