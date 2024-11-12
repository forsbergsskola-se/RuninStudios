using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscBehavior : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 startTouchPosition; 
    private bool isTouchingButton = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouchingButton = true;
        startTouchPosition = eventData.position;
    }
    public void OnPointerUp(PointerEventData eventData) { isTouchingButton = false;    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isTouchingButton)
        {
            Vector2 currentTouchPosition = eventData.position;
            float deltaY = currentTouchPosition.y - startTouchPosition.y;
            if (Mathf.Abs(deltaY) > 20) // //Threshold for sensitivity
            {
                if (deltaY > 0)
                {
                    Debug.Log("Dragging up!");
                    OnDragUp();
                }
                else
                {
                    Debug.Log("Dragging down!");
                    OnDragDown();
                }
                startTouchPosition = currentTouchPosition;
            }
        }
    }
    private void OnDragUp()
    {
        Debug.Log("User dragged up on the disc button!");
    }

    private void OnDragDown()
    {
        Debug.Log("User dragged down on the disc button!");
    }
}
