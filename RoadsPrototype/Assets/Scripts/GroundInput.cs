using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GroundInput : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    public delegate void GroundPointerClick(Vector3 position);
    public delegate void GroundPointerDown(Vector3 position);
    public delegate void GroundPointerUp(Vector3 position);

    public event GroundPointerClick OnGroundPointerClick = new GroundPointerClick((Vector3) => { });
    public event GroundPointerDown OnGroundPointerDown = new GroundPointerDown((Vector3) => { });
    public event GroundPointerUp OnGroundPointerUp = new GroundPointerUp((Vector3) => { });

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnGroundPointerClick");
        OnGroundPointerClick(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnGroundPointerDown");
        OnGroundPointerDown(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnGroundPointerUp");
        OnGroundPointerUp(eventData.pointerCurrentRaycast.worldPosition);
    }

    
}
