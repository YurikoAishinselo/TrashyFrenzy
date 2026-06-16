using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float destroyY = 6f;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}