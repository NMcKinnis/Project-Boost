using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 15f;
    [SerializeField] GameObject[] volcanicRocks;
    bool spawn = true;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnVolcanicRock();
        }
    }

private void SpawnVolcanicRock()
    {
        var spawnableIndex = Random.Range(0, volcanicRocks.Length);
        Spawn(volcanicRocks[spawnableIndex]);
    }

    private void Spawn(GameObject volcanicRock)
    {
        GameObject newVolcanicRock = Instantiate(volcanicRock, transform.position, transform.rotation);
        newVolcanicRock.transform.parent = transform;
    }
}
