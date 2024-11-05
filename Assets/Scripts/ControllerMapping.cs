using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerMapping : MonoBehaviour
{
    
    [SerializeField] private NoteReceiver noteReciever;
    public void buttonPressRed()
    {
        noteReciever.NotePadPressedRed();
    }
    public void buttonPressBlue()
    {
        noteReciever.NotePadPressedBlue();
    }
    public void buttonPressGreen()
    {
        noteReciever.NotePadPressedGreen();
    }
}
