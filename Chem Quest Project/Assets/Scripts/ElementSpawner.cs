using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    public Transform[] Elements;
    public float spawnRadius = 5f;
    public float spawnDelay = 0.1f;
    public int spawnCount = 15;

    private void Start()
    {
        StartCoroutine(FirstSpawn());
    }
    IEnumerator FirstSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomElementOnRadius();
            yield return new WaitForSeconds(spawnDelay);
            yield return null;
        }
    }

    public void SpawnElement()
    {
        SpawnRandomElementOnRadius();
    }

    private void SpawnRandomElementOnRadius()
    {
        if (Elements.Length > 0)
        {
            Vector2 randomPos = (Random.insideUnitCircle.normalized * spawnRadius);
            int randomIndex = Random.Range(0, Elements.Length);
            Instantiate(Elements[randomIndex], new Vector3(randomPos.x, randomPos.y,0) + transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No elements available to spawn.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
