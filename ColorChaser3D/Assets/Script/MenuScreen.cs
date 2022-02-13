using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MenuScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playScreen;
    [SerializeField] private GameObject ball;
    [SerializeField] private bool isItTest;
    [SerializeField] private AudioSource music;

    void Start()
    {
        switch (PlayerPrefs.GetInt("MuteMusic"))
        {
            case 1:
                music.mute = true;
                break;

            case 0:
                music.mute = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (isItTest)
        {
            case true:

                if (Input.GetMouseButton(0))
                {

                    playScreen.SetActive(true);
                    ball.SetActive(true);
                    gameObject.SetActive(false);
                }
                break;

            case false:
                if (Input.touchCount>0)
                {
                    ball.SetActive(true);
                    playScreen.SetActive(true);
                    gameObject.SetActive(false);
                }
                break;

        }

        
    }
}
