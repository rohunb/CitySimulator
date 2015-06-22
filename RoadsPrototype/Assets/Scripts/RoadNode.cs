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
    public class RoadNode : MonoBehaviour
    {
        [SerializeField]
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            private set
            {
                transform.position = value;
            }
        }

        public RoadNode(Vector3 position)
        {
            this.Position = position;
        }

        public override string ToString()
        {
            return "RoadNode: ID: " + ID + " Position " + Position.ToString();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            //Gizmos.DrawIcon(Position, "NodeIcon.tiff");
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, .1f);
        }
#endif
    }
}

