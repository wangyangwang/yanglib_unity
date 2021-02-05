using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class MaterialAssigner : EditorWindow
{
    string parentTR;
    string targetStrMatch;

    [System.Serializable]
    public struct MatInfo
    {
        public Material material;
        public string parentNamePattern;
        public string namePattern;
    }

    public List<MatInfo> matInfos = new List<MatInfo>();
    Vector2 scrollPos;



    [MenuItem("Helper/Material Assigner")]
    static void Init()
    {
       
        // Get existing open window or if none, make a new one:
        MaterialAssigner window = (MaterialAssigner)EditorWindow.GetWindow(typeof(MaterialAssigner));
        window.Show();
    }

    void OnGUI()
    {
        var so = new SerializedObject(this);

        var list = so.FindProperty("matInfos");

        if (GUILayout.Button("Add New Mat Info Group"))
        {
            matInfos.Add(new MatInfo());
        }


        if (GUILayout.Button("Remove Last Mat Info Group"))
        {
            matInfos.RemoveAt(matInfos.Count - 1);
        }


        if (matInfos == null) return;
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < matInfos.Count; i++)
        {

            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            // var group = lis
            // group.material = EditorGUILayout.ObjectField(group.material, typeof(Material), false) as Material;
            // group.parentNamePattern = EditorGUILayout.TextField("Parent Object Name", group.parentNamePattern);
            // group.namePattern = EditorGUILayout.TextField("Match Pattern", group.namePattern);
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();


        if (GUILayout.Button("Assign Away"))
        {
            foreach (var i in matInfos)
            {
                var containerRegex = new Regex(i.parentNamePattern);
                var objectRegex = new Regex(i.namePattern);

                var containers = GameObject.FindObjectsOfType<Transform>();
                foreach (var container in containers)
                {
                    if (containerRegex.IsMatch(container.name))
                    {
                        var allRends = container.GetComponentsInChildren<Renderer>().ToList();
                        allRends.ForEach(rend =>
                        {
                            if (objectRegex.IsMatch(rend.name))
                            {
                                rend.sharedMaterial = i.material;
                            }
                        });
                    }
                }
            }
        }


        so.ApplyModifiedProperties();




        // GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        // myString = EditorGUILayout.TextField("Text Field", myString);

        // groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        // myBool = EditorGUILayout.Toggle("Toggle", myBool);
        // myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        // EditorGUILayout.EndToggleGroup();
    }
}
