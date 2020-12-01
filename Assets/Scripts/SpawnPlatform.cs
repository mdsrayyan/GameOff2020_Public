using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public Vector2 velocity;

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
