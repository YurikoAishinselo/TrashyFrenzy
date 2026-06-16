using UnityEngine;
using System.Collections;

public class BubbleCrowdControl : MonoBehaviour
{
    public Sprite bubbleSprite;
    public Sprite normalSprite;
    public float pushStrength = 6f;

    PlayerController controller;
    SpriteRenderer spriteRenderer;

    bool isInBubble = false;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyBubble(float duration)
    {
        if (!isInBubble)
            StartCoroutine(BubbleRoutine(duration));
    }

    IEnumerator BubbleRoutine(float duration)
    {
        isInBubble = true;

        controller.inputLocked = true;
        spriteRenderer.sprite = bubbleSprite;

        float timer = 0;

        while (timer < duration)
        {
            Vector2 push = Vector2.up * pushStrength;
            controller.ApplyExternalForce(push * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        controller.inputLocked = false;
        Debug.Log("tes");
        spriteRenderer.sprite = normalSprite;

        isInBubble = false;
    }
}