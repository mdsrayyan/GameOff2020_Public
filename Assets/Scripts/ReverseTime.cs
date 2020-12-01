using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTime : MonoBehaviour
{
    [SerializeField]
    private float maxReverseTime = 3.0f;
    private Vector3[] positions = new Vector3[300];
    private Quaternion[] rotations = new Quaternion[300];
    private int index;
    private float reverseTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = this.transform.position;
            rotations[i] = this.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>().RewindInput)
        {
            index++;
            if(index <= positions.Length)
            {
                transform.position = positions[positions.Length - index];
                transform.rotation = rotations[positions.Length - index];
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>().UseRewindInput();
            }
        }
        else
        {
            AddPositon(positions, this.transform.position);
            AddRotation(rotations, this.transform.rotation);
            index = 0;
        }
    }

    private void AddPositon(Vector3[] positions, Vector3 position)
    {
        for (int i = 1; i < positions.Length; i++)
        {
            positions[i - 1] = positions[i];
        }

        positions[positions.Length-1] = position;
    }

    private void AddRotation(Quaternion[] positions, Quaternion position)
    {
        for (int i = 1; i < positions.Length; i++)
        {
            positions[i - 1] = positions[i];
        }

        positions[positions.Length - 1] = position;
    }

    void Rewind()
    {
        Debug.Log("Rewinding");
        for (int i = 1; i <= positions.Length; i++)
        {
            Debug.Log(positions[positions.Length - i]);
            transform.position = positions[positions.Length - i];
        }
    }
}
