using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Ajoutez cette ligne pour utiliser SceneManager

public class MenuManager : MonoBehaviour
{
    public bool isPaused;
    public Button pauseButton;
    public Sprite spritePause;
    public Sprite spritePlay;
    public GameObject pauseMenu;
    public string GameScene;
    public string MainMenuScene;
    public FMOD.Studio.EventInstance backgroundMusic;

    void Start()
    {
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/UI/MusiqueDeFond");
        backgroundMusic.start();
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void GamePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pauseButton.image.sprite = spritePause;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
            pauseMenu.SetActive(false);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseButton.image.sprite = spritePause;
            pauseMenu.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
            pauseMenu.SetActive(false);
        }
    }

    // Fonction pour relancer le jeu (appelée par le bouton Retry)
    public void Retry()
    {
        SceneManager.LoadScene(GameScene);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuScene); 
    }
}
