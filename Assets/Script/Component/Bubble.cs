using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float ccDuration = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touched: " + other.name);
        // if (!other.CompareTag("Player"))
        //     return;

        Debug.Log("Tes");
        BubbleCrowdControl cc = other.GetComponent<BubbleCrowdControl>();

        if (cc != null)
        {
            Debug.Log("CC ");
            cc.ApplyBubble(ccDuration);
        }

        Destroy(gameObject);
    }
}