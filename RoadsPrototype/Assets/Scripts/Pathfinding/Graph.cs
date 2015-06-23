/*
  Graph.cs
  RoadsPrototype
  Created by Rohun Banerji on June 06, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using UnityEngine;

namespace Pathfinding
{
    public class Graph<T> : IEnumerable
    {
        //Fields
        //private GraphNodeList<T> nodeList = new GraphNodeList<T>();
        //public GraphNodeList<T> NodeList
        //{
        //    get { return nodeList; }
        //}
        private List<GraphNode<T>> nodeList = new List<GraphNode<T>>();
        public List<GraphNode<T>> NodeList
        {
            get { return nodeList; }
        }

        //Ctor
        //public Graph()
        //{}

        //public Graph(GraphNodeList<T> nodeList)
        //{
        //    this.nodeList = nodeList;
        //}

        //Public Methods

        //Methods to Add to Graph
        public GraphNode<T> AddNode(T value)
        {
            //Assert.IsFalse(nodeList.Any(node => node.Value.Equals(value)));
            if (ContainsValue(value))
            {
                return nodeList.Single(node => node.Value.Equals(value));
            }
            else
            {
                GraphNode<T> graphNode = new GraphNode<T>(value);
                AddNode(graphNode);
                return graphNode;
            }
        }

        public void AddNode(GraphNode<T> node)
        {
            Assert.IsFalse(nodeList.Contains(node));
            if (!nodeList.Contains(node))
            {
                Debug.Log("Adding node " + node);
                nodeList.Add(node);
                Assert.IsTrue(nodeList.Contains(node));
            }
        }

        public void AddAndConnectDirected(GraphNode<T> fromNode, GraphNode<T> toNode, float cost)
        {
            Assert.IsFalse(fromNode == toNode);
            Assert.IsFalse(fromNode.Value.Equals(toNode.Value));
            AddNode(fromNode);
            AddNode(toNode);
            fromNode.AddNeighbour(toNode, cost);
        }

        public void AddAndConnect(GraphNode<T> node1, GraphNode<T> node2, float cost)
        {
            AddAndConnectDirected(node1, node2, cost);
            node2.AddNeighbour(node1, cost);
        }

        public void AddAndConnectDirected(T value1, T value2, float cost)
        {
            GraphNode<T> node1 = AddNode(value1);
            GraphNode<T> node2 = AddNode(value2);
            node1.AddNeighbour(node2, cost);
        }

        public void AddAndConnect(T value1, T value2, float cost)
        {
            GraphNode<T> node1 = AddNode(value1);
            GraphNode<T> node2 = AddNode(value2);
            node1.AddNeighbour(node2, cost);
            node2.AddNeighbour(node1, cost);
        }

        public void AddNodesInEdge(GraphEdge<T> edge)
        {
            AddAndConnectDirected(edge.FromNode, edge.ToNode, edge.Cost);
        }

        /// <summary>
        /// Potentially expensive operation since it will also search and remove connections for nodes connected to the node. After which the graph will search through and purge orphan nodes.
        /// </summary>
        /// <param name="nodeToRemove"></param>
        public void RemoveNode(GraphNode<T> nodeToRemove)
        {
            Assert.IsTrue(nodeList.Contains(nodeToRemove));

            //remove any connections to this node
            foreach (GraphNode<T> node in nodeList)
            {
                if (node.IsConnectedTo(nodeToRemove))
                {
                    node.RemoveConnectionTo(nodeToRemove);
                }
            }
            //might end up with orphan nodes...run clean up
            RemoveOrphanNodes();
        }

        //Search Graph
        //public GraphNode<T> FindNodeByID(int ID)
        //{
        //    return nodeList[ID];
        //}

        public bool ContainsNode(GraphNode<T> node)
        {
            return nodeList.Contains(node);
        }

        public bool ContainsValue(T value)
        {
            return nodeList.Any(node => node.Value.Equals(value));
        }

        public void Clear()
        {
            nodeList.Clear();
        }

        /// <summary>
        /// Removes any orphan Nodes
        /// </summary>
        private void RemoveOrphanNodes()
        {
            var orphanNodes = new List<GraphNode<T>>();
            foreach (GraphNode<T> node in nodeList)
            {
                if (node.Connections.Count == 0)
                {
                    orphanNodes.Add(node);
                }
            }
            foreach (var node in orphanNodes)
            {
                nodeList.Remove(node);
            }
        }

        // Enables foreach
        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodeList.GetEnumerator();
        }
    }
}
