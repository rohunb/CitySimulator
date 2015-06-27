/*
  ScripObjActivator.cs
  RoadsPrototype
  Created by Rohun Banerji on June 26, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RoadsEditor;

public class ScriptableObjActivator : MonoBehaviour 
{
    [SerializeField]
    private RoadGraphDatabase roadGraphDB;

#if UNITY_EDITOR
    void OnEnable()
    {
        Assert.IsNotNull(roadGraphDB);
    }
#endif
}

