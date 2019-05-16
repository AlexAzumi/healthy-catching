using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	[Header("Parámetros")]
	public int playerScore;
	public float timeBetweenSpawn = 1.0f;
	public Animator blackScreenAnimator;
	public AudioSource sourceHit;
	public AudioSource sourceCatch;
	[Header("Chatarra")]
	public int minProbability = 2;
	public int maxProbability = 4;
	[Header("Fácil")]
	public float minGravEasy = 0.5f;
	public float maxGravEasy = 0.9f;
	[Header("Normal")]
	public float minGravNormal = 0.4f;
	public float maxGravNormal = 1.1f;
	public GameObject preparationScreen;
	public Text foodText;
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
	private int heartCount;
	private int spawnTrashCount;
	private int selectedFoodNum;
	private string selectedFood;
	private float timer;
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
		spawnTrashCount = Random.Range(minProbability, maxProbability);
		gameParameters = GetComponent<GameParameters>();
		// Parámetros de dificultad
		switch (gameParameters.getDifficulty())
		{
			// Fácil
			case 0:
				minGravityScale = minGravEasy;
				maxGravityScale = maxGravEasy;
				setEasyGame();
				break;
			case 1:
				minGravityScale = minGravNormal;
				maxGravityScale = maxGravNormal;
				// Configurar juego en normal
				setNormalGame();
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
			Debug.LogWarning("Siguiente chatarra: " + spawnTrashCount);
			// Calcular spawn aleatorio
			int spawn = Random.Range(0, spawnPositions.Length);
			// Calcular tipo de comida aleatorio
			int foodPosition = 5;
			if (spawnTrashCount != 0)
			{
				foodPosition = Random.Range(0, 6);
			}
			// Obtener comida
			GameObject[] food = getFoodType(foodPosition);
			// Aparecer comida
			spawnFood(food, spawn);
			// Reducir cuenta
			if (gameParameters.getDifficulty() != 1)
				spawnTrashCount -= 1;
			// Reiniciar timer
			timer = 0.0f;

			if (spawnTrashCount < 0)
			{
				spawnTrashCount = Random.Range(minProbability - 1, maxProbability + 1);
				Debug.Log("Espera: " + spawnTrashCount);
			}
		}
	}

	/*
	 * Establecer juego en Fácil
	 */
	private void setEasyGame()
	{
		// Pausar juego
		GetComponent<Pause>().PauseGame();
		// Activar pantalla
		blackScreenAnimator.gameObject.SetActive(true);
		blackScreenAnimator.SetTrigger("StartGame");
	}

	/*
	 * Establecer juego en Normal
	 */
	private void setNormalGame()
	{
		// Pausar juego
		GetComponent<Pause>().PauseGame();
		// Seleccionar alimento
		selectedFoodNum = Random.Range(0, 5);
		selectedFood = getFoodType(selectedFoodNum)[0].GetComponent<FoodType>().getFoodType();
		// Preparar pantalla
		foodText.text = selectedFood;
		preparationScreen.SetActive(true);
	}

	/*
	 * Obtener tipo de comida
	 */
	private GameObject[] getFoodType(int position)
	{
		switch (position)
			{
				case 0:
					return cereales;
				case 1:
					return frutas;
				case 2:
					return leguminosas;
				case 3:
					return origenAnimal;
				case 4:
					return verduras;
				case 5:
					return chatarra;
				default:
					Debug.LogError("Tipo de comida fuera de rango");
					return chatarra;
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
	 * Agregar un punto
	 */
	private void addPoint()
	{
		// SFX
		sourceCatch.Play();
		playerScore += 1;
		// Actualizar UI
		playerScoreText.GetComponent<Text>().text = "Puntos: " + playerScore;
	}

	/*
	 * Eliminar corazón
	 */
	private void deleteHeart()
	{
		// SFX
		sourceHit.Play();
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
		// Fácil
		if (gameParameters.getDifficulty() == 0)
		{
			if (type != "Chatarra")
			{
				addPoint();
			}
			else
			{
				// Vibrar teléfono
				Handheld.Vibrate();
				// Eliminar corazón
				deleteHeart();
			}
		}
		// Normal
		else if (gameParameters.getDifficulty() == 1)
		{
			if (type.Equals(selectedFood))
			{
				addPoint();
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
}
