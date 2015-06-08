/*
  GraphEdge.cs
  RoadsPrototype
  Created by Rohun Banerji on June 07, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class GraphEdge<NodeValueType>
    {
        public float Cost { get; private set; }
        public GraphNode<NodeValueType> FromNode { get; private set; }
        public GraphNode<NodeValueType> ToNode { get; private set; }

        public GraphEdge()
            : this(null, null, default(float))
        { }

        public GraphEdge(GraphNode<NodeValueType> fromNode, GraphNode<NodeValueType> toNode)
            : this(fromNode, toNode, default(float))
        { }

        public GraphEdge(GraphNode<NodeValueType> fromNode, GraphNode<NodeValueType> toNode, float cost)
        {
            this.FromNode = fromNode;
            this.ToNode = toNode;
            this.Cost = cost;
        }

    }
}
