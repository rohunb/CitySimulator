/*
  RoadSystem.cs
  RoadsPrototype
  Created by Rohun Banerji on June 15, 2015.
*/

using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

namespace RoadsPrototype
{
    public class RoadSystem : MonoBehaviour
    {
        //Private Members
        private Graph<RoadNode> roadGraph = new Graph<RoadNode>();

        //Unity Callbacks
        private void Awake()
        {
            Assert.raiseExceptions = true;
        }
        private void Start()
        {
            GraphNode<RoadNode> botLeftNode
                = new GraphNode<RoadNode>(0, new RoadNode(Vector3.zero));
            GraphNode<RoadNode> botCenterNode
                = new GraphNode<RoadNode>(1, new RoadNode(new Vector3(10.0f, 0.0f, 0.0f)));
            GraphNode<RoadNode> topCenterNode
                = new GraphNode<RoadNode>(2, new RoadNode(new Vector3(10.0f, 0.0f, 10.0f)));
            GraphNode<RoadNode> topRightNode
                = new GraphNode<RoadNode>(3, new RoadNode(new Vector3(20.0f, 0.0f, 10.0f)));

            //roadGraph.AddAndConnect(botLeftNode, botCenterNode, 1.0f);
            //botCenterNode.AddNeighbour(topCenterNode, 15.0f);
            //roadGraph.AddAndConnect(topCenterNode, topRightNode, 20.0f);

            roadGraph.AddAndConnect(botLeftNode, topCenterNode, 10.0f);
            roadGraph.AddAndConnect(topCenterNode, botCenterNode, 60.0f);
            roadGraph.AddAndConnect(topCenterNode, topRightNode, 10.0f);
            roadGraph.AddAndConnect(topRightNode, botCenterNode, 10.0f);

            Func<GraphNode<RoadNode>, GraphNode<RoadNode>, float> heuristicCalc 
                = (node1, node2) => 
                {
                  return Vector3.Distance(node1.Value.Position, node2.Value.Position)  ;
                };

            AStarCalculator<RoadNode> aStar = new AStarCalculator<RoadNode>(roadGraph, heuristicCalc);

            //List<GraphEdge<RoadNode>> path = aStar.CalculatePath(botLeftNode, botCenterNode);
            List<GraphNode<RoadNode>> path = aStar.CalculatePath(botLeftNode, botCenterNode);

            Assert.IsTrue(path.Count > 0);
            //Debug.Log(path[0].FromNode);

            //foreach (var edge in path)
            //{
            //    Debug.Log(edge.ToNode);
            //}

            foreach (var node in path)
            {
                Debug.Log(node);
            }

        }
    }
}

