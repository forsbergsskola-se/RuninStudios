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

    
    public void PlaySongOne()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlaySongTwo()
    {
        
        SceneManager.LoadSceneAsync(1);
    }

    public void PlaySongThree()
    {
        
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void EndGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
