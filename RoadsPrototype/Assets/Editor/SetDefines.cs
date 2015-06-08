/*
  SetDefines.cs
  CitySimulator
  Created by Rohun Banerji on Dec 18/2014
  Copyright (c) 2014 Rohun Banerji. All rights reserved.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class SetDefines
{
    const string debug = "DEBUG";
    const string release = "RELEASE";

    [MenuItem("Custom/Defines/Debug Mode")]
    private static void SetDebugMode()
    {
        Debug.LogWarning("Set Debug Mode");
        SetDefinesForAllBuilds(debug);
    }
    [MenuItem("Custom/Defines/Release Mode")]
    private static void SetReleaseMode()
    {
        Debug.LogWarning("Set Release Mode");
        SetDefinesForAllBuilds(release);
    }
    private static void SetDefinesForAllBuilds(string defineName)
    {
        foreach (BuildTargetGroup buildTarget in Enum.GetValues(typeof(BuildTargetGroup)))
        {
            //invalid enum
            if (buildTarget == BuildTargetGroup.Unknown) continue;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTarget, defineName);
        }
    }

}
