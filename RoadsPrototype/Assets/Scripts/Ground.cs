/*
  Ground.cs
  RoadsPrototype
  Created by Rohun Banerji on June 28, 2015.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IMoveHandler
{
    public delegate void GroundClick(Vector3 position);
    public delegate void GroundUp();
    public delegate void GroundEnter(Vector3 position);
    public delegate void GroundExit();

    public event GroundClick OnGroundClick = new GroundClick((Vector3) => { });
    public event GroundUp OnGroundUp = new GroundUp(() => { });
    public event GroundEnter OnGroundEnter = new GroundEnter((Vector3) => { });
    public event GroundExit OnGroundExit = new GroundExit(() => { });

    public void OnPointerClick(PointerEventData eventData)
    {
        OnGroundClick(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnGroundUp();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnGroundEnter(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnGroundExit();
    }

    public bool GetMousePositionOnGround(out Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 8))
        {
            position = hit.point;
            return true;
        }
        else
        {
            position = Vector3.zero;
            return false;
        }
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("On Move");
    }
}

