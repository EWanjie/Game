using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Вызывается при столкновении с другим коллайдером
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Столкновение со статичным объектом!");
    }
}