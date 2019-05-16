using UnityEngine;

public class GameParameters : MonoBehaviour
{
    // Dificultad
    [Header("Parámetros")]
    static public int? difficult;

    /*
		* Start
		*/
    private void Start()
    {
			if (difficult != null)
			{
				switch (difficult)
				{
					// Fácil
					case 0:
						Debug.Log("Dificultad: Fácil");
						break;
					// Normal
					case 1:
						Debug.Log("Dificultad: Normal");
						break;
					// Desconocido
					default:
						Debug.Log("Dificultad (desconocida): " + difficult);
						break;
				}
			}
			else
			{
				Debug.Log("Dificultad no definida");
				difficult = 0;
			}
    }

    /*
		* Definir dificultad
		*/
    public void setDifficulty(int diff)
    {
			difficult = diff;
    }

		/*
		 *
		 */
		public int? getDifficulty()
		{
			return difficult;
		}
}
