using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daughter : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject particles;

    void Start()
    {
        particles.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().levelStatus = LevelManager.Status.Won;
            anim = GetComponent<Animator>();
            particles.SetActive(true);
            particles.transform.position = gameObject.transform.position;
            anim.SetBool("disappear", true);
        }
    }
}
