/*
  RoadGraphDatabase.cs
  RoadsPrototype
  Created by Rohun Banerji on June 21, 2015.
*/

using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using RoadsPrototype;

namespace RoadsEditor
{
    //[CreateAssetMenuAttribute]
    public class RoadGraphDatabase : MonoBehaviour
    {
        [SerializeField]
        private RoadNode roadNodePrefab;
        public RoadNode RoadNodePrefab
        {
            get { return roadNodePrefab; }
        }

        //This is what the SO will actually save
        [SerializeField]
        private List<SerializableRoadNode> sz_roadNodeList;// = new List<GraphNode<RoadNode>>();

        //non-serialized data - used at runtime
        //converted to the list since that's what Unity can serialize
        private Graph<RoadNode> roadGraph;// = new Graph<RoadNode>();
        public Graph<RoadNode> RoadGraph
        {
            get { return roadGraph; }
        }

        //GUI interface
        public void AddConnection(RoadNode fromNode, RoadNode toNode, float cost, bool directed)
        {
            Assert.IsNotNull(fromNode);
            Assert.IsNotNull(toNode);
            if(roadGraph == null)
            {
                roadGraph = new Graph<RoadNode>();
                Debug.Log("New Graph");
            }
            Assert.IsNotNull(roadGraph);

            if (roadGraph.ContainsValue(fromNode) && roadGraph.ContainsValue(toNode))
            {
                return;
            }

            if (directed)
            {
                roadGraph.AddAndConnectDirected(fromNode, toNode, cost);
            }
            else
            {
                roadGraph.AddAndConnect(fromNode, toNode, cost);
            }

            Save();
        }

        /// <summary>
        /// Converts the graph to a format that Unity can serialize (List)
        /// </summary>
        public void Save()
        {
            Debug.Log("Saving");
            if (sz_roadNodeList == null)
            {
                sz_roadNodeList = new List<SerializableRoadNode>();
            }
            else
            {
                sz_roadNodeList.Clear();
            }
            if (roadGraph == null) return;

            foreach (GraphNode<RoadNode> node in roadGraph)
            {
                SerializableRoadNode sz_roadNode = new SerializableRoadNode
                {
                    RoadNode = node.Value,
                    //Connections = new List<SerializableRoadEdge>()
                };

                foreach (var edge in node.Connections)
                {
                    SerializableRoadEdge sz_edge = new SerializableRoadEdge
                    {
                        FromNode = edge.FromNode.Value,
                        ToNode = edge.ToNode.Value,
                        Cost = edge.Cost
                    };
                    sz_roadNode.Connections.Add(sz_edge);
                }
                sz_roadNodeList.Add(sz_roadNode);
            }
        }
        /// <summary>
        /// Converts the serialized List of RoadNodes to a Graph
        /// </summary>
        public void Load()
        {
            Debug.Log("Loading");
            if (roadGraph == null)
            {
                roadGraph = new Graph<RoadNode>();
            }
            else
            {
                roadGraph.Clear();
            }

            if (sz_roadNodeList == null) return;

            //adding nodes only...connections later
            foreach (var roadNode in sz_roadNodeList)
            {
                roadGraph.AddNode(roadNode.RoadNode);
            }
            Assert.IsTrue(roadGraph.NodeCount == sz_roadNodeList.Count);

            //add connections now...have to do it in separate loops to ensure all nodes actually exist in the graph
            foreach (var roadNode in sz_roadNodeList)
            {
                GraphNode<RoadNode> fromNode = roadGraph.GetNodeByValue(roadNode.RoadNode);
                Assert.IsNotNull(fromNode);
                //add all of this node's connections to the graph
                foreach (var edge in roadNode.Connections)
                {
                    //make sure the fromNode for this edge is the node we are currently dealing with
                    Assert.IsTrue(edge.FromNode.Equals(roadNode.RoadNode));
                    GraphNode<RoadNode> toNode = roadGraph.GetNodeByValue(edge.ToNode);
                    Assert.IsNotNull(toNode);
                    roadGraph.ConnectDirected(fromNode, toNode, edge.Cost);
                }
                Assert.IsTrue(fromNode.Connections.Count == roadNode.Connections.Count);
            }
        }

        public void Clear()
        {
            Assert.IsNotNull(roadGraph);
            Assert.IsNotNull(sz_roadNodeList);
            Debug.Log("Clear");
            roadGraph.Clear();
            sz_roadNodeList.Clear();
        }

        private void OnDisable()
        {
            Debug.Log("On Disable");
            Save();
        }
        private void OnEnable()
        {
            Debug.Log("On Enable");
            Load();
        }

        //private GraphNode<RoadNode> Deserialze(SerializedRoadNode sz_roadNode)
        //{
        //    return new GraphNode<RoadNode>(sz_roadNode.RoadNode);
        //}
    }

    [Serializable]
    public class SerializableRoadNode
    {
        public RoadNode RoadNode;
        public List<SerializableRoadEdge> Connections = new List<SerializableRoadEdge>();

    }
    [Serializable]
    public class SerializableRoadEdge
    {
        public RoadNode FromNode;
        public RoadNode ToNode;
        public float Cost;
    }

}

