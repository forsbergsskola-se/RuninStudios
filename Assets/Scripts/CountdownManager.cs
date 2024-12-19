using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText; 
    public AudioSource metronomeSound; 
    public SongManger2 songManager; 
    public float countdownTime = 3.0f; 

    private float timer;
    private int lastDisplayedNumber;

    private void Start()
    {
        RestartCountdown();
    }

    public void RestartCountdown()
    {
        timer = countdownTime;
        lastDisplayedNumber = Mathf.CeilToInt(countdownTime);

        // Disable CvsConverter functionality initially
        songManager.enabled = false;

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        Time.timeScale = 1f;
        while (timer > 0)
        {
            Debug.Log(timer);
            int currentNumber = Mathf.CeilToInt(timer);
            if (currentNumber != lastDisplayedNumber)
            {
                // Update displayed text and play the metronome sound
                countdownText.text = currentNumber.ToString();
                metronomeSound.Play();
                lastDisplayedNumber = currentNumber;
            }

            // Decrease timer
            timer -= Time.deltaTime;
            yield return null;
        }

        // Display "GO!" for a brief moment
        countdownText.text = "";
        songManager.StartNoteSpawns();
        StartCvsConverter();

        yield return new WaitForSeconds(1f);

        Debug.Log("countdown completed...");
        // Start the game
        countdownText.gameObject.SetActive(false);
    }

    private void StartCvsConverter()
    {
        // Enable CvsConverter script and allow it to start
        songManager.enabled = true;

        // Optionally, call Start() manually if needed
        songManager.Invoke("SongStart", 0f);
    }
    
}

