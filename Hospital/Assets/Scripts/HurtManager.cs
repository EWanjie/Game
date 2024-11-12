using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtManager : MonoBehaviour
{
    [SerializeField] private GameObject hart;
    [SerializeField] private List<Sprite> heartAnimationList;

    private float curentTime;

    public float duration;
    private float frameTimer;
    private float currentTimer = 0;
    private int currentFrame;
    private int currentAnimation = 0;
    private const float animationSpeed = 0.2f;
    private const float coefficient = 0.85f;
    private Vector3[] moveAnimation = { Vector3.zero, Vector3.zero };


    private void Start()
    {
        hart.SetActive(false);
        frameTimer = animationSpeed;
        moveAnimation[0] = (Vector2)transform.localScale;
        moveAnimation[1] = coefficient * (Vector2)transform.localScale;
    }

    private void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer < 0f)
        {
            currentTimer += animationSpeed;
            frameTimer += animationSpeed;

            float x = currentTimer / (duration / heartAnimationList.Count);
            currentAnimation = (int)x;
            GetComponent<SpriteRenderer>().sprite = heartAnimationList[currentAnimation];

            currentFrame = (currentFrame + 1) % 2;
            transform.localScale = moveAnimation[currentFrame];
        }
        if(currentTimer + 0.1f >= duration)
        {
            hart.SetActive(false);
        }
    }

    public void StartHart(float store)
    {
        currentTimer = 0;
        duration = store;
        GetComponent<SpriteRenderer>().sprite = heartAnimationList[0];
        hart.SetActive(true);
    }
}
