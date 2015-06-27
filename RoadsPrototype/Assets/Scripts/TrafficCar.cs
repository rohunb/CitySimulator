/*
  TrafficCar.cs
  RoadsPrototype
  Created by Rohun Banerji on June 26, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RoadsPrototype
{
    public class TrafficCar : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10.0f;
        [SerializeField]
        private float epsilon = 0.2f;

        private Coroutine driveCoroutine;
        private Coroutine drivePathCoroutine;

        public void DriveTo(Vector3 destination)
        {
            if (driveCoroutine != null)
            {
                StopCoroutine(driveCoroutine);
            }
            driveCoroutine = StartCoroutine(DriveToDestination(destination));
        }

        public void DriveAlongPath(IEnumerable<RoadNode> path)
        {
            if (drivePathCoroutine != null)
            {
                StopCoroutine(drivePathCoroutine);
            }
            drivePathCoroutine = StartCoroutine(DriveAlongPathCoroutine(path));
        }

        private IEnumerator DriveAlongPathCoroutine(IEnumerable<RoadNode> path)
        {
            foreach (var node in path)
            {
                yield return StartCoroutine(DriveToDestination(node.Position));
            }
        }

        private IEnumerator DriveToDestination(Vector3 destination)
        {
            Vector3 direction = destination - transform.position;
            float distance = direction.magnitude;
            transform.LookAt(destination);
            while (distance > epsilon)
            {
                Vector3 normalizedDir = direction / distance;
                //transform.Translate(normalizedDir * Time.deltaTime * speed);
                transform.position += normalizedDir * Time.deltaTime * speed;
                direction = destination - transform.position;
                distance = direction.magnitude;
                yield return null;
            }
        }
    }
}