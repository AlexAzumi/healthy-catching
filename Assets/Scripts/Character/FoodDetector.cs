using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDetector : MonoBehaviour
{
    public GameObject player;
    public Sprite normalPlayer;
    public Sprite eatingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entrado");
        if (collision.gameObject.tag == "Food")
        {
            player.GetComponent<SpriteRenderer>().sprite = eatingPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Saliendo");
        player.GetComponent<SpriteRenderer>().sprite = normalPlayer;
    }
}
