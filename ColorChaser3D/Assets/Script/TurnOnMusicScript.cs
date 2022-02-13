using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnMusicScript : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject muteMusicSound;


    public void TurnOnSounds()
    {

        PlayerPrefs.SetInt("MuteMusic",1);

        musicSource.mute = true;

        gameObject.SetActive(false);
        muteMusicSound.SetActive(true);
    }
}
