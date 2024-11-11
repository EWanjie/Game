using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void Active()
    {
        if (canvas)
            canvas.sortingOrder = 100;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("bar"))
        {
            Canvas otherSprite = obj.GetComponent<Canvas>();
            if (obj != gameObject && otherSprite)
            {
                otherSprite.sortingOrder = 0;
            }
        }
    }
}
