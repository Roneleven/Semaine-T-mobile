using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public FMOD.Studio.EventInstance backgroundMusicMenu;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicMenu = FMODUnity.RuntimeManager.CreateInstance("event:/UI/MusiqueDuMenu");
        backgroundMusicMenu.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        backgroundMusicMenu.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Quit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Button");
        Application.Quit();
    }

    public void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/MARBLE ARCADE/SD_HUD/S_HUD_INGAME/S_HUD_INGAME_CLICK");
    }
}
