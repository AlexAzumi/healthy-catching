using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Variables
    public float initialHeight = 60f;
    private Touch touch;

    /*
     * Actualización
     */
    void Update()
    {
        MoveCharacter();
    }

    /*
     * Movimiento del personaje
     */
    private void MoveCharacter()
    {
        // Si un dedo toca la pantalla
        if (Input.touchCount > 0)
        {
            // Tomar los dedos
            touch = Input.GetTouch(0);
            // Verificar si se movió el dedo
            if (touch.phase == TouchPhase.Moved)
            {
                // Obtener posición
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, initialHeight, 10));
                // Establecer posición del personaje
                transform.position = touchPosition;
            }
        }
    }
}
