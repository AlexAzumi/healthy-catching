using UnityEngine;

public class CatchFood : MonoBehaviour
{
    [Header("Controlador de partida")]
    public Game game;
    [Header("Sprite original")]
    public Sprite originalSprite;

    /*
     * Atrapar comida
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Atrapado: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Food")
        {
            // Obtener tipo de comida
            string type = collision.gameObject.GetComponent<FoodType>().getFoodType();
            // Enviar a controlador de juego
            game.cachedFood(type);
            // Eliminar objeto
            Destroy(collision.gameObject);
            // Restaurar sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = originalSprite;
        }
    }
}
