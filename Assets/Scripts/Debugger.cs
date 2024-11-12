using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static TMP_Text redButton;
    public static TMP_Text redFunction;
    
    public static TMP_Text blueButton;
    public static TMP_Text blueFunction;
    
    public static TMP_Text greenButton;
    public static TMP_Text greenFunction;
    
    
    static void debugButtonInput(string button, float timer) // Check timing on button pressed
    {
        switch (button)
        {
            case "RED":
                redButton.text = timer.ToString();
                break;
            case "BLUE":
                blueButton.text = timer.ToString();
                break;
            case "GREEN":
                greenButton.text = timer.ToString();
                break;
        }
    }
}
