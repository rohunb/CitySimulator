using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class RoadGeneratorInput : MonoBehaviour
{
    [SerializeField]
    private GroundInput groundInput;

    private bool gotFirstClick = false;
    private Vector3 startPos;

    public delegate void CreateRoadInputReceived(Vector3 startPos, Vector3 endPos);
    public event CreateRoadInputReceived OnCreateRoadInputReceived = new CreateRoadInputReceived((Vector3 startPos, Vector3 endPos) => {});

    private void Start()
    {
        Assert.IsNotNull(groundInput);
        //groundInput.OnGroundPointerClick += GroundPointerClick;
        groundInput.OnGroundPointerDown += GroundPointerDown;
        groundInput.OnGroundPointerUp += GroundPointerUp;
    }

    private void GroundPointerDown(Vector3 position)
    {
        gotFirstClick = true;
        startPos = position;
    }

    private void GroundPointerClick(Vector3 position)
    {
        gotFirstClick = true;
        startPos = position;
    }

    private void GroundPointerUp(Vector3 position)
    {
        if (gotFirstClick)
        {
            OnCreateRoadInputReceived(startPos, position);
            gotFirstClick = false;
        }
    }

}
