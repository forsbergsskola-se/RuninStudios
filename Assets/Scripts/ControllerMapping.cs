using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerMapping : MonoBehaviour
{
    #region GameplayMapping
    public Button red;
    public Button blue;
    public Button green;
    #endregion

    public void buttonPress()
    {
        Debug.Log("Button Pressed!");
    }
}
