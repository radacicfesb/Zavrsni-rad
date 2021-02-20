using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    [SerializeField] GameObject leftGroundTile;
    [SerializeField] GameObject rightGroundTile;
    [SerializeField] Transform ground;

    Vector3 nextSpawnPoint;
    Vector3 nextLeftSpawnPoint;
    Vector3 nextRightSpawnPoint;
    void Start()
    {
        for (int i = 0; i < 15; i++)//popravi posli ovaj 15, mozda digne fps ako ih  se manje spawna na pocetku
        {
            if (i < 2) //prve dvi podloge nece spawnat prepreke
                SpawnTiles(false);
            else
                SpawnTiles(true);

            SpawnLeftTiles();
            SpawnRightTiles();
        }
    }

    public void SpawnTiles(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity) as GameObject;
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        temp.transform.SetParent(ground);

        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();

        }
        temp.GetComponent<GroundTile>().SpawnLeftStuff();
        temp.GetComponent<GroundTile>().SpawnRightStuff();
    }

    public void SpawnLeftTiles()
    {
        GameObject temp = Instantiate(leftGroundTile, new Vector3(10, nextLeftSpawnPoint.y, nextLeftSpawnPoint.z), Quaternion.identity) as GameObject;
        nextLeftSpawnPoint = temp.transform.GetChild(1).transform.position;
        temp.transform.SetParent(ground);

        temp.GetComponent<LRGroundTiles>().SpawnEnvironment();
    }

    public void SpawnRightTiles()
    {
        GameObject temp = Instantiate(rightGroundTile, new Vector3(-10, nextRightSpawnPoint.y ,nextRightSpawnPoint.z), Quaternion.identity) as GameObject;
        nextRightSpawnPoint = temp.transform.GetChild(1).transform.position;
        temp.transform.SetParent(ground);

        temp.GetComponent<LRGroundTiles>().SpawnEnvironment();
    }
}
