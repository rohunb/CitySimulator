/*
  Graph.cs
  RoadsPrototype
  Created by Rohun Banerji on June 06, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataStructures
{
    public class Graph<NodeValueType>
    {
        private List<GraphNode<NodeValueType>> nodes = new List<GraphNode<NodeValueType>>();

        public ReadOnlyCollection<GraphNode<NodeValueType>> Nodes
        {
            get { return nodes.AsReadOnly(); }
        }

        public GraphNode<NodeValueType> AddNode(NodeValueType value)
        {
            GraphNode<NodeValueType> node = new GraphNode<NodeValueType>(value);
            nodes.Add(node);
            return node;
        }
        public GraphNode<NodeValueType> FindNodeByValue(NodeValueType value)
        {
            return nodes.Find(n => n.Value.Equals(value));
        }
        public void AddEdge(GraphNode<NodeValueType> node1, GraphNode<NodeValueType> node2)
        {

        }
        public void AddEdge(NodeValueType value1, NodeValueType value2)
        {

        }
    }
}
