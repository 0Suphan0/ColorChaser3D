using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject startAgain;
    [SerializeField] private GameObject splash;
    [SerializeField] private GameObject menuCanvas;




    public void StartAgain()
    {
        //startAgain.SetActive(false);



        //splash.SetActive(false);

        

        
        //ball.SetActive(true);
        
        //ball.transform.localScale = new Vector3(.7f, .7f, .7f);
        //ball.transform.position = new Vector3(0, 0.315f, 5);

        //menuCanvas.SetActive(true);

        SceneManager.LoadScene(0);



    }
}
