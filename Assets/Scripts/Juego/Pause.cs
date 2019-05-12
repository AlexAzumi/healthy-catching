using UnityEngine;

public class Pause : MonoBehaviour
{
	/*
	 * Pausar el juego
	 */
	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	/*
	 * Reanudar el juego
	 */
	public void ResumeGame()
	{
		Time.timeScale = 1;
	}
}
