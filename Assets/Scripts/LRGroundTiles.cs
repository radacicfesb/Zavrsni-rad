using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRGroundTiles : MonoBehaviour
{
    PathSpawner pathSpawner;
    [SerializeField] GameObject[] environmentPrefabs;


    void Start()
    {
        pathSpawner = GameObject.FindObjectOfType<PathSpawner>();
    }


    public void SpawnEnvironment()
    {
        for (int i = 2; i < 7; i++)
        {
            Transform spawnPoint = transform.GetChild(i).transform;
            GameObject environment = Instantiate(environmentPrefabs[Random.Range(0, environmentPrefabs.Length)], spawnPoint.position, Quaternion.identity) as GameObject;
            environment.transform.SetParent(spawnPoint);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject, 3f);
    }
}
