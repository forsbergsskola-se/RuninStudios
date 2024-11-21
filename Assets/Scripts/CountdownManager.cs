using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public TMP_Text countdownText; 
    public AudioSource metronomeSound; 
    public SongManger2 songManager; 
    public float countdownTime = 3f; 

    private float timer;
    private int lastDisplayedNumber;

    private void Start()
    {
        timer = countdownTime;
        lastDisplayedNumber = Mathf.CeilToInt(countdownTime);

        // Disable CvsConverter functionality initially
        songManager.enabled = false;

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (timer > 0)
        {
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
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        // Start the game
        countdownText.gameObject.SetActive(false);
        StartCvsConverter();
    }

    private void StartCvsConverter()
    {
        // Enable CvsConverter script and allow it to start
        songManager.enabled = true;

        // Optionally, call Start() manually if needed
        songManager.Invoke("SongStart", 0f);
    }
}

