/*
  RoadGraphEditor.cs
  RoadsPrototype
  Created by Rohun Banerji on June 21, 2015.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using RoadsPrototype;
using Pathfinding;

[CustomEditor(typeof(RoadSystem))]
public class RoadGraphEditor : Editor
{
    private Transform fromNodeTrans;
    private Transform toNodeTrans;
    private float cost;
    private bool directed;
    private int fromID = 0;
    private int toID = 1;

    private Graph<RoadNode> roadGraph;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoadSystem roadSystem = target as RoadSystem;

        if (!roadSystem) return;

        roadGraph = roadSystem.RoadGraph;

        //Add to graph
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("Add connection to road graph");
            fromNodeTrans = (Transform)EditorGUILayout.ObjectField("FromNode", fromNodeTrans, typeof(Transform), true);
            fromID = EditorGUILayout.IntField("ID", fromID);
            toNodeTrans = (Transform)EditorGUILayout.ObjectField("ToNode", toNodeTrans, typeof(Transform), true);
            toID = EditorGUILayout.IntField("ID", toID);
            cost = EditorGUILayout.FloatField("Cost", cost);
            directed = EditorGUILayout.Toggle("Directed", directed);
            if(GUILayout.Button("Add Connection"))
            {
                var fromNode = new GraphNode<RoadNode>(fromID, new RoadNode(fromNodeTrans.position));
                var toNode= new GraphNode<RoadNode>(toID, new RoadNode(toNodeTrans.position));
                roadSystem.AddConnection(fromNode, toNode, cost, directed);
                ++fromID;
                ++toID;
            }
        }
        EditorGUILayout.EndVertical();
    }

    void OnSceneGUI()
    {

        Handles.DrawLine(Vector3.zero, Vector3.right * 10.0f);
        Handles.ArrowCap(1, Vector3.zero, Quaternion.identity, 1.0f);

        int counter = 0;
        if (roadGraph==null) return;

        foreach (GraphNode<RoadNode> node in roadGraph.NodeList)
        {
            foreach (var connection in node.Connections)
            {
                //Handles.ArrowCap(counter++, )
            }
        }
    }

}

