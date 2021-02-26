using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class IllumCameraController : MonoBehaviour
{
    [System.Serializable]
    public struct Cam
    {
        public string name;
        public Camera cam;
    }

    public Cam[] cameras;
    string activeCam;
    public bool showControl;
    public int superSize = 1;

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
        if (!showControl) return;
        if (!GlobalStaticVars.showGUI) return;
        int buttonHeight = Screen.height / 20;
        int buttonWidth = Screen.width / 10;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize =  (int)(Screen.width / 100);

        for (int i = 0; i < cameras.Length; i++)
        {
            if (GUI.Button(new Rect(0, i * buttonHeight, buttonWidth, buttonHeight), cameras[i].name,buttonStyle))
            {
                foreach (var c in cameras) { c.cam.gameObject.SetActive(false); }
                cameras[i].cam.gameObject.SetActive(true);
                activeCam = cameras[i].name;
            }

            if (GUI.Button(new Rect(buttonWidth, i * buttonHeight, buttonWidth / 3f, buttonHeight), "select", buttonStyle))
            {
                UnityEditor.Selection.activeObject = cameras[i].cam.gameObject;
            }
        }

        if (GUI.Button(new Rect(0, cameras.Length * buttonHeight, buttonWidth, buttonHeight), "Capture Screenshot"))
        {
            StartCoroutine(GoCapture(activeCam));
        }
    }


    

    public void CaptureScreenshot(string additionalString)
    {
        
        string filename = System.DateTime.Now.ToString();
        filename = filename.Replace("/", "_");
        filename = filename.Replace(" ", "_");
        filename = filename.Replace(":", "_");

        ScreenCapture.CaptureScreenshot("C:/Users/YW/Desktop/" + filename + "__" + additionalString + ".png", superSize);
       
    }

    IEnumerator GoCapture(string additionalString)
    {
        GlobalStaticVars.showGUI = false;
        yield return new WaitForEndOfFrame();
        CaptureScreenshot(additionalString);
        GlobalStaticVars.showGUI = true;

    }
    
}



[CustomEditor(typeof(IllumCameraController))]
public class IllumCameraControllerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var ta = target as IllumCameraController;
        if (GUILayout.Button("capture"))
        {
            ta.CaptureScreenshot("");
        }
    }
}