using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Bubble Prefab")]
    [SerializeField] GameObject bubblePrefab;

    [Header("Spawn Range")]
    [SerializeField] float minX = -8f;
    [SerializeField] float maxX = 8f;
    [SerializeField] float spawnY = -5f;

    [Header("Spawn Timing")]
    [SerializeField] float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnBubble), 0f, spawnInterval);
    }

    void SpawnBubble()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPos = new Vector3(randomX, spawnY, 0);

        Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
    }
}   