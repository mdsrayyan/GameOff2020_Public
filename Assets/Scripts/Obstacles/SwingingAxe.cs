using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingingAxe : Obstacle
{
    [SerializeField] Transform pivot;
    [SerializeField] float amplitude = 3f;
    [SerializeField] float speed = 20f;
    [SerializeField] bool randomize = true;
    float currentAngle = 0.0f;
    float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        if(randomize)
        {
            amplitude = Random.Range(amplitude * 0.8f, amplitude * 1.2f);
            speed = Random.Range(speed * 0.8f, speed * 1.2f);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        float angle = amplitude * Mathf.Sin(Time.time * speed);
        transform.RotateAround(pivot.transform.position, new Vector3(0, 0, 1), angle - currentAngle);
        currentAngle = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnKilled();
        }
    }
}
