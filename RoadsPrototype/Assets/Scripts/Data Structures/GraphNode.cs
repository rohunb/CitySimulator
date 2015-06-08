/*
  GraphNode.cs
  RoadsPrototype
  Created by Rohun Banerji on June 06, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataStructures
{
    public class GraphNode<ValueType>
    {
        public ValueType Value { get; private set;}

        private List<GraphNode<ValueType>> neighbours = new List<GraphNode<ValueType>>();
        public ReadOnlyCollection<GraphNode<ValueType>> Neighbours
        {
            get { return neighbours.AsReadOnly(); }
        }

        public GraphNode(ValueType value)
        {
            this.Value = value;
        }

    }
}
