using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTiles : MonoBehaviour
{
    PathSpawner pathSpawner;

    void Start()
    {
        pathSpawner = GameObject.FindObjectOfType<PathSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        //pathSpawner.SpawnLeftTiles();
        pathSpawner.SpawnRightTiles();
        Destroy(gameObject, 3f);
    }
}
