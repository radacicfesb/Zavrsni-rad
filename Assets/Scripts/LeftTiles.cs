using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTiles : MonoBehaviour
{
    PathSpawner pathSpawner;
    void Start()
    {
        pathSpawner = GameObject.FindObjectOfType<PathSpawner>();

    }

    private void OnTriggerExit(Collider other)
    {
        pathSpawner.SpawnLeftTiles();
        
    }
}
