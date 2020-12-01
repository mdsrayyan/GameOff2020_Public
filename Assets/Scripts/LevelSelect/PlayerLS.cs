using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLS : MonoBehaviour
{
    [SerializeField] Transform currentNode;
    [SerializeField] float waitTime = 0.2f;
    private LSInputHandler inputHandler;
    [SerializeField] float speed;
    [SerializeField] bool takeInput = true;
    [SerializeField] string prevScene;
    private int xInput;
    private int yInput;
    private bool jumpInput;
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1.0f;
        inputHandler = GetComponent<LSInputHandler>();
        string objectName = FindObjectOfType<GameSession>().currentSave.currentWorld1Pos;
        if(objectName != "")
        {
            currentNode = GameObject.Find(objectName).transform;
        }
        transform.position = currentNode.position;
        UpdateLabels();
    }

    // Update is called once per frame
    void Update()
    {
        jumpInput = inputHandler.JumpInput;
        if(currentNode.position == transform.position && inputHandler.JumpInput)
        {
            currentNode.GetComponent<LevelNode>().LoadLevel();
            
        }
        //Debug.Log("X Input: " + inputHandler.NormInputX);
        //Debug.Log("Y Input: " + inputHandler.NormInputY);
        if(takeInput)
        {
            CaptureInput();
        }
        ExecuteMovement();
    }

    private void ExecuteMovement()
    {
        if (xInput == 1)
        {
            MoveRight();
        }
        else if (xInput == -1)
        {
            MoveLeft();
        }

        else if (yInput == 1)
        {
            MoveUp();
        }

        else if (yInput == -1)
        {
            MoveDown();
        }

        else
        {
            transform.position = transform.position;
        }
    }

    private void CaptureInput()
    {
        if (inputHandler.NormInputX != 0)
        {
            xInput = inputHandler.NormInputX;
            takeInput = false;
        }

        else if (inputHandler.NormInputY != 0)
        {
            yInput = inputHandler.NormInputY;
            takeInput = false;
        }
    }

    private void MoveDown()
    {
        if (currentNode.GetComponent<LevelNode>().downNode != null)
        {
            MoveTo(currentNode.GetComponent<LevelNode>().downNode);


        }

        else
        {

            xInput = 0;
            yInput = 0;
            takeInput = true;
        }

    }

    private void MoveUp()
    {
        if (currentNode.GetComponent<LevelNode>().upNode != null)
        {
            MoveTo(currentNode.GetComponent<LevelNode>().upNode);
        }

        else
        {

            xInput = 0;
            yInput = 0;
            takeInput = true;
        }
    }

    private void MoveLeft()
    {
        if (currentNode.GetComponent<LevelNode>().leftNode != null)
        {
            
            MoveTo(currentNode.GetComponent<LevelNode>().leftNode);
        }

        else
        {

            xInput = 0;
            yInput = 0;
            takeInput = true;
        }
    }

    private void MoveRight()
    {
        if (currentNode.GetComponent<LevelNode>().rightNode != null)
        {
            MoveTo(currentNode.GetComponent<LevelNode>().rightNode);
        }

        else
        {

            xInput = 0;
            yInput = 0;
            takeInput = true;
        }
    }

    private void MoveTo(Transform nextNode)
    {
        if (nextNode != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextNode.position, speed * Time.deltaTime);
            if (transform.position == nextNode.position)
            {
                currentNode = nextNode;
                FindObjectOfType<GameSession>().currentSave.currentWorld1Pos = currentNode.gameObject.name;
                SaveSystem.UpdateSave(FindObjectOfType<GameSession>().currentSave);
                UpdateLabels();
                xInput = 0;
                yInput = 0;
                takeInput = true;
            }
        }

        else
        {
            xInput = 0;
            yInput = 0;
            takeInput = true;
        }
        
    }

    private void UpdateLabels()
    {

        string levelName = currentNode.GetComponent<LevelNode>().levelName;
        LevelData levelData = FindObjectOfType<GameSession>().currentSave.FindLevelData(levelName);
        if(levelData != null)
        {
            int numCollectibles = levelData.GetCollectibles();
            float bestTime = levelData.bestTime;
            int deaths = levelData.deaths;
            GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>().text = currentNode.GetComponent<LevelNode>().levelName;
            GameObject.Find("Collectibles").GetComponent<TextMeshProUGUI>().text = "Collectibles: " + numCollectibles + "/3";
            GameObject.Find("BestTime").GetComponent<TextMeshProUGUI>().text = "Best: " + bestTime;
            GameObject.Find("Deaths").GetComponent<TextMeshProUGUI>().text = "Deaths: " + deaths;
        }

        else
        {
            GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>().text = currentNode.GetComponent<LevelNode>().levelName;
            GameObject.Find("Collectibles").GetComponent<TextMeshProUGUI>().text = "Collectibles: " + 0 + "/3";
            GameObject.Find("BestTime").GetComponent<TextMeshProUGUI>().text = "Best: " + "NA";
            GameObject.Find("Deaths").GetComponent<TextMeshProUGUI>().text = "Deaths: " + "NA";
        }
    }

    public void LoadPrevScene()
    {
        StartCoroutine(LoadSceneCoroutine(prevScene));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}
