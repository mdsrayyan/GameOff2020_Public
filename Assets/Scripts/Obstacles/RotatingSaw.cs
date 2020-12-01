using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotatingSaw : Obstacle
{
    [SerializeField] float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speed * 0.9f, speed * 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1), speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnKilled();
        }

        else if (collision.gameObject.CompareTag("Stopper"))
        {
            Destroy(this.gameObject);
        }
    }
}
