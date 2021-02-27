using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    PathSpawner pathSpawner;
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject[] leftSpawningStuff;
    [SerializeField] GameObject[] rightSpawningStuff;
    [SerializeField] GameObject[] pickUpStuff;


    void Start()
    {
        pathSpawner = GameObject.FindObjectOfType<PathSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        pathSpawner.SpawnTiles(true);
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);//od random childa ce izabrat di ce spawnat obstacle
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPoint.position, Quaternion.Euler(new Vector3(0, 0, 90)), transform);
    }

   public void SpawnLeftStuff()
    {
        for (int i = 6; i < 9; i++)
        {
            Transform spawningPlaceLeft = transform.GetChild(i).transform;
            GameObject tree = Instantiate(leftSpawningStuff[Random.Range(0, leftSpawningStuff.Length)], spawningPlaceLeft.position, Quaternion.identity) as GameObject;
            tree.transform.SetParent(spawningPlaceLeft);//negdi san zeznia u kodu pa ih moran sam dodijelit parentu
        }
    }

   public void SpawnRightStuff()
    {
        for (int i = 9; i < 12; i++)
        {
            Transform spawningPlaceRight = transform.GetChild(i).transform;
            GameObject tree = Instantiate(rightSpawningStuff[Random.Range(0, rightSpawningStuff.Length)], spawningPlaceRight.position, Quaternion.identity) as GameObject;
            tree.transform.SetParent(spawningPlaceRight);//ode isto moran sam dodijelit parentu inace se ne unisti
        }
    }

    public void SpawnPickUps()
    {
        Transform spawningPickUp = transform.GetChild(12).transform;
        GameObject pickUp = Instantiate(pickUpStuff[Random.Range(0, pickUpStuff.Length)], spawningPickUp.position, Quaternion.identity) as GameObject;
        pickUp.transform.SetParent(spawningPickUp);
    }
}
