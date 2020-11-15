using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPlacer : MonoBehaviour
{
    public Transform player;
    public GameObject island;
    public float islandSpacing = 30;
    public int seed = 0;
    public int worldSize = 10;
    public float placeChance = 0.2f;

    List<GameObject> islands = new List<GameObject>();

    void Start()
    {
        Random.seed = seed;
        for (int i = -1 * worldSize; i < worldSize; i++)
        {
            for (int j = -1 * worldSize; j < worldSize; j++)
            {
                if (Random.value < placeChance)
                {
                    PlaceIsland(i + Random.value, j + Random.value);
                }
            }
        }
        StartCoroutine(CheckIslands());
    }

    void PlaceIsland(float i, float j)
    {
        islands.Add(GameObject.Instantiate(island, new Vector3(i * islandSpacing, 1, j * islandSpacing), Quaternion.identity));
    }

    IEnumerator CheckIslands()
    {
        foreach (var isl in islands)
        {
            isl.SetActive(Vector3.Distance(isl.transform.position, player.position) < 100f);
        }
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(CheckIslands());
    }
}