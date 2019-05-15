using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Altura")]
    public float playerHeight = -3.75f;

    private Touch touch;

    /*
     * Actualización
     */
    void Update()
    {
			if (Time.timeScale == 1.0f)
			{
				MoveCharacter();
			}
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
				// Calcular posición en mundo
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0.0f, 0.0f));
				// Aplicar en nuevo vector
				Vector3 newPosition = new Vector3(touchPosition.x, playerHeight, 0.0f);
				// Establecer posición del personaje
				transform.position = newPosition;
			}
		}
	}
}
