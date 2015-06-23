/*
  RoadGraphDatabase.cs
  RoadsPrototype
  Created by Rohun Banerji on June 21, 2015.
*/

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using RoadsPrototype;

namespace RoadsEditor
{
    [CreateAssetMenuAttribute]
    public class RoadGraphDatabase : ScriptableObject
    {
        [SerializeField]
        private RoadNode roadNodePrefab;
        public RoadNode RoadNodePrefab
        {
            get { return roadNodePrefab; }
        }

        [SerializeField]
        private List<GraphNode<RoadNode>> nodeList = new List<GraphNode<RoadNode>>();
        public List<GraphNode<RoadNode>> NodeList
        {
            get { return nodeList; }
        }

        //non-serialized data - used at runtime
        //converted to the list since that's what Unity can serialize
        private Graph<RoadNode> roadGraph = new Graph<RoadNode>();
        public Graph<RoadNode> RoadGraph
        {
            get { return roadGraph; }
        }

        //GUI interface
        public void AddConnection(RoadNode fromNode, RoadNode toNode, float cost, bool directed)
        {
            Assert.IsNotNull(fromNode);
            Assert.IsNotNull(toNode);
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
        }

        /// <summary>
        /// Converts the graph to a format that Unity can serialize (List)
        /// </summary>
        public void Save()
        {
            
        }
        
        public void Clear()
        {
            Assert.IsNotNull(roadGraph);
            roadGraph.Clear();
        }
    }
}
