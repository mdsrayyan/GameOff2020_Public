using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] float timerPeriod;
    [SerializeField] Vector2 velocity;
    [SerializeField] GameObject barrel;
    [SerializeField] bool reverse;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerPeriod)
        {
            LaunchBullet();
            timer = 0;
        }
    }

    void LaunchBullet()
    {
        GameObject currentPlatform = Instantiate(platform, barrel.transform.position, Quaternion.identity);
        if(!reverse)
            currentPlatform.transform.rotation = Quaternion.Euler(0, 0, 180);
        currentPlatform.GetComponent<SpawnPlatform>().velocity = velocity;
        
    }
}
