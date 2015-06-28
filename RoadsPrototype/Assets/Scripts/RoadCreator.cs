/*
  RoadCreator.cs
  RoadsPrototype
  Created by Rohun Banerji on June 28, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class RoadCreator : MonoBehaviour 
{
    [SerializeField]
    private Button roadCreationButton;
    [SerializeField]
    private Ground ground;
    [SerializeField]
    private LineRenderer roadLinePrefab;

    private LineRenderer currentRoadLine;
    private bool drawingRoad;

    //button callback
    public void StartRoadCreation()
    {
        roadCreationButton.enabled = false;
        RegisterGroundInputs();
        StartCoroutine(WaitingToStartRoadRoutine());
    }

    public void StopRoadCreation()
    {
        DeRegisterGroundInputs();
        roadCreationButton.enabled = true;
    }

    private IEnumerator WaitingToStartRoadRoutine()
    {
        Assert.IsFalse(drawingRoad);
        
        while(!drawingRoad)
        {
            Debug.Log("Waiting to start");
            yield return null;
        }
        yield return StartCoroutine(CreateRoadRoutine());
    }

    private IEnumerator CreateRoadRoutine()
    {
        while (drawingRoad)
        {
            Vector3 groundPos;
            if(ground.GetMousePositionOnGround(out groundPos))
            {
                Debug.Log("Ground pos " + groundPos);
                SetRoadEnd(groundPos);
            }
            else
            {
                Debug.Log("Not on ground");
            }
            yield return null;
        }
    }

    private void StartDrawingRoad(Vector3 position)
    {
        currentRoadLine = (LineRenderer)(Instantiate(roadLinePrefab));
        Assert.IsNotNull(currentRoadLine);
        currentRoadLine.SetPosition(0, position);
        currentRoadLine.SetPosition(1, position);
        ground.OnGroundClick -= StartDrawingRoad;
        ground.OnGroundClick += EndDrawingRoad;
        Assert.IsFalse(drawingRoad);
        drawingRoad = true;
    }

    private void EndDrawingRoad(Vector3 position)
    {
        SetRoadEnd(position);
        drawingRoad = false;
    }
    private void CancelDrawingRoad()
    {
        Vector3 endPosition;
        if(ground.GetMousePositionOnGround(out endPosition))
        {
            EndDrawingRoad(endPosition);
        }
        else
        {
            drawingRoad = false;
        }
    }
    private void SetRoadEnd(Vector3 position)
    {
        Assert.IsTrue(drawingRoad);
        Assert.IsNotNull(currentRoadLine);
        currentRoadLine.SetPosition(1, position);
    }

    private void RegisterGroundInputs()
    {
        ground.OnGroundClick += StartDrawingRoad;
        InputManager.Instance.RegisterKeysDown((key) => CancelDrawingRoad(), KeyCode.Escape);
    }
    private void DeRegisterGroundInputs()
    {
        ground.OnGroundClick -= StartDrawingRoad;
        ground.OnGroundClick -= EndDrawingRoad;
    }

    private void Awake()
    {
        Assert.IsNotNull(roadCreationButton);
        Assert.IsNotNull(ground);
        Assert.IsNotNull(roadLinePrefab);
    }
    private void Start()
    {
        InputManager.Instance.RegisterKeysDown((key) => StopRoadCreation(), KeyCode.Escape);
    }
}

