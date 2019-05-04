using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    // Personaje
    [Header("Personaje")]
    public GameObject player;
    [Header("Sprites")]
    public Sprite normalPlayer;
    public Sprite eatingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            player.GetComponent<SpriteRenderer>().sprite = eatingPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.GetComponent<SpriteRenderer>().sprite = normalPlayer;
    }
}
