using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeThrower : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float timerPeriod;
    [SerializeField] float speed;
    [SerializeField] float gravityScale = 1.0f;
    [SerializeField] bool gravityEffect;
    [SerializeField] GameObject barrel;
    Vector2 velocity;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        float angle = Mathf.Deg2Rad * barrel.transform.rotation.eulerAngles.z;
        timer = 0;
        velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timerPeriod)
        {
            LaunchBullet();
            timer = 0;
        }
    }

    void LaunchBullet()
    {
        GameObject currentBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        currentBullet.GetComponent<Rigidbody2D>().velocity = velocity;
        if(gravityEffect)
        {
            currentBullet.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }

        else
        {
            currentBullet.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
}
