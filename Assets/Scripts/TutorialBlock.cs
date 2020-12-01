using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlock : MonoBehaviour
{
    public GameObject tutorialScene;
    // Start is called before the first frame update
    void Start()
    {
        tutorialScene.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tutorialScene.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void OnJumpPressed()
    {
        Time.timeScale = 1.0f;
        tutorialScene.SetActive(false);
    }
}