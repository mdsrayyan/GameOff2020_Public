using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTutorial : MonoBehaviour
{
    public float timeNeeded = 7.0f;
    public GameObject tutorial;
    bool startDestruction = false;
    Color startColor;
    float alpha;

    void Start()
    {
        StartCoroutine(TutorialComplete());
    }

    void Update()
    {
        if(startDestruction)
        {
            DestorySequence();
        }
    }
    IEnumerator TutorialComplete()
    {
        yield return new WaitForSecondsRealtime(timeNeeded-1.0f);
        startColor = tutorial.GetComponent<Image>().color;
        alpha = startColor.a;
        startDestruction = true;
    }

    void DestorySequence()
    {
        alpha -= Time.deltaTime;
        if(alpha < 0)
        {
            alpha = 0;
        }
        tutorial.GetComponent<Image>().color = new Color(startColor.r, startColor.g, startColor.b, alpha);
    }
}
