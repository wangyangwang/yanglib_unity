﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class NDIModeToggler : MonoBehaviour
{

    public bool useNDICams;
    public Camera[] NDICams;
    public Transform NDISenderContainer;
    public GUISkin skin;    

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        foreach (var c in NDICams)
        {
            c.gameObject.SetActive(useNDICams);
        }
        NDISenderContainer.gameObject.SetActive(useNDICams);
    }

    private void OnGUI()
    {
        if (!GlobalStaticVars.showGUI) return;
        useNDICams = GUI.Toggle(new Rect(0,Screen.height-100,300,100), useNDICams, "use RenderTexture cams", skin.GetStyle("toggle"));
    }
}
