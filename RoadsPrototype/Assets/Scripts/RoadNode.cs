/*
  RoadNode.cs
  RoadsPrototype
  Created by Rohun Banerji on June 15, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace RoadsPrototype
{
    public class RoadNode : MonoBehaviour
    {
        [SerializeField]
        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                uiText.text = id.ToString();
            }
        }
        [SerializeField]
        private Text uiText;

        public Vector3 Position
        {
            get
            {
                Assert.IsNotNull(gameObject);
                Assert.IsNotNull(transform);
                return transform.position;
            }
            private set
            {
                Assert.IsNotNull(gameObject);
                Assert.IsNotNull(transform);
                transform.position = value;
            }
        }

        public override string ToString()
        {
            return "RoadNode: ID: " + ID + " Position " + Position.ToString();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Position, .1f);
        }
#endif
    }
}

