﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StandGameManager : MonoBehaviour 
{
    //Fade関係
    public GameObject fadeObject;
    Image fadeImage;
    int alpha = 255;

    public int state = 0;

	// Use this for initialization
	void Start () 
	{
        //Fade関係
        fadeImage = fadeObject.GetComponent<Image>();
        fadeObject.SetActive(false);

        StartCoroutine("FadeIn");
        StartCoroutine("FadeOut");
        StartCoroutine("Show");
        StartCoroutine("GameStart");
	}

    IEnumerator FadeIn()
    {
        while (true)
        {
            if (state == 0)
            {
                fadeObject.SetActive(true);

                if (alpha < 5)
                {
                    state += 1;
                    yield return null;
                }

                fadeImage.color = new Color(1, 1, 1, alpha / 255.0f);
                alpha -= 5;
                yield return new WaitForEndOfFrame();
            }

            yield return null;
        }
        
    }


    IEnumerator Show()
    {
        while (true)
        {
            if (state == 1)
            {
                yield return new WaitForSeconds(2.0f);

                state += 1;

            }
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            if (state == 2)
            {
                fadeObject.SetActive(true);

                if (alpha > 250)
                {
                    state += 1;
                    yield return null;
                }

                fadeImage.color = new Color(255, 255, 255, alpha / 255.0f);
                alpha += 5;

                yield return new WaitForEndOfFrame();

            }
            yield return null;
        }
    }


    IEnumerator GameStart()
    {
        while (true)
        {
            if (state == 3)
            {
                Application.LoadLevel("Title");

            }
            yield return null;
        }
    }

}
