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

    public float normalVolume = 1.0f; // Volume normal
    public float pausedVolume = 0.2f;

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
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
            backgroundMusic.setVolume(pausedVolume); // Diminue le volume
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
            pauseMenu.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
            backgroundMusic.setVolume(normalVolume); // Remet le volume à la normale
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseButton.image.sprite = spritePlay;
            pauseMenu.SetActive(false);
            
        }
    }

   public void Retry()
{
    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
    // Arrêtez la musique de fond
    backgroundMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    
    backgroundMusic.setVolume(normalVolume);
    // Rechargez la scène du jeu
    SceneManager.LoadScene(GameScene); 
    Time.timeScale = 1;
}


    public void MainMenu()
    {
    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
    backgroundMusic.setVolume(normalVolume);
        SceneManager.LoadScene(MainMenuScene); 
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
