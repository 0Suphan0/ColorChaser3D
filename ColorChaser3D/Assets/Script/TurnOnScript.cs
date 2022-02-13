using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurnOnScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource[] soundSources;
    [SerializeField] private GameObject muteSoundSprite;


    public void TurnOnSounds()
    {
        PlayerPrefs.SetInt("MuteSound",1);


        for (int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i].mute = true;
        }
        gameObject.SetActive(false);
        muteSoundSprite.SetActive(true); 
    }
}
