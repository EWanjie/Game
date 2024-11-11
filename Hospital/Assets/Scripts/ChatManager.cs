using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{

    [SerializeField] private GameObject chat;

    [SerializeField] private GameObject[] chatStation;


    private bool isActive = true;
    private float timer = 10f;

    public void Start()
    {
        chat.SetActive(!isActive);

    }

    public void SetDoctors(List<int> doctors)
    {
        for (int i = 0; i < chatStation.Length; i++)
        {
            chatStation[i].SetActive(false);
        }
       
        foreach (int item in doctors)
        {
            chatStation[item].SetActive(true);
        }
        
    }

    public void Show(bool status)
    {
        isActive = status;
        chat.SetActive(isActive);

        if (isActive)
            StartCoroutine(Timer());
    }
    IEnumerator Timer() // показ врачей
    {
        yield return new WaitForSeconds(timer);
        chat.SetActive(!isActive);

    }
    public void Active()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite)
            sprite.sortingOrder = 100;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chat"))
        {
            SpriteRenderer otherSprite = obj.GetComponent<SpriteRenderer>();
            if (obj != gameObject && otherSprite)
            {
                otherSprite.sortingOrder = 0;
            }
        }


        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("chaticon"))
        {
            SpriteRenderer otherSprite = obj.GetComponent<SpriteRenderer>();
            if (otherSprite)
            {
                otherSprite.sortingOrder = 1;
            }
        }
        for (int i = 0; i < chatStation.Length; i++)
        {
            sprite = chatStation[i].GetComponent<SpriteRenderer>();
            if (sprite)
                sprite.sortingOrder = 101;
        }

    }
}
