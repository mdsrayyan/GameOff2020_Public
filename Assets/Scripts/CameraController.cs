using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private PlayerInputHandler inputHandler;
    private LevelManager levelManager;
    private Vector3 nextPosition;
    private Vector3 startPosition;
    private float nextY;
    private bool startPositionUpdated = false;
    private int index;
    private int num;
    [SerializeField] private float upDistance = 5.0f;
    [SerializeField] private float downDistance = 5.0f;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float sweepSpeed = 10.0f;
    [SerializeField] private Transform[] sweepPoints;

    // Start is called before the first frame update
    void Start()
    {
        
        if(sweepPoints.Length != 0)
        {
            transform.position = sweepPoints[0].position;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
        inputHandler = player.GetComponent<PlayerInputHandler>();
        levelManager = FindObjectOfType<LevelManager>();
        nextY = player.transform.position.y;
        num = sweepPoints.Length;
        index = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManager.levelStatus == LevelManager.Status.Started)
        {
            //Debug.Log(FindObjectOfType<LevelManager>().currentLevelData.playCount);
            //if(FindObjectOfType<LevelManager>().currentLevelData.playCount == 1)
            //{
            //    //CameraSweep();
            //}
            if (inputHandler.CameraInputY != 0)
            {

            }
        }

        else
        {
            CameraControl();
        }
    }

    private void CameraSweep()
    {
        if (inputHandler.CameraInputY != 0)
        {
            levelManager.levelStatus = LevelManager.Status.Running;
            gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
        }

        gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = null;
        if (index < num)
        {
            transform.position = Vector3.MoveTowards(transform.position, sweepPoints[index].position, speed * Time.deltaTime);
            if (transform.position == sweepPoints[index].position)
            {
                index++;
                if (index == num)
                {
                    levelManager.levelStatus = LevelManager.Status.Running;
                    gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
                }
            }
        }
    }
    private void CameraControl()
    {
        if (inputHandler.CameraInputY != 0)
        {
            gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = null;
            nextY += inputHandler.CameraInputY * speed * Time.deltaTime;
            nextY = Mathf.Clamp(nextY, startPosition.y - downDistance, startPosition.y + upDistance);
            Debug.Log(nextY);
            transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
        }

        else
        {
            if (!startPositionUpdated)
            {
                startPosition = transform.position;
                nextY = startPosition.y;
                startPositionUpdated = true;
            }

            ResetPosition();
        }
    }

    void ResetPosition()
    {
        gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
        if (transform.position.y == startPosition.y)
        {
            startPositionUpdated = false;
        }
    }
}
