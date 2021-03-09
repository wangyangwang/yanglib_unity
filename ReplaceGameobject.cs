using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ReplaceGameobject : EditorWindow
{
    public string toReplaceNamePattern;
    public GameObject replaceWith;
    public bool useTag;
    public string tagName;

    [MenuItem("Helper/GameObject Swap Helper")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ReplaceGameobject window = (ReplaceGameobject)EditorWindow.GetWindow(typeof(ReplaceGameobject));
        window.Show();
    }

    void OnGUI()
    {


        var so = new SerializedObject(this);


        EditorGUILayout.PropertyField(so.FindProperty("useTag"));

        tagName = EditorGUILayout.TextField("tag", tagName);
        toReplaceNamePattern = EditorGUILayout.TextField("name pattern", toReplaceNamePattern);
        //EditorGUILayout.PropertyField(so.FindProperty("toReplaceNamePattern"));
        EditorGUILayout.PropertyField(so.FindProperty("replaceWith"));

        var regex = new Regex(toReplaceNamePattern);
        if (GUILayout.Button("Replace"))
        {
            Transform[] matched;
            if (useTag)
            {
                var gameobjects = GameObject.FindGameObjectsWithTag(tagName).Where(i => i.transform);
                matched = gameobjects.Select(i => i.transform).ToArray();
            }
            else
            {
                var everything = GameObject.FindObjectsOfType<Transform>();
                matched = everything.Where(i => regex.IsMatch(i.name) == true).ToArray();
            }


            foreach (var obj in matched)
            {

                //var newone = GameObject.Instantiate(replaceWith);
                var newone = PrefabUtility.InstantiatePrefab(replaceWith as GameObject) as GameObject;

                newone.transform.localScale = obj.transform.localScale;
                newone.transform.position = obj.transform.position;
                newone.transform.rotation = obj.transform.rotation;
                newone.transform.parent = obj.transform.parent;
                //obj.name = obj.name + " (old)";
                obj.gameObject.SetActive(false);
                Debug.Log("replaced " + obj.name + " with " + newone.name);

            }
        }


        so.ApplyModifiedProperties();



    }
}
