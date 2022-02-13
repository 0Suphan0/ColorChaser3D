using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject turnOnMusicSprite;


    public void TurnOnSounds()
    {

        PlayerPrefs.SetInt("MuteMusic",0);

        musicSource.mute = false;

        gameObject.SetActive(false);
        turnOnMusicSprite.SetActive(true);
    }
}
