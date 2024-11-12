using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CursorManager;

public class PortalManager : MonoBehaviour
{

    [SerializeField] private List<Sprite> portalAnimationList;

    private float frameTimer;
    private int currentFrame = 0;
    private const float animationSpeed = 0.2f;

    void Start()
    {
        frameTimer = animationSpeed;

        GetComponent<SpriteRenderer>().sprite = portalAnimationList[0];
    }

    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer < 0f)
        {
            frameTimer += animationSpeed;
            currentFrame = (currentFrame + 1) % portalAnimationList.Count;
            GetComponent<SpriteRenderer>().sprite = portalAnimationList[currentFrame];
        }
    }
}
