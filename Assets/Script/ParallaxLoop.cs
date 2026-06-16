using UnityEngine;

public class ParallaxSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 2f;
    public float spawnY = 0f;
    public float spawnX = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, spawnInterval);
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(prefab, pos, Quaternion.identity);
    }
}