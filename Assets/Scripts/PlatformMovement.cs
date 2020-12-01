using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    private int num;
    private int currentIndex;
    [SerializeField] GameObject platform;
    [SerializeField] bool reversible = true;


    private void Start()
    {
        num = points.Length;
        currentIndex = num-1;
    }

    private void Update()
    {
        if (currentIndex < num)
        {

            platform.transform.position = Vector2.MoveTowards(platform.transform.position, points[currentIndex].position, speed * Time.deltaTime);
            if (Vector2.Distance(platform.transform.position, points[currentIndex].position) < 0.001)
            {
                currentIndex++;
                if (currentIndex == num)
                {
                    if (reversible)
                    {
                        currentIndex = 0;
                    }
                    
                    else
                    {
                        platform.transform.position = points[0].position;
                        currentIndex = 1;
                    }
                }
            }
        }
    }
}
