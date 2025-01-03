using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Input soundtracks + respective csv file
    public AudioClip SongOne;
    public TextAsset csvFileOne;
    
    public AudioClip SongTwo;
    public TextAsset csvFileTwo;
    
    public AudioClip SongThree;
    public TextAsset csvFileThree;
    
    [SerializeField] Animator transitionanimator;
    public void PlaySongOne()
    {
        SongManger2.UnpauseGame();
        SongHolder.songToPlay = SongOne;
        SongHolder.csvToRead = csvFileOne;
        SongHolder.songTrack = 0;
        SceneManager.LoadSceneAsync(1);
    }

    public void PlaySongTwo()
    {
        SongManger2.UnpauseGame();
        SongHolder.songToPlay = SongTwo;
        SongHolder.csvToRead = csvFileTwo;
        SongHolder.songTrack = 1;
        SceneManager.LoadSceneAsync(1);
    }

    public void PlaySongThree()
    {
        SongManger2.UnpauseGame();
        SongHolder.songToPlay = SongThree;
        SongHolder.csvToRead = csvFileThree;
        SongHolder.songTrack = 2;
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
