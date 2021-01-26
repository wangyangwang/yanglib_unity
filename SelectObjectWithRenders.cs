using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SelectObjectWithRenders : ScriptableWizard
{
    public GameObject tripical_plant_leaf_mesh;

    [MenuItem("Helper/SelectChildrenWithRenderer")]
    static void SelectAllChildrenWithRender()
    {
        List<Transform> withRend = new List<Transform>();
        var renderers = Selection.activeGameObject.GetComponentsInChildren<Renderer>();
        Selection.objects = renderers.Select(i => i.gameObject).ToArray();
    }



    [MenuItem("Helper/GetLightTransformIntoMaterials")]
    static void GetLightTransformIntoMaterials()
    {
        var allLights = GameObject.FindObjectsOfType<Transform>();
        foreach (var l in allLights)
        {
            if (l.name.Contains("LIGHT"))
            {
                var parent = l.transform.parent;
                var sibilings = parent.GetComponentsInChildren<Renderer>();
                foreach (var rend in sibilings)
                {
                    rend.sharedMaterial.SetInt("_USEMYLIGHTDIR", 1);
                    rend.sharedMaterial.SetVector("_MyLightDir", l.transform.position);
                }
            }
        }
    }





}
