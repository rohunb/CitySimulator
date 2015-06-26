/*
  RoadGraphEditor.cs
  RoadsPrototype
  Created by Rohun Banerji on June 21, 2015.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RoadsPrototype;
using Pathfinding;

namespace RoadsEditor
{
    [CustomEditor(typeof(RoadGraphDatabase))]
    public class RoadGraphEditor : Editor
    {
        private RoadNode fromNode;
        private RoadNode toNode;
        private float cost;
        private bool directed;
        //private int fromID = 0;
        //private int toID = 1;
        //private bool autoGenIDs = true;

        private Graph<RoadNode> roadGraph;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RoadGraphDatabase roadGraphDB = target as RoadGraphDatabase;

            if (roadGraphDB == null) return;

            roadGraph = roadGraphDB.RoadGraph;

            if (roadGraph == null) return;

            //Add to graph
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Add connection to road graph");
                //From Node
                fromNode = (RoadNode)EditorGUILayout.ObjectField("FromNode", fromNode, typeof(RoadNode), true);
                //fromID = EditorGUILayout.IntField("ID", fromID);
                //To Node
                toNode = (RoadNode)EditorGUILayout.ObjectField("ToNode", toNode, typeof(RoadNode), true);
                //toID = EditorGUILayout.IntField("ID", toID);
                //autoGenIDs = EditorGUILayout.Toggle("Auto Generate IDs", autoGenIDs);
                cost = EditorGUILayout.FloatField("Cost", cost);
                directed = EditorGUILayout.Toggle("Directed", directed);

                //Buttons
                if (GUILayout.Button("Add Connection"))
                {
                    AddConnection(roadGraphDB);
                }
                if (GUILayout.Button("Clear"))
                {
                    Clear(roadGraphDB);
                }
                if (GUILayout.Button("Create New Road Node"))
                {
                    CreateNewRoadNode(roadGraphDB);
                }
                if(GUILayout.Button("Display"))
                {
                    Display();
                }
            }
            EditorGUILayout.EndVertical();
        }
        private void AddConnection(RoadGraphDatabase roadGraphDB)
        {
            roadGraphDB.AddConnection(fromNode, toNode, cost, directed);
            EditorUtility.SetDirty(roadGraphDB);
        }
        private void Clear(RoadGraphDatabase roadGraphDB)
        {
            roadGraph.Clear();
            fromNode = null;
            toNode = null;
            EditorUtility.SetDirty(roadGraphDB);
        }
        private void CreateNewRoadNode(RoadGraphDatabase roadGraphDB)
        {
            var newRoadNode = (RoadNode)(Instantiate(roadGraphDB.RoadNodePrefab));
            RoadNode[] roadNodes = FindObjectsOfType<RoadNode>();
            int ID = 0;
            while (roadNodes.Any(roadNode => roadNode.ID == ID))
            {
                ++ID;
            }
            newRoadNode.ID = ID;
            newRoadNode.name = "RoadNode " + ID;
            Selection.activeGameObject = newRoadNode.gameObject;
            EditorGUIUtility.PingObject(Selection.activeGameObject);
        }
        void Display()
        {
            Debug.LogWarning("Road Graph:");
            foreach (GraphNode<RoadNode> node in roadGraph)
            {
                foreach (var edge in node.Connections)
                {
                    Debug.Log("From " + edge.FromNode + " To " + edge.ToNode + " Cost " + edge.Cost);
                }
            }
        }
        void OnEnable()
        {
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }
        void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }
        void OnSceneGUI(SceneView sceneView)
        {
            if (roadGraph == null) return;
            int counter = 0;

            foreach (GraphNode<RoadNode> node in roadGraph)
            {
                foreach (var connection in node.Connections)
                {
                    Vector3 fromNodePos = connection.FromNode.Value.Position;
                    Vector3 toNodePos = connection.ToNode.Value.Position;
                    Vector3 direction = toNodePos - fromNodePos;
                    Quaternion arrowRotation = Quaternion.LookRotation(direction);
                    Handles.color = Color.red;
                    //red line to show the connection
                    Handles.DrawLine(fromNodePos, toNodePos);
                    Handles.color = Color.blue;
                    //Draws an arrow near the toNode
                    Handles.ConeCap(counter++, toNodePos - direction.normalized*.15f, arrowRotation, .1f);
                }
            }
            HandleUtility.Repaint();
        }
        [MenuItem("Custom/RoadGraph/Select Database #&r")]
        private static void SelectRoadGraphDB()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/Databases/RoadGraphDatabase.asset");
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}

