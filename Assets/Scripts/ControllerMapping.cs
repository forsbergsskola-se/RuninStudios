using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ControllerMapping : MonoBehaviour
{
    [SerializeField] private NoteReceiver noteReciever;
    
//  -----Button Functions-----
    public void buttonPressRed() { noteReciever.NotePadPressedRed(); }
    public void buttonPressBlue() { noteReciever.NotePadPressedBlue(); }
    public void buttonPressGreen() { noteReciever.NotePadPressedGreen(); }
//  -------------------------

    public void Scratch()
    {
        noteReciever.NotePadScratch();
    }
}
