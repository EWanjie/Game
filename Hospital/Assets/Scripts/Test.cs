using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.Entities;

public class Test : MonoBehaviour
{

    private Sprite[] frameArray;
    private int currentFrame;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    private void Update()
    {
       /* timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer -= 1f;
            currentFrame++;
            gameObject.GetComponent<Renderer>().sprite = frameArray[currentFrame];
        }*/
    }
}
