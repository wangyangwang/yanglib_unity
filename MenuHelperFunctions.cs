using System.Linq;
using UnityEditor;
using UnityEngine;



public class MenuHelperFunctions : MonoBehaviour
{
    public GameObject tropical_plant_leaf_mesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMeshToChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount <= 0)
            {
                var newborn = GameObject.Instantiate(tropical_plant_leaf_mesh);
                newborn.transform.SetParent(child);
                newborn.transform.localPosition = Vector3.zero;
                // newborn.transform.localRotation = Quaternion.identity;
                newborn.transform.localScale = Vector3.one;
            }
        }
    }

    public void DeleteCloned()
    {
        var children = transform.GetComponentsInChildren<Transform>().ToList();
        children.Remove(transform);
        foreach (var c in children)
        {
            if (c.name.Contains("(Clone)"))
            {
                DestroyImmediate(c.gameObject);
            }
        }

    }


}

[CustomEditor(typeof(MenuHelperFunctions))]
public class HelperFunctonsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MenuHelperFunctions ins = (MenuHelperFunctions)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("add"))
        {
            ins.AddMeshToChildren();
        }

        if (GUILayout.Button("remove Clones"))
        {
            ins.DeleteCloned();
        }
    }
}
