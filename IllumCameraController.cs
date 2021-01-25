using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IllumCameraController : MonoBehaviour
{
    [System.Serializable]
    public struct CamInfo
    {
        public string name;
        public Vector3 rotation;
    }

    public CamInfo[] presets;
    string currentCamPreset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnGUI()
    {
        int buttonHeight = Screen.height / 20;
        int buttonWidth = Screen.width / 10;

        for (int i = 0; i < presets.Length; i++)
        {
            if (GUI.Button(new Rect(0, i * buttonHeight, buttonWidth, buttonHeight), presets[i].name))
            {
                transform.rotation = Quaternion.Euler(presets[i].rotation);
                currentCamPreset = presets[i].name;
            }
        }

        if (GUI.Button(new Rect(0, presets.Length * buttonHeight, buttonWidth, buttonHeight), "Capture Screenshot"))
        {
            CaptureScreenshot(currentCamPreset);
        }
    }



    void CaptureScreenshot(string additionalString)
    {
        string filename = System.DateTime.Now.ToString();
        filename = filename.Replace("/", "_");
        filename = filename.Replace(" ", "_");
        filename = filename.Replace(":", "_");
        ScreenCapture.CaptureScreenshot("C:/Users/YW/Desktop/" + filename + "__" + additionalString + ".png");
    }
}
