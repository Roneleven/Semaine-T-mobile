using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    public Button pauseButton;
    public Sprite spritePause;
    public Sprite spritePlay; 

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void GamePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseButton.image.sprite = spritePause;
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseButton.image.sprite = spritePause;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
        }
    }
}
