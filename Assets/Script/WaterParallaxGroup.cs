using UnityEngine;

public class WaterParallaxGroup : MonoBehaviour
{
    public Transform background;
    public Transform foreground;

    public float speed = 2f;
    public float width = 20f;

    void Update()
    {
        Move(background);
        Move(foreground);
    }

    void Move(Transform obj)
    {
        obj.Translate(Vector3.left * speed * Time.deltaTime);

        if (obj.localPosition.x <= -width)
        {
            obj.localPosition += new Vector3(width * 2f, 0, 0);
        }
    }
}