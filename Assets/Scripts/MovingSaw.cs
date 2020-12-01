using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    private int num;
    private int currentIndex;
    [SerializeField] GameObject saw1;
  

    private void Start()
    {
        num = points.Length;
        currentIndex = 0;
    }

    private void Update()
    {
        if (currentIndex < num)
        {

            saw1.transform.position = Vector2.MoveTowards(saw1.transform.position, points[currentIndex].position, speed * Time.deltaTime);
            if (Vector2.Distance(saw1.transform.position, points[currentIndex].position) < 0.001)
            {
                currentIndex++;
                if (currentIndex == num)
                {
                    currentIndex = 0;
                }
            }
        }
    }
}
