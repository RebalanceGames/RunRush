using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor((typeof(LevelOlusturma)))]
public class LevelOlusturmaEditor: Editor
{ public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var levelOlusturma = (LevelOlusturma)target;

        if(GUILayout.Button("Test", GUILayout.Height(40)))
        {
            levelOlusturma.Test();
        }
        if(GUILayout.Button("obje Olustur", GUILayout.Height(40)))
        {
            levelOlusturma.InstanteObj();
        }
        
        
        
    }
}
