/*
  GraphNode.cs
  RoadsPrototype
  Created by Rohun Banerji on June 06, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.Assertions;

namespace Pathfinding
{
    public class GraphNode<T>
    {
        //public int ID { get; set; }
        public T Value { get; private set;}

        private List<GraphEdge<T>> connections = new List<GraphEdge<T>>();
        public ReadOnlyCollection<GraphEdge<T>> Connections
        {
            get { return connections.AsReadOnly(); }
        }

        //public GraphNode(int ID, T value)
        //{
        //    this.ID = ID;
        //    this.Value = value;
        //}
        public GraphNode(T value)
        {
            this.Value = value;
        }

        ///// <summary>
        ///// overloaded equality to test for IDs only - validation should happen in Graph to make sure duplicate IDs are not inserted
        ///// </summary>
        ///// <param name="lhs"></param>
        ///// <param name="rhs"></param>
        ///// <returns></returns>
        //public static bool operator == (GraphNode<T> lhs, GraphNode<T> rhs)
        //{
        //    return lhs.ID == rhs.ID;
        //}
        ///// <summary>
        ///// overloaded equality to test for IDs only - validation should happen in Graph to make sure duplicate IDs are not inserted 
        ///// </summary>
        ///// <param name="lhs"></param>
        ///// <param name="rhs"></param>
        ///// <returns></returns>
        //public static bool operator !=(GraphNode<T> lhs, GraphNode<T> rhs)
        //{
        //    return lhs.ID != rhs.ID;
        //}

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
        
        public void AddNeighbour(GraphNode<T> toNode, float cost)
        {
            Assert.IsFalse(this == toNode);
            Assert.IsFalse(IsConnectedTo(toNode));
            connections.Add(new GraphEdge<T>(this, toNode, cost));
        }

        public bool IsConnectedTo(GraphNode<T> toNode)
        {
            Assert.IsFalse(this == toNode);
            return connections.Any(connection => connection.ToNode == toNode);
        }

        public void RemoveConnectionTo(GraphNode<T> toNode)
        {
            Assert.IsTrue(IsConnectedTo(toNode));
            connections.Remove(GetConnectionTo(toNode));
        }

        public GraphEdge<T> GetConnectionTo(GraphNode<T> toNode)
        {
            Assert.IsTrue(IsConnectedTo(toNode));
            return connections.Single(connection => connection.ToNode == toNode);
        }

        public override string ToString()
        {
            return "GraphNode: " + Value.ToString();
        }
    }
}
