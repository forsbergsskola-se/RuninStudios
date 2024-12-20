using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteReceiver : MonoBehaviour
{
    private static Note redNote;
    private static Note blueNote;
    private static Note greenNote;
    private static Note discNote;

    [SerializeField] private GameObject redTile;
    [SerializeField] private GameObject blueTile;
    [SerializeField] private GameObject greenTile;
    
    [SerializeField] private ScoreManager scoreManager;
    
    //---Particle effects----
    [SerializeField] private ParticleSystem redSuccess;
    [SerializeField] private ParticleSystem blueSuccess;
    [SerializeField] private ParticleSystem greenSuccess;
    [SerializeField] private ParticleSystem scratchSuccess;

    [SerializeField] private Material baseMaterialRed;
    private Color originalColorRed;
    
    [SerializeField] private Material baseMaterialBlue;
    private Color originalColorBlue;
    
    [SerializeField] private Material baseMaterialGreen;
    private Color originalColorGreen;
    
    [SerializeField] private Material baseMaterialYellow;
    private Color originalColorYellow;
    
    private bool isTransitioning;
    private float duration = 0.1f;
    private void Start()
    {
        originalColorRed = baseMaterialRed.color; // Store the original color
        originalColorBlue = baseMaterialBlue.color;
        originalColorGreen = baseMaterialGreen.color;
        originalColorYellow = baseMaterialYellow.color;
    }
    
    IEnumerator ChangeColorOnce(Color targetColor, Color originalColor, Material baseMaterial)
    {
        isTransitioning = true;

        // Transition to the target color
        yield return StartCoroutine(LerpColor(originalColor, targetColor, duration, baseMaterial));

        // Transition back to the original color
        yield return StartCoroutine(LerpColor(targetColor, originalColor, duration, baseMaterial));

        isTransitioning = false;
    }

    IEnumerator LerpColor(Color fromColor, Color toColor, float duration, Material baseMaterial)
    {
        float time = 0f;
        while (time < duration)
        {
            baseMaterial.color = Color.Lerp(fromColor, toColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        baseMaterial.color = toColor; // Ensure the final color is set
    }

    public void TriggerFlip(Note note, bool state)
    {
        note.isOnTrigger = state;
        if (state)
        {
            switch (note.ID)
            {
                case "RED":
                    redNote = note;
                    redNote.isOnTrigger = true;
                    break;
                case "BLUE":
                    blueNote = note;
                    blueNote.isOnTrigger = true;
                    break;
                case "GREEN":
                    greenNote = note;
                    greenNote.isOnTrigger = true;
                    break;
                case "DISC":
                    discNote = note;
                    break;
            }
        }
        else
        {
            switch(note.ID)
            {
                case "RED":
                    redNote = null;
                    break;
                case "BLUE":
                    blueNote = null;
                    break;
                case "GREEN":
                    greenNote = null;
                    break;
                case "DISC":
                    discNote = null;
                    break;
            }
        }
    }

    public void NotePadPressedRed()
    {
        StartCoroutine(RedRoutine());
    }

    private IEnumerator RedRoutine()
    {
        var initialPosition = redTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        redTile.transform.position = pressedPosition;
        if (!isTransitioning)
        {
            StartCoroutine(ChangeColorOnce(Color.red, originalColorRed, baseMaterialRed));
        }
        try
        {
            if (redNote != null && redNote.isOnTrigger)
            {
                Debug.Log("Red Success");
                
                scoreManager.AddScore(redNote.scoreValue);
                
                // Reset the particle system by re-enabling its GameObject
                redSuccess.gameObject.SetActive(false);
                redSuccess.gameObject.SetActive(true);

                redSuccess.Play();
                redNote.gameObject.SetActive(false); // Disable the red note GameObject
                redNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
        yield return new WaitForSeconds(0.1f);
        redTile.transform.position = initialPosition;
    }

    public void NotePadPressedBlue()
    {
        StartCoroutine(BlueRoutine());
    }

    private IEnumerator BlueRoutine()
    {
        var initialPosition = blueTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        blueTile.transform.position = pressedPosition;
        if (!isTransitioning)
        {
            StartCoroutine(ChangeColorOnce(Color.blue, originalColorBlue, baseMaterialBlue));
        }
        try
        {
            if (blueNote != null && blueNote.isOnTrigger)
            {
                Debug.Log("Blue Success");

                scoreManager.AddScore(blueNote.scoreValue);
                
                blueSuccess.gameObject.SetActive(false);
                blueSuccess.gameObject.SetActive(true);

                blueSuccess.Play();
                blueNote.gameObject.SetActive(false); // Disable the blue note GameObject
                blueNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!");
        }
        yield return new WaitForSeconds(0.1f);
        blueTile.transform.position = initialPosition;
    }

    public void NotePadPressedGreen()
    {
        StartCoroutine(GreenRoutine());
    }

    private IEnumerator GreenRoutine()
    {
        var initialPosition = greenTile.transform.position;
        var pressedPosition = initialPosition;
        pressedPosition.y -= 0.3f;
        greenTile.transform.position = pressedPosition;
        if (!isTransitioning)
        {
            StartCoroutine(ChangeColorOnce(Color.green, originalColorGreen, baseMaterialGreen));
        }
        try
        {
            if (greenNote != null && greenNote.isOnTrigger)
            {
                Debug.Log("Green Success");
                
                scoreManager.AddScore(greenNote.scoreValue);
                
                greenSuccess.gameObject.SetActive(false);
                greenSuccess.gameObject.SetActive(true);

                greenSuccess.Play();
                greenNote.gameObject.SetActive(false); // Disable the green note GameObject
                greenNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }

        yield return new WaitForSeconds(0.1f);
        greenTile.transform.position = initialPosition;
    }

    public void NotePadScratch()
    {
        StartCoroutine(DiscRoutine());
    }
    private IEnumerator DiscRoutine()
    {
        if (!isTransitioning)
        {
            StartCoroutine(ChangeColorOnce(Color.yellow, originalColorYellow, baseMaterialYellow));
        }
        try
        {
            if (discNote != null && discNote.GetComponent<Note>().isOnTrigger)
            {
                scoreManager.AddScore(discNote.scoreValue);
                
                discNote.gameObject.SetActive(false);
                discNote.gameObject.SetActive(true);

                scratchSuccess.Play();
                discNote.gameObject.SetActive(false); // Disable the disc note GameObject
                discNote = null;
            }
        }
        catch 
        { 
            Debug.Log("MISSED NOTE!"); 
        }
        yield return new WaitForSeconds(0.1f);
    }
}
