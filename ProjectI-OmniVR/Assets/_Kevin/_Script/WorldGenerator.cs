using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    //Arrays to hold objects that we will spawn in world
    [SerializeField]
    GameObject[] trees;
    [SerializeField]
    GameObject[] stones;
    [SerializeField]
    GameObject[] terrain;

    private int stoneChanceAmt = 6;

    //Marker objects
    [SerializeField]
    GameObject blMarker;
    [SerializeField]
    GameObject trMarker;

    //spawning grid values / var to control world size
    Vector3 currentPos;
    Vector3 worldObjectStartPos;
    Vector3 terrainStartPos;

    float groundWidth;

    float worldObjetcIncAmt;
    float terrainIncAmt;

    float worldObjetcRandAmt;
    float terrainRandAmt;

    //values to control spawn loop through grid
    [SerializeField]
    int worldObjectRowsAndCols;
    [SerializeField]
    int terrainRowsAndCols;

    [SerializeField]
    int repeatPasses;
    [SerializeField]
    int currentPass;

    [SerializeField]
    float worldObjectSphereRad;
    [SerializeField]
    float terrainSphereRad;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    LayerMask terrainLayer;
    [SerializeField]
    LayerMask worldObjectLayer;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnWorld());
    }

    IEnumerator SpawnWorld()
    {
        groundWidth = trMarker.transform.position.x - blMarker.transform.position.x;

        worldObjetcIncAmt = groundWidth / worldObjectRowsAndCols;
        worldObjetcRandAmt = worldObjetcIncAmt / 2f;

        terrainIncAmt = groundWidth / terrainRowsAndCols;
        terrainRandAmt = terrainIncAmt / 2f;

        worldObjectStartPos = new Vector3(blMarker.transform.position.x - (worldObjetcIncAmt / 2f), blMarker.transform.position.y, blMarker.transform.position.z + (worldObjetcIncAmt / 2f));
        terrainStartPos = new Vector3(blMarker.transform.position.x - (terrainIncAmt / 2f), blMarker.transform.position.y, blMarker.transform.position.z + (terrainIncAmt / 2f));

        for (int rp = 0; rp <= repeatPasses; rp++)
        {
            currentPass = rp;

            if (currentPass == 0)
            {
                currentPos = terrainStartPos;

                for (int cols = 1; cols <= terrainRowsAndCols; cols++)
                {
                    for (int rows = 1; rows <= terrainRowsAndCols; rows++)
                    {
                        currentPos = new Vector3(currentPos.x + terrainIncAmt, currentPos.y, currentPos.z);
                        GameObject newSpawn = terrain[Random.Range(0, terrain.Length)];
                        SpawnHere(currentPos, newSpawn, terrainSphereRad, true);
                        yield return new WaitForSeconds(.01f);
                    }

                    currentPos = new Vector3(terrainStartPos.x, currentPos.y, currentPos.z + terrainIncAmt);
                }
            }
            else if (currentPass > 0)
            {
                currentPos = worldObjectStartPos;

                for (int cols = 1; cols <= worldObjectRowsAndCols; cols++)
                {
                    for (int rows = 1; rows <= worldObjectRowsAndCols; rows++)
                    {
                        currentPos = new Vector3(currentPos.x + worldObjetcIncAmt, currentPos.y, currentPos.z);

                        int spawnChance = Random.Range(1, stoneChanceAmt + 1);

                        if (spawnChance == 1)
                        {
                            GameObject newSpawn = terrain[Random.Range(0, stones.Length)];
                            SpawnHere(currentPos, newSpawn, worldObjectSphereRad, true);
                            yield return new WaitForSeconds(.01f);
                        }
                        else
                        {
                            GameObject newSpawn = terrain[Random.Range(0, trees.Length)];
                            SpawnHere(currentPos, newSpawn, worldObjectSphereRad, true);
                            yield return new WaitForSeconds(.01f);
                        }
                    }

                    currentPos = new Vector3(worldObjectStartPos.x, currentPos.y, currentPos.z + worldObjetcIncAmt);
                }
            }
        }
        WorldGenDone();
    }

    void SpawnHere(Vector3 _newSpawnPos, GameObject _objectToSpawn, float _radiusOfSphere, bool _isObjectTerrain)
    {
        if (_isObjectTerrain)
        {
            Vector3 randPos = new Vector3(_newSpawnPos.x + Random.Range(-terrainRandAmt, terrainRandAmt + 1), 0, _newSpawnPos.z + Random.Range(-terrainRandAmt, terrainRandAmt + 1));
            Vector3 rayPos = new Vector3(randPos.x, 10, randPos.z);


            if (Physics.Raycast(rayPos, -Vector3.up, Mathf.Infinity, groundLayer))
            {
                Collider[] objectsHit = Physics.OverlapSphere(randPos, _radiusOfSphere, terrainLayer);

                if (objectsHit.Length == 0)
                {
                    GameObject terrainObject = (GameObject)Instantiate(_objectToSpawn, randPos, Quaternion.identity);
                    terrainObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);
                }
            }
        }
        else
        {
            Vector3 randPos = new Vector3(_newSpawnPos.x + Random.Range(-worldObjetcRandAmt, worldObjetcRandAmt + 1), 0, _newSpawnPos.z 
                + Random.Range(-worldObjetcRandAmt, worldObjetcRandAmt + 1));
            Vector3 rayPos = new Vector3(randPos.x, 20, randPos.z);

            RaycastHit hit;

            if(Physics.Raycast(rayPos, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
            {
                randPos = new Vector3(randPos.x, hit.point.y, randPos.z);
                Collider[] objectshit = Physics.OverlapSphere(randPos, _radiusOfSphere, worldObjectLayer);

                if (objectshit.Length == 0)
                {
                    GameObject worldObject = (GameObject)Instantiate(_objectToSpawn, randPos, Quaternion.identity);
                    worldObject.transform.position = new Vector3(worldObject.transform.position.x, worldObject.transform.position.y + (worldObject.GetComponent<Renderer>().bounds.extents.y * .7f), 
                        worldObject.transform.position.z);
                    worldObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);
                }
            }
        }
    }

    void WorldGenDone()
    {

    }
}
