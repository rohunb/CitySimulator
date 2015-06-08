/*
  ScriptTemplateImporter.cs
  CitySimulator
  Created by Rohun Banerji on May 29/2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ScriptTemplateImporter : UnityEditor.AssetModificationProcessor 
{
    public static void OnWillCreateAsset(string path)
    {
        //not a script
        if (!path.Contains(".cs")) return;

        path = path.Replace(".meta", "");
        int index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        string file = File.ReadAllText(path);
        file = file.Replace("#CREATIONDATE#", DateTime.Now.ToString("MMMM dd, yyyy"));
        file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
        file = file.Replace("#YEAR#", DateTime.Now.Year.ToString());
        File.WriteAllText(path, file);
        Debug.Log("Injecting copyright info: " +path.Replace(Application.dataPath, ""));
        AssetDatabase.Refresh();
    }

}
