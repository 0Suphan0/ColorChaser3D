using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Topun forward yönünde ilerleme hýzý , levele göre hýzlandýrýlabilir.
    [SerializeField] private float speed;

    // Topun doðru platformda yol alýrken büyüme katsayýsý.
    [SerializeField] private float scaleBiggerMultiplier;

    //Topun yanlýþ platformda yol alýrken küçülme katsayýsý.
    [SerializeField] private float scaleSmallerMultiplier;

    //Topun sað ve sola hareketlerindeki hýzý
    [SerializeField] private float LeftRightSpeed;

    [SerializeField] private bool isItTest;

    [SerializeField] private Material ballMaterial;

    [SerializeField] private float minBall;
    [SerializeField] private float maxBall;

    [SerializeField] private ParticleSystem rightWayParticleSystem;
    [SerializeField] private ParticleSystem wrongWayParticleSystem;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private AudioClip start;
    [SerializeField] private GameObject highScoreCanvas;

    private int highScoreValue;

    private float currentPositionZ;
    private float currentPositionY;

    private float initialSpeedMultiplier;

    private int currentScore = 0;

    private Rigidbody ballRigidbody;

    private ParticleSystem.MainModule effectColor;

   


    public AudioSource gasLeak;
    public AudioSource gasFill;
    public AudioSource changeColor;


    [SerializeField] float scoreSpeed;



    private Touch phoneTouch;


    private string currentColor;

    private float initalPosition;

    // Start is called before the first frame update
    void Start()
    {
        

        gameObject.GetComponent<AudioSource>().PlayOneShot(start);


        currentPositionY = transform.position.y;
        currentPositionZ = transform.position.z;
        initalPosition = currentPositionZ;
       



        try
        {
            highScoreValue = PlayerPrefs.GetInt("HighScore");
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            
        }
        catch (Exception)
        {
           PlayerPrefs.SetInt("HighScore",0);
           highScore.text = PlayerPrefs.GetInt("HighScore").ToString();

        }


        initialSpeedMultiplier = speed;


        currentScore = 0;
        score.text = currentScore.ToString();
        score.color = Color.white;



        effectColor = rightWayParticleSystem.main;
        

        ballRigidbody = gameObject.GetComponent<Rigidbody>();
        ballMaterial.color = Color.black;
        currentColor = "black";
    }
    
    //void OnEnable()
    //{
    //    currentPositionZ = initalPosition;
    //    gameObject.GetComponent<AudioSource>().PlayOneShot(start);
    //    currentScore = 0;
    //    score.text = currentScore.ToString();
    //    score.color = Color.white;
    //    ballMaterial.color = Color.black;
    //    currentColor = "black";
    //}
    

    // Update is called once per frame
    void Update()
    {

        
        HeightChecker();


            if (transform.position.z - currentPositionZ >= scoreSpeed)
            {
                currentPositionZ = transform.position.z;
                currentScore++;
                score.text = currentScore.ToString();

                
                
                // Degerleri degiskenle tutma degisim miktari her score icin sabit olmayacak...
            if (currentScore == 210)
            {
                ChangeSpeed(1f);
            }

            if (currentScore == 410)
            {
                ChangeSpeed(1f);
            }

            if (currentScore > highScoreValue)
                {

                    if (highScoreCanvas != null)
                    {
                        highScoreCanvas.SetActive(true);
                        Destroy(highScoreCanvas, 4f);
                    }

                    highScoreValue++;
                    highScore.text = highScoreValue.ToString();
                }

            }

            ballRigidbody.AddForce(Vector3.forward * speed - ballRigidbody.velocity);


            float touchMovement = 0;

            switch (isItTest)
            {
                case false:
                    if (Input.touchCount > 0)
                    {
                        phoneTouch = Input.GetTouch(0);

                        if (phoneTouch.phase == TouchPhase.Moved)
                        {
                            touchMovement = 1 * phoneTouch.deltaPosition.x / 10;
                        }


                    }

                    break;
                case true:
                    if (Input.GetMouseButton(0))
                    {
                        touchMovement = Input.GetAxis("Mouse X");
                    }

                    break;
            }



            float translation = touchMovement * 1 * Time.deltaTime * LeftRightSpeed;

            // Move translation along the object's z-axis
            var transformPosition = gameObject.transform.position;
            transformPosition.x += translation;

            gameObject.transform.position = transformPosition;




        

    }

    public void ChangeSpeed(float speedChange)
    {
        speed += speedChange;
    }

    void OnCollisionEnter(Collision coll)
    {


        if (coll.gameObject.CompareTag(currentColor))
        {
            if (!gasLeak.isPlaying)
            {

                gasLeak.Play();
            }
        }

       
        else if (coll.gameObject.CompareTag("white"))
        {
            
        }
        else
        {
            if (!gasFill.isPlaying)
            {
                gasFill.Play();
            }
        }

        //StartCoroutine("DestroyPlatform", coll.gameObject);

    }

    //IEnumerator DestroyPlatform(GameObject platform)
    //{
    //    yield return new WaitForSeconds(10f);
    //    Destroy(platform);
    //    yield break;
    //}

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.CompareTag(currentColor))
        {
            
            Decrease(scaleSmallerMultiplier);
            


        }
        else if (coll.gameObject.CompareTag("white"))
        {

        }

        else
        {

            Increase(scaleBiggerMultiplier);
           
        }

      
    }
    void OnCollisionExit(Collision coll)
    {
        // finish animations

        if (coll.gameObject.CompareTag(currentColor))
        {
            rightWayParticleSystem.Stop();
            gasLeak.Stop();
        }
        else if (coll.gameObject.CompareTag("white"))
        {

        }
        else
        {
            wrongWayParticleSystem.Stop();
            gasFill.Stop();
        }
    }

    void OnTriggerEnter(Collider coll)
    {

        changeColor.Play();

        switch (coll.gameObject.tag)
        {
            case "redChanger":
                
                effectColor.startColor = new ParticleSystem.MinMaxGradient(Color.magenta, Color.red);
                ballMaterial.color = Color.red;
                currentColor = "red";
                break;
            case "blueChanger":
                
                effectColor.startColor = new ParticleSystem.MinMaxGradient(Color.blue, Color.cyan);
                ballMaterial.color = Color.cyan;
                currentColor = "blue";
                break;
            case "yellowChanger":
                
                effectColor.startColor = new ParticleSystem.MinMaxGradient(Color.grey, Color.yellow);
                ballMaterial.color = Color.yellow;
                currentColor = "yellow";
                break;
            case "greenChanger":
                
                effectColor.startColor = new ParticleSystem.MinMaxGradient(Color.grey, Color.green);
                ballMaterial.color = Color.green;
                currentColor = "green";
                break;
            case "purpleChanger":

                effectColor.startColor = new ParticleSystem.MinMaxGradient(Color.grey, Color.magenta);
                ballMaterial.color = Color.magenta;
                currentColor = "purple";
                break;
        }

        Destroy(coll.gameObject);

        //StartCoroutine("ChangeBack", coll.gameObject);


    }


    //IEnumerator ChangeBack(GameObject changeColor)
    //{
    //    yield return new WaitForSeconds(1f);
    //    changeColor.SetActive(true);
    //    yield break;
    //}

    void HeightChecker()
    {
        if (transform.position.y < -2) 
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().DeathEffect("Fall");

            if (currentScore > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
            }

            gameObject.SetActive(false);
        }
    }


    void Increase(float multi)
    {
        rightWayParticleSystem.Stop();
        wrongWayParticleSystem.Play();


        gasLeak.Stop();
        if (!gasFill.isPlaying)
        {
            gasFill.Play();
        }

        gameObject.transform.localScale += Vector3.one * multi / 100;

            if (gameObject.transform.localScale.x >= maxBall)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().DeathEffect("Boom");

                if (currentScore > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", currentScore);
                }

                GameObject.FindGameObjectWithTag("splash").GetComponent<SplashScript>().ChangeColor(ballMaterial.color);



                

                gameObject.SetActive(false);

             

            }
        
    }

    void Decrease(float multi)
    {
        
        wrongWayParticleSystem.Stop();
        // Prevent two audios both of playing...
        if (gasLeak.isPlaying || !gasFill.isPlaying)
        {
            gasFill.Stop();
        }
        

        if (gameObject.transform.localScale.x >= minBall)
        {
            if (!gasLeak.isPlaying)
            {
                gasLeak.Play();
            }

            rightWayParticleSystem.Play();
            gameObject.transform.localScale -= Vector3.one * multi / 100;
        }
        else
        {
            rightWayParticleSystem.Stop();
            gasLeak.Stop();
        }
    }


}
