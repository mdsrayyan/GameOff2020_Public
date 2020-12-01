using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected void OnKilled()
    {
        // C#
        //GameObject deathPrefab = (GameObject)Resources.Load("prefabs/prefab1", typeof(GameObject));
        //ParticleSystem exp = deathPrefab.GetComponent<ParticleSystem>();
        //exp.Play();
        FindObjectOfType<LevelManager>().levelStatus = LevelManager.Status.Death;
    }
}
