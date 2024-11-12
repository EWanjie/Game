using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> heartAnimationList;

    private float frameTimer;
    private int currentFrame = 0;
    private const float animationSpeed = 0.2f;

    void Start()
    {
        frameTimer = animationSpeed;

        GetComponent<SpriteRenderer>().sprite = heartAnimationList[0];
    }

    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer < 0f)
        {
            frameTimer += animationSpeed;
            currentFrame = (currentFrame + 1) % heartAnimationList.Count;
            GetComponent<SpriteRenderer>().sprite = heartAnimationList[currentFrame];
        }
    }
}
