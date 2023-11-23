using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
