using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SplashScript : MonoBehaviour
{

    [SerializeField] private GameObject[] splashes;

    public void ChangeColor(Color ballcolor)
    {
        for (int i = 0; i < splashes.Length; i++)
        {
            splashes[i].GetComponent<Image>().color = ballcolor;
        }
    }

}
