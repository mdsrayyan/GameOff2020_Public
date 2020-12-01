using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] int index;
    [SerializeField] GameObject pLight;
    [SerializeField] GameObject collectableSound;
    [SerializeField] GameObject collectableParticle;
    [SerializeField] Sprite collectedSprite;
    // Start is called before the first frame update
    void Start()
    {
        collectableSound.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-45.0f, 45.0f));
        CheckLevelData();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 1, 0), speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WriteData();
            collectableSound.SetActive(true);
            collectableParticle.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            pLight.SetActive(false);
            StartCoroutine(DestroyCoroutine());
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        Destroy(gameObject);
    }
    void WriteData()
    {
        if(index == 1)
            FindObjectOfType<LevelManager>().collectible1 = true;
        else if(index == 2)
            FindObjectOfType<LevelManager>().collectible2 = true;
        else if (index == 3)
            FindObjectOfType<LevelManager>().collectible3 = true;
    }

    void CheckLevelData()
    {
        if (index == 1)
        {
            if(FindObjectOfType<LevelManager>().collectible1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = collectedSprite;
            }
        }
        if (index == 2)
        {
            if (FindObjectOfType<LevelManager>().collectible2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = collectedSprite;
            }
        }

        if (index == 3)
        {
            if (FindObjectOfType<LevelManager>().collectible3)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = collectedSprite;
            }
        }
    }
}
