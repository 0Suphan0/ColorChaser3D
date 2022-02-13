using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject ball;
    [SerializeField] private float distanceFromBall;

    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject playAgainScreen;

    [SerializeField] private GameObject playScreen;
    

    [SerializeField] private AudioClip deathSound;
    private AudioSource ballSource;


    void Start()
    {
        ballSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball != null)
        {
            var transformPosition = gameObject.transform.position;
            transformPosition.z = ball.transform.position.z - (distanceFromBall);

            gameObject.transform.position = transformPosition;
        }
      
    }

    public void DeathEffect(string whichType)
    {
        switch (whichType)
        {
            case "Boom":
                ballSource.PlayOneShot(deathSound);
                playScreen.SetActive(false);
                splashScreen.SetActive(true);
                Invoke("ShowStartAgain",2f);

                break;

            case "Fall":
                playScreen.SetActive(false);
                Invoke("ShowStartAgain", 2f);
                break;
        }
       
        
    }

    public void ShowStartAgain()
    {
        playAgainScreen.SetActive(true);
    }
}
