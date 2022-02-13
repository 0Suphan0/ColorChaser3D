using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject muteMusic;
    [SerializeField] private GameObject muteSound;
    [SerializeField] private GameObject turnOnMusic;
    [SerializeField] private GameObject turnOnSounds;

    [SerializeField] private AudioSource[] soundSources;
    [SerializeField] private AudioSource musicSource;


    void Start()
    {
        try
        {
            switch (PlayerPrefs.GetInt("MuteSound"))
            {
                case 1:

                    muteSound.SetActive(true);
                    turnOnSounds.SetActive(false);


                    for (int i = 0; i < soundSources.Length; i++)
                    {
                        soundSources[i].mute = true;
                    }
                    break;


                case 0:

                    muteMusic.SetActive(false);
                    turnOnMusic.SetActive(true);

                    for (int i = 0; i < soundSources.Length; i++)
                    {
                        soundSources[i].mute = false;
                    }
                    break;
            }


            switch (PlayerPrefs.GetInt("MuteMusic"))
            {
                case 1:
                    musicSource.mute = true;

                    muteMusic.SetActive(true);
                    turnOnMusic.SetActive(false);


                    break;

                case 0:
                    musicSource.mute = false;

                    muteMusic.SetActive(false);
                    turnOnMusic.SetActive(true);

                    break;
            }
        }
        catch (Exception e)
        {
            
        }
    }





}
