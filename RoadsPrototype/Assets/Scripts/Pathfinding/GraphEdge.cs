/*
  GraphEdge.cs
  RoadsPrototype
  Created by Rohun Banerji on June 07, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections;
using System.Collections.Generic;

namespace Pathfinding
{
    public class GraphEdge<T>
    {
        public float Cost { get; private set; }
        public GraphNode<T> FromNode { get; private set; }
        public GraphNode<T> ToNode { get; private set; }

        public GraphEdge()
            : this(null, null, default(float))
        { }

        public GraphEdge(GraphNode<T> fromNode, GraphNode<T> toNode)
            : this(fromNode, toNode, default(float))
        { }

        public GraphEdge(GraphNode<T> fromNode, GraphNode<T> toNode, float cost)
        {
            this.FromNode = fromNode;
            this.ToNode = toNode;
            this.Cost = cost;
        }

        public bool IsValidEdge()
        {
            return FromNode != null || ToNode != null;
        }

        public override string ToString()
        {
            return "From: " + FromNode.ToString() + " To " + ToNode.ToString() + " Cost " + Cost;
        }
    }
}
