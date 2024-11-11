using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{

    [SerializeField] private GameObject chat;

    private bool isActive = true;
    private float timer = 5f;

    public void Start()
    {
        chat.SetActive(!isActive);
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
    }
}
