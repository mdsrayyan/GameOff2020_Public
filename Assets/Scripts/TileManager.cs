using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>(); 
            renderer.color = new Color(176f, 253f, 0f, 255f); // Set to opaque black
        }
    }
}
