/*
  CarSpawner.cs
  RoadsPrototype
  Created by Rohun Banerji on June 26, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RoadsEditor;

namespace RoadsPrototype
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField]
        private TrafficCar carPrefab;
        [SerializeField]
        private RoadNode startNode;
        [SerializeField]
        private RoadNode destinationNode;

        private RoadGraphDatabase roadGraphDB;

        private void SpawnCar()
        {
            Debug.Log("Spawn Car");
            var carClone = (TrafficCar)(Instantiate(carPrefab));
        }

        private void Awake()
        {
            Assert.IsNotNull(carPrefab);
            Assert.IsNotNull(startNode);
            Assert.IsNotNull(destinationNode);
            roadGraphDB = FindObjectOfType<RoadGraphDatabase>();
            Assert.IsNotNull(roadGraphDB);
        }

        private void Start()
        {
            InputManager.Instance.RegisterKeysDown((key) => SpawnCar(), KeyCode.Space);
        }

    }
}

