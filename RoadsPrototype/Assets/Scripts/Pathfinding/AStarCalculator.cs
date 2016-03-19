/*
  AStar.cs
  RoadsPrototype
  Created by Rohun Banerji on June 18, 2015.
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pathfinding
{
    public class AStarCalculator<T>
    {
        //Fields
        private Graph<T> graph;
        private Func<GraphNode<T>, GraphNode<T>, float> heuristicCalculator;
        //private List<GraphEdge<T>> path = new List<GraphEdge<T>>();
        private List<GraphNode<T>> path = new List<GraphNode<T>>();
        private Dictionary<GraphNode<T>, float> costSoFar = new Dictionary<GraphNode<T>, float>();
        private Dictionary<GraphNode<T>, GraphNode<T>> cameFrom = new Dictionary<GraphNode<T>, GraphNode<T>>();
        private PriorityQueue<float, GraphNode<T>> openQueue = new PriorityQueue<float, GraphNode<T>>();

        /// <summary>
        /// Pass in the method used to calculate the heuristic
        /// </summary>
        /// <param name="heuristicCalculator"></param>
        public AStarCalculator(Graph<T> graph, Func<GraphNode<T>, GraphNode<T>, float> heuristicCalculator)
        {
            Assert.IsNotNull(graph);
            Assert.IsNotNull(heuristicCalculator);
            this.graph = graph;
            this.heuristicCalculator = heuristicCalculator;
        }

        /// <summary>
        /// Returns a list of edges leading from the start to end. Returns an empty list if no path is found.
        /// </summary>
        public List<GraphNode<T>> CalculatePath(GraphNode<T> startNode, GraphNode<T> destinationNode)
        {
            Assert.IsNotNull(graph);
            Assert.IsNotNull(startNode);
            Assert.IsNotNull(destinationNode);
            Assert.IsTrue(graph.ContainsNode(startNode));
            Assert.IsTrue(graph.ContainsNode(destinationNode));

            Reset();
            openQueue.Enqueue(0.0f, startNode);

            //cameFrom.Add(startNode, startNode);
            //path.Add(startNode);
            //closed
            costSoFar.Add(startNode, 0.0f);

            while(!openQueue.IsEmpty)
            {
                var currentNode = openQueue.Dequeue();
                Assert.IsNotNull(currentNode);
                Debug.Log("Current Node: " + currentNode);

                //reached goal
                if(currentNode == destinationNode) break;

                //loop through connections of current node
                foreach (var connection in currentNode.Connections)
                {
                    Debug.Log("Checking connection: " + connection);
                    float newCost = costSoFar[currentNode] + connection.Cost;
                    var nextNode = connection.ToNode;
                    Assert.IsNotNull(nextNode);

                    if (!costSoFar.ContainsKey(nextNode) || newCost < costSoFar[nextNode])
                    {
                        //Assert.IsFalse(costSoFar.ContainsKey(nextNode));
                        //costSoFar.Add(nextNode, newCost);
                        costSoFar.AddOrReplace(nextNode, newCost);
                        float priority = newCost + heuristicCalculator(nextNode, destinationNode);
                        openQueue.Enqueue(priority, nextNode);
                        //Assert.IsFalse(cameFrom.ContainsKey(nextNode));
                        cameFrom.AddOrReplace(nextNode, currentNode);
                        //cameFrom.Add(nextNode, currentNode);
                        //path.Add(connection);
                        Debug.Log("Setting parent " + connection);
                        //path.Add(currentNode);
                    }//

                    //if (connection.ToNode == destinationNode) break;

                }//for connections
            }//while open not empty

            GeneratePath(startNode, destinationNode);

            //Assert.AreEqual(startNode, path[0].FromNode);
            //Assert.AreEqual(destinationNode, path[path.Count - 1].ToNode);
            Debug.Log("Start Node : " + startNode);
            Debug.Log("Destination: " + destinationNode);
            Assert.AreEqual(startNode, path[0]);
            Assert.AreEqual(destinationNode, path[path.Count - 1]);
            return path;

        }//CalculatePath

        /// <summary>
        /// Walk back through the parents (in the cameFrom dictionary) to generate the path
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="destinationNode"></param>
        void GeneratePath(GraphNode<T> startNode, GraphNode<T> destinationNode)
        {
            var currentNode = destinationNode;
            //path.
            while(cameFrom.ContainsKey(currentNode))
            {
                var childNode = cameFrom[currentNode];
                //var connection = currentNode.GetConnectionTo(childNode);
                //path.Add(connection);
                path.Add(currentNode);
                //Debug.Log("Adding path " + connection);
                Debug.Log("Adding path node " + currentNode);
                currentNode = childNode;
            }
            path.Add(startNode);
            path.Reverse();
        }

        private void Reset()
        {
            openQueue.Clear();
            path.Clear();
            costSoFar.Clear();
            cameFrom.Clear();
        }
    }
}

