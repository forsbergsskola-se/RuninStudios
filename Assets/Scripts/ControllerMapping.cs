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

    [SerializeField] private NotePad notePad;
    public void buttonPressRed()
    {
        notePad.NotePadPressedRed();
    }
    public void buttonPressBlue()
    {
        notePad.NotePadPressedBlue();
    }
    public void buttonPressGreen()
    {
        notePad.NotePadPressedGreen();
    }
}
