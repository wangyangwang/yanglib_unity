using UnityEngine;
using System.Collections;
using UnityEditor;
#if UNITY_EDITOR
[ExecuteInEditMode]
public class PanoCamScript : MonoBehaviour
{

    public int camCount = 16;

    void Update()
    {
        Init();
    }

    public void Init()
    {

        if (transform.childCount == 0)
        {
            for (int i = 0; i < camCount; i++)
            {
                GameObject go = new GameObject();
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.name = "subCam" + i;
                go.AddComponent<Camera>();
            }
        }
        else if (transform.childCount != camCount)
        {
            Debug.LogError("to have the new cam count, manually delete all child cameras!");
        }

        // update all cameras

        prepSubCameras();

    }

    void prepSubCameras()
    {
        float hwratio = (float)Screen.height / (float)Screen.width * (float)transform.childCount;
        Debug.LogFormat("Screen height {0}, ScreenWidth {1}, child Cound {2}, hwratio {3}", Screen.height, Screen.width, transform.childCount, hwratio);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            var cam = child.GetComponent<Camera>();
            float onepart = 1.0f / transform.childCount;
            cam.fieldOfView = 2 * Mathf.Atan(hwratio * Mathf.Tan(180 * Mathf.Deg2Rad / transform.childCount)) * Mathf.Rad2Deg;
            //change rotation
            child.localEulerAngles = new Vector3(0, 360.0f / transform.childCount * i, 0);
            //viewport rect
            cam.rect = new Rect(onepart * i, 0, onepart, 1);
        }
    }
}


[CustomEditor(typeof(PanoCamScript))]
class PanoCamEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (PanoCamScript)target;
        base.OnInspectorGUI();



    }
}
#endif