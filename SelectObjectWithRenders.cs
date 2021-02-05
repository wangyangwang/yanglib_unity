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
        
        var selected = Selection.activeTransform;

        if (selected.name.Contains("LIGHT") == false)
        {
            Debug.Log("selection is not LIGHT");
            return;
        }

        Vector3 lightPos = selected.localPosition;

        var parent = selected.parent;
        var sibilings = parent.GetComponentsInChildren<Renderer>();

        foreach (var rend in sibilings)
        {
            rend.sharedMaterial.SetVector("_MyLightPos", lightPos);
        }

     
    }





}
