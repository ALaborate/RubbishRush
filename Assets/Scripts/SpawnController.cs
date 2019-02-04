using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{

    public GameObject[] spawnPoints;
    public GameObject[] objectsToSpawn;
    public float randomRange = 2f;
    public float spawnRate;
    public bool constantSpawnSpeed;


    private float nextTimeToSpawn;


    // Use this for initialization
    void Start()
    {
        foreach (GameObject obj in spawnPoints)
        {
            obj.transform.parent = transform;
        }
        nextTimeToSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!constantSpawnSpeed)
        {
            if (Time.time >= nextTimeToSpawn && !GameController.instance.IsGameOver)
            {

                SpawnAtRandomPoint(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)]);
                nextTimeToSpawn = Time.time + spawnRate;
                spawnRate *= 0.98f;
                spawnRate = Mathf.Clamp(spawnRate, 2f, 5);
            }
        }
        else
        {
            if (Time.time >= nextTimeToSpawn && !GameController.instance.IsGameOver)
            {
                SpawnAtRandomPoint(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)]);
                nextTimeToSpawn = Time.time + spawnRate;
          
            }
        }
    }

    private void SpawnWave()
    {
        if (spawnPoints.Length == 0)
            return;
        foreach (GameObject obj in spawnPoints)
        {
            Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], obj.transform.position, Quaternion.identity);
        }
    }

    private void SpawnAtRandomPoint(GameObject objToSpawn)
    {
        var sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var r = Random.insideUnitCircle*randomRange;
        var obj = Instantiate(objToSpawn, sp.transform.position+new Vector3(r.x, r.y), Quaternion.identity);
        obj.transform.SetParent(sp.transform.parent);
    }

}
