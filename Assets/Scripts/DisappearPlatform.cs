using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    public Transform[] points;
    public float disappearTime = 3.0f;
    public float speed;
    private int num;
    private int currentIndex;
    private float timer;
    private bool startTimer = false;
    private int count;
    private Sprite sprite;

    private void Start()
    {
        num = points.Length;
        currentIndex = 0;
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
        }
        
        if(timer > disappearTime / 2 )
        {
            Blink();
        }

        if (timer > disappearTime)
        {
            Disappear();
        }

        if (currentIndex < num)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[currentIndex].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[currentIndex].position) < 0.001)
            {
                currentIndex++;
                if (currentIndex == num)
                {
                    currentIndex = 0;
                }
            }
        }
    }

    private void Disappear()
    {
        Debug.Log("disappearing");
       if(GameObject.Find("Player"))
        {
            GameObject.Find("Player").transform.parent = null;
        }
        
        
        startTimer = false;
        timer = 0f;
        Destroy(gameObject);
    }

    private void Blink()
    {
        Debug.Log("blinking");
        count++;
        if(count % 5 == 0)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        else
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        startTimer = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.activeSelf)
                collision.gameObject.transform.parent = null;
        }
    }
}
