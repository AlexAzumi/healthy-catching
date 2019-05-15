using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	[Header("Parámetros")]
	public int playerScore;
	public float timeBetweenSpawn = 1.0f;
	[Header("Fácil")]
	public float minGravEasy = 0.5f;
	public float maxGravEasy = 1.2f;
	[Header("Normal")]
	public float minGravNormal = 0.4f;
	public float maxGravNormal = 1.5f;
	[Header("UI")]
	public GameObject playerScoreText;
	public GameObject[] hearts;
	public GameObject gameOverSceen;
	public Text finalScore;
	[Header("Posiciones")]
	public GameObject[] spawnPositions;
	[Header("Cereales")]
	public GameObject[] cereales;
	[Header("Chatarra")]
	public GameObject[] chatarra;
	[Header("Frutas")]
	public GameObject[] frutas;
	[Header("Leguminosas")]
	public GameObject[] leguminosas;
	[Header("Origen animal")]
	public GameObject[] origenAnimal;
	[Header("Verduras")]
	public GameObject[] verduras;

	// Componentes
	private GameParameters gameParameters;

	// Otras variables
	private float timer;
	private int heartCount;
	private float minGravityScale;
	private float maxGravityScale;

	/*
	 * Iniciar juego
	 */
	private void Start()
	{
		// Instanciar el puntaje
		playerScore = 0;
		timer = 0;
		heartCount = 2;
		gameParameters = GetComponent<GameParameters>();
		// Parámetros de dificultad
		switch (gameParameters.getDifficulty())
		{
			// Fácil
			case 0:
				minGravityScale = minGravEasy;
				maxGravityScale = maxGravEasy;
				break;
			case 1:
				minGravityScale = minGravNormal;
				maxGravityScale = maxGravNormal;
				break;
			default:
				Debug.Log("No se seleccionó dificultad...");
				minGravityScale = minGravEasy;
				maxGravityScale = maxGravEasy;
				break;
		}
	}

	/*
	 * Actualización por frame
	 */
	private void Update()
	{
		// Agregar tiempo
		timer += Time.deltaTime;
		if (timer >= timeBetweenSpawn)
		{
			// Calcular spawn aleatorio
			int spawn = Random.Range(0, spawnPositions.Length);
			// Calcular tipo de comida aleatorio
			int food = Random.Range(0, 5);
			switch (food)
			{
				case 0:
					spawnFood(cereales, spawn);
					break;
				case 1:
					spawnFood(chatarra, spawn);
					break;
				case 2:
					spawnFood(frutas, spawn);
					break;
				case 3:
					spawnFood(leguminosas, spawn);
					break;
				case 4:
					spawnFood(origenAnimal, spawn);
					break;
				case 5:
					spawnFood(verduras, spawn);
					break;
				default:
					Debug.LogError("Tipo de comida fuera de rango");
					break;
			}
			// Reiniciar timer
			timer = 0.0f;
		}
	}

	/*
	 * Aparecer comida en lugares aleatorios
	 */
	private void spawnFood(GameObject[] food, int spawnPosition)
	{
		// Obtener alimento
		int foodPosition = Random.Range(0, food.Length);
		// Instanciar elemento
		GameObject foodOnGame = Instantiate(food[foodPosition], spawnPositions[spawnPosition].transform.position, spawnPositions[spawnPosition].transform.rotation);
		// Variar escala de gravedad
		float gravityScale = Random.Range(minGravityScale, maxGravityScale);
		foodOnGame.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
	}

	/*
	 * Actualizar puntuación del jugador
	 */
	private void setScore(int score)
	{
		if (playerScore + score >= 0)
		{
			playerScore += score;
			// Actualizar UI
			playerScoreText.GetComponent<Text>().text = "Puntos: " + playerScore;
		}
		// Aplicar animación
		applyScoreAnimation(score > 0);
	}

	/*
	 * Activar animación cuando se atrapa algún alimento
	 */
	private void applyScoreAnimation(bool positive)
	{
		if (!positive)
		{
			playerScoreText.GetComponent<Animator>().SetTrigger("Error");
		}
	}

	/*
	 * Eliminar corazón
	 */
	private void deleteHeart()
	{
		// Desactivar corazón
		hearts[heartCount].SetActive(false);
		if (heartCount > 0)
		{
			// Reducir cuenta
			heartCount--;
		}
		else
		{
			Debug.LogWarning("Partida terminada");
			finalScore.text = playerScore.ToString();
			finishGame();
		}
}

	/*
	 * Fin de la partida
	 */
	public void finishGame()
	{
		// Obtener componente 'Pause'
		Pause pause = GetComponent<Pause>();
		// Pausar el juego
		pause.PauseGame();
		// Activar pantalla de fin de juego
		gameOverSceen.SetActive(true);
	}

	/*
	 * Método a llamar cuando el jugador atrapa comida
	 */
	public void catchedFood(string type)
	{
		if (type != "Chatarra")
		{
			setScore(1);
		}
		else
		{
			// Vibrar teléfono
			Handheld.Vibrate();
			// Eliminar corazón
			deleteHeart();
		}
	}
}
