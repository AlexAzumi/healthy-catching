using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	/*
	 * Cargar otra escena
	 */
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
