using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscBehavior : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 startTouchPosition; 
    private bool isTouchingButton = false;
    private int scratchCount;
    private bool lastDirectionWasUp = false; // Track latest direction
    [SerializeField] private ControllerMapping controllerMapping;
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouchingButton = true;
        startTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData) 
    { 
        isTouchingButton = false;
        lastDirectionWasUp = false; // Reset direction
        scratchCount = 0;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isTouchingButton)
        {
            Vector2 currentTouchPosition = eventData.position;
            float deltaY = currentTouchPosition.y - startTouchPosition.y;
            if (Mathf.Abs(deltaY) > 20) // Threshold for sensitivity
            {
                if (deltaY > 0 && !lastDirectionWasUp)
                {
                    Debug.Log("Dragging up!");
                    OnDragUp();
                    lastDirectionWasUp = true; // Sets direction up
                }
                else if (deltaY < 0 && lastDirectionWasUp)
                {
                    Debug.Log("Dragging down!");
                    OnDragDown();
                    lastDirectionWasUp = false; // Sets direction down
                }
                startTouchPosition = currentTouchPosition;
            }
        }

        if (scratchCount >= 4)
        {
            Debug.Log("Full Scratch!");
            controllerMapping.Scratch();
        }
    }

    private void OnDragUp()
    {
        Debug.Log("User dragged up on the disc button!");
        scratchCount++;
    }

    private void OnDragDown()
    {
        Debug.Log("User dragged down on the disc button!");
        scratchCount++;
    }

}