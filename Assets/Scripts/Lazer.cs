using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform startPoint;
    public Transform endPoint;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        lineRenderer.SetPosition (0, startPoint.localPosition);
        lineRenderer.SetPosition (1, endPoint.localPosition);
    }
}
