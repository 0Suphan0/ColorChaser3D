using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource[] soundSources;
    [SerializeField] private GameObject turnOnSounds;


    public void TurnOnSounds()
    {
        PlayerPrefs.SetInt("MuteSound",0);

        for (int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i].mute = false;
        }
        gameObject.SetActive(false);
        turnOnSounds.SetActive(true);
    }
}
