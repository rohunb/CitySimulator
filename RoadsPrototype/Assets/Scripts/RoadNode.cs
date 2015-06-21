/*
  RoadNode.cs
  RoadsPrototype
  Created by Rohun Banerji on June 15, 2015.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RoadsPrototype
{
    public struct RoadNode
    {
        public Vector3 Position { get; private set; }

        public RoadNode(Vector3 position)
        {
            this.Position = position;
        }

        public override string ToString()
        {
            return "RoadNode: Position " + Position.ToString();
        }
    }
}

