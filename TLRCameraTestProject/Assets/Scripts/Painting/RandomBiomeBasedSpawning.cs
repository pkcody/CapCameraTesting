using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBiomeBasedSpawning : MonoBehaviour
{
    public RandomBiomeBasedSpawning instance;
    public LayerMask terrainLayerMask;

    public List<Cell> doNotSpawnGOsInCells;

    [Header("Red Biome Spawns")]
    public List<GameObject> redBiomePrefabs;
    public Transform redBiomeParent;
    public int redBiomeSpawnMinAmount = 10;
    public int redBiomeSpawnMaxAmount = 15;
    public int redBiomeMinDistanceBetweenSpawns = 10;
    public int redBiomeAttemptsToSpawnNotNearAnotherSpawn = 5;
    public List<GameObject> redBiomeSpawnedGOs = new List<GameObject>();

    [Header("Green Biome Spawns")]
    public List<GameObject> greenBiomePrefabs;
    public Transform greenBiomeParent;
    public int greenBiomeSpawnMinAmount = 15;
    public int greenBiomeSpawnMaxAmount = 25;
    public int greenBiomeMinDistanceBetweenSpawns = 7;
    public int greenBiomeAttemptsToSpawnNotNearAnotherSpawn = 5;
    public List<GameObject> greenBiomeSpawnedGOs = new List<GameObject>();

    [Header("Blue Biome Spawns")]
    public List<GameObject> blueBiomePrefabs;
    public Transform blueBiomeParent;
    public int blueBiomeSpawnMinAmount = 5;
    public int blueBiomeSpawnMaxAmount = 10;
    public int blueBiomeMinDistanceBetweenSpawns = 15;
    public int blueBiomeAttemptsToSpawnNotNearAnotherSpawn = 5;
    public List<GameObject> blueBiomeSpawnedGOs = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Object[] redBiome_Resources = Resources.LoadAll("RedResources", typeof(GameObject));
        Object[] greenBiome_Resources = Resources.LoadAll("GreenResources", typeof(GameObject));
        Object[] blueBiome_Resources = Resources.LoadAll("BlueResources", typeof(GameObject));

        foreach (var go in redBiome_Resources)
        {
            redBiomePrefabs.Add(go as GameObject);
        }
        foreach (var go in greenBiome_Resources)
        {
            greenBiomePrefabs.Add(go as GameObject);
        }
        foreach (var go in blueBiome_Resources)
        {
            blueBiomePrefabs.Add(go as GameObject);
        }
    }


    public Vector3 RandomPrefabSpawnPosInCell(Cell c)
    {
        int xMin = c.col * GridBreakdown.cellPixelSize - 495;
        int xMax = c.col * GridBreakdown.cellPixelSize - 405 - GridBreakdown.cellPixelSize;
        int zMin = c.row * GridBreakdown.cellPixelSize - 495;
        int zMax = c.row * GridBreakdown.cellPixelSize - 405 - GridBreakdown.cellPixelSize;

        //print($"xmin {xMin} xmax {xMax}");
        //print($"zmin {zMin} zmax {zMax}");
        float randXinCell = Random.Range(xMin, xMax);
        float randZinCell = Random.Range(zMin, zMax);

        return new Vector3(randXinCell, 30f, randZinCell);
    }

    public Vector3 MoveGOToTerrainHeight(GameObject go)
    {
        //Debug.DrawRay(go.transform.position, new Vector3(0, -70, 0), Color.magenta, 15);
        Physics.Raycast(go.transform.position, Vector3.down, out RaycastHit hit, 100);

        return new Vector3(go.transform.position.x, hit.point.y, go.transform.position.z);
    }

    public void SpawnBiomeGOs()
    {
        RedBiomeSpawn();
        GreenBiomeSpawn();
        BlueBiomeSpawn();
    }

    private void RedBiomeSpawn()
    {
        print("RedBiomeSpawn");
        redBiomeParent = GameObject.Find("RedBiomeParent").transform;
        foreach (Cell c in GridBreakdown.instance.redBiomeCells)
        {
            if (!doNotSpawnGOsInCells.Contains(c))
            {
                for (int i = 0; i < Random.Range(redBiomeSpawnMinAmount, redBiomeSpawnMaxAmount); i++)
                {
                    for (int a = 0; a < redBiomeAttemptsToSpawnNotNearAnotherSpawn; a++)
                    {

                        Vector3 spawnPos = RandomPrefabSpawnPosInCell(c);

                        if (redBiomeSpawnedGOs.Count == 0)
                        {
                            GameObject pickedRedBiomePrefab = redBiomePrefabs[Random.Range(0, redBiomePrefabs.Count)];
                            GameObject redBiomeGO = Instantiate(pickedRedBiomePrefab, spawnPos, Quaternion.identity, redBiomeParent);
                            redBiomeGO.transform.position = MoveGOToTerrainHeight(redBiomeGO);
                            redBiomeSpawnedGOs.Add(redBiomeGO);
                            break;
                        }
                        else
                        {
                            foreach (GameObject existingSpawn in redBiomeSpawnedGOs)
                            {
                                if ((spawnPos - existingSpawn.transform.position).magnitude < redBiomeMinDistanceBetweenSpawns)
                                {
                                    break;
                                }
                                else
                                {
                                    GameObject pickedRedBiomePrefab = redBiomePrefabs[Random.Range(0, redBiomePrefabs.Count)];
                                    GameObject redBiomeGO = Instantiate(pickedRedBiomePrefab, spawnPos, Quaternion.identity, redBiomeParent);
                                    redBiomeGO.transform.position = MoveGOToTerrainHeight(redBiomeGO);
                                    redBiomeSpawnedGOs.Add(redBiomeGO);
                                    break;
                                }
                            }
                            print("Unable to spawn Red");
                            break;
                        }
                    }
                } 
            }
        }
    }

    private void GreenBiomeSpawn()
    {
        print("GreenBiomeSpawn");
        greenBiomeParent = GameObject.Find("GreenBiomeParent").transform;
        foreach (Cell c in GridBreakdown.instance.greenBiomeCells)
        {
            if (!doNotSpawnGOsInCells.Contains(c))
            {
                for (int i = 0; i < Random.Range(greenBiomeSpawnMinAmount, greenBiomeSpawnMaxAmount); i++)
                {
                    for (int a = 0; a < greenBiomeAttemptsToSpawnNotNearAnotherSpawn; a++)
                    {

                        Vector3 spawnPos = RandomPrefabSpawnPosInCell(c);

                        if (greenBiomeSpawnedGOs.Count == 0)
                        {
                            GameObject pickedGreenBiomePrefab = greenBiomePrefabs[Random.Range(0, greenBiomePrefabs.Count)];
                            GameObject greenBiomeGO = Instantiate(pickedGreenBiomePrefab, spawnPos, Quaternion.identity, greenBiomeParent);
                            greenBiomeGO.transform.position = MoveGOToTerrainHeight(greenBiomeGO);
                            greenBiomeSpawnedGOs.Add(greenBiomeGO);
                            break;
                        }
                        else
                        {
                            foreach (GameObject existingSpawn in greenBiomeSpawnedGOs)
                            {
                                if ((spawnPos - existingSpawn.transform.position).magnitude < greenBiomeMinDistanceBetweenSpawns)
                                {
                                    break;
                                }
                                else
                                {
                                    GameObject pickedGreenBiomePrefab = greenBiomePrefabs[Random.Range(0, greenBiomePrefabs.Count)];
                                    GameObject greenBiomeGO = Instantiate(pickedGreenBiomePrefab, spawnPos, Quaternion.identity, greenBiomeParent);
                                    greenBiomeGO.transform.position = MoveGOToTerrainHeight(greenBiomeGO);
                                    greenBiomeSpawnedGOs.Add(greenBiomeGO);
                                    break;
                                }
                            }
                            print("Unable to spawn Green");
                            break;
                        }
                    }
                } 
            }
        }
    }

    private void BlueBiomeSpawn()
    {
        print("BlueBiomeSpawn");
        blueBiomeParent = GameObject.Find("BlueBiomeParent").transform;
        foreach (Cell c in GridBreakdown.instance.blueBiomeCells)
        {
            if (!doNotSpawnGOsInCells.Contains(c))
            {
                for (int i = 0; i < Random.Range(blueBiomeSpawnMinAmount, blueBiomeSpawnMaxAmount); i++)
                {
                    for (int a = 0; a < blueBiomeAttemptsToSpawnNotNearAnotherSpawn; a++)
                    {

                        Vector3 spawnPos = RandomPrefabSpawnPosInCell(c);

                        if (blueBiomeSpawnedGOs.Count == 0)
                        {
                            GameObject pickedBlueBiomePrefab = blueBiomePrefabs[Random.Range(0, blueBiomePrefabs.Count)];
                            GameObject blueBiomeGO = Instantiate(pickedBlueBiomePrefab, spawnPos, Quaternion.identity, blueBiomeParent);
                            blueBiomeGO.transform.position = MoveGOToTerrainHeight(blueBiomeGO);
                            blueBiomeSpawnedGOs.Add(blueBiomeGO);
                            break;
                        }
                        else
                        {
                            foreach (GameObject existingSpawn in blueBiomeSpawnedGOs)
                            {
                                if ((spawnPos - existingSpawn.transform.position).magnitude < blueBiomeMinDistanceBetweenSpawns)
                                {
                                    break;
                                }
                                else
                                {
                                    GameObject pickedBlueBiomePrefab = blueBiomePrefabs[Random.Range(0, blueBiomePrefabs.Count)];
                                    GameObject blueBiomeGO = Instantiate(pickedBlueBiomePrefab, spawnPos, Quaternion.identity, blueBiomeParent);
                                    blueBiomeGO.transform.position = MoveGOToTerrainHeight(blueBiomeGO);
                                    blueBiomeSpawnedGOs.Add(blueBiomeGO);
                                    break;
                                }
                            }
                            print("Unable to spawn Blue");
                            break;
                        }
                    }
                } 
            }
        }
    }
}
