using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public GameObject ringPrefab;
    public float spawningDelay;
    public Bounds spawningBounds;
    public int ringAmountPerWave;

    List<GameObject> rings = new List<GameObject>();

    void Start()
    {
        spawningBounds.center = transform.position;
        StartSpawning();
    }

    public void StartSpawning() => StartCoroutine(SpawningRoutine());

    IEnumerator SpawningRoutine()
    {
        while (true)
        {
            float distance = spawningBounds.size.x / (ringAmountPerWave + 1);
            float currentXPos = spawningBounds.min.x + distance;
            for (int rings = 0; rings < ringAmountPerWave; ++rings)
            {
                yield return new WaitForSeconds(spawningDelay);
                var position = new Vector2
                (
                    currentXPos + Random.Range(-distance, distance),
                    Random.Range(spawningBounds.min.y, spawningBounds.max.y)
                );
                SpawnRingAt(position);
                currentXPos += distance;
            }
            yield return WaitForRingsToBeCollected();
        }
    }

    void SpawnRingAt(Vector2 position)
    {
        rings.Add(SimplePool.Spawn(ringPrefab, position, Quaternion.identity, transform));
    }

    IEnumerator WaitForRingsToBeCollected()
    {
        while (rings.Count > 0)
        {
            yield return null;
            rings.RemoveAll((ring) => !ring || !ring.activeInHierarchy);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.5f);
        Gizmos.DrawCube(transform.position, spawningBounds.size);
    }
}
