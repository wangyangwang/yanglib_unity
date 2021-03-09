using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShowHierachyOnScreen : MonoBehaviour
{

    string hierachy = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
        
    }

    void OnDrawGizmos()
    {

   

        var o = UnityEditor.Selection.activeTransform;
        if (o == null) return;
        hierachy = o.name + "\n";

        string prefix = "-";
        while (o.parent != null)
        {
            hierachy += prefix + o.parent.name + "\n";
            o = o.parent;
            prefix += "-";
        }


        UnityEditor.Handles.Label(UnityEditor.Selection.activeTransform.position,hierachy);
    }


    private void OnGUI()
    {
       
    }
}
