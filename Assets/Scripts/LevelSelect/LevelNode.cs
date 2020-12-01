using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNode : MonoBehaviour
{
    public Transform upNode = null;
    public Transform downNode = null;
    public Transform rightNode = null;
    public Transform leftNode = null;
    public bool unlocked = false;
    

    public string levelName;

    private int index;
    private LineRenderer line;

    private void Awake()
    {
        CheckLevelUnlockStatus();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        line = this.gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.positionCount = 9;
        for (int i = 0; i < 9; i++)
        {
            line.SetPosition(i, transform.position);
        }
        if (upNode != null)
        {
            index++;
            line.SetPosition(index, upNode.position);
            index++;
            line.SetPosition(index, transform.position);
        }

        if (downNode != null)
        {
            index++;
            line.SetPosition(index, downNode.position);
            index++;
            line.SetPosition(index, transform.position);
        }

        if (rightNode != null)
        {
            index++;
            line.SetPosition(index, rightNode.position);
            index++;
            line.SetPosition(index, transform.position);
        }

        if (leftNode != null)
        {
            index++;
            line.SetPosition(index, leftNode.position);
            index++;
            line.SetPosition(index, transform.position);
        }
    }

    void CheckLevelUnlockStatus()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(147, 147, 147, 255);
        if (levelName != "" && FindObjectOfType<GameSession>().currentSave.FindLevelData(levelName) != null)
        {
            if (FindObjectOfType<GameSession>().currentSave.FindLevelData(levelName).unlocked)
            {
                unlocked = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }

            if (FindObjectOfType<GameSession>().currentSave.FindLevelData(levelName).completed)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(202, 255, 16, 255);
            }

        }
    }

    public void LoadLevel()
    {
        GameObject.Find("ButtonSounds").GetComponent<AudioSource>().Play();
        StartCoroutine(LoadLevelCoroutine());
    }
    public IEnumerator LoadLevelCoroutine()
    {
        if(levelName == "")
        {
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            if(unlocked)
                SceneManager.LoadScene(levelName);
        }
        
    }
}
