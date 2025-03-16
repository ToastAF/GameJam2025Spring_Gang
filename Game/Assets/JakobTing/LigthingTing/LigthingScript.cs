using UnityEngine;
using System.Collections.Generic;

public class LigthingScript : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        FindNearestEnemy();
    }

    void Update()
    {
        
    }

    public void FindNearestEnemy()
    {
        //List <GameObject> bruh =  GameObject.FindGameObjectsWithTag;

        lineRenderer.SetPosition(1, new Vector3());
    }
}
