using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Parámetros")]
    public int playerScore;
    public float timeBetweenSpawn = 1.0f;
    [Header("UI")]
    public GameObject playerScoreText;
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

    // Otras variables
    private float timer;

    /*
     * Iniciar juego
     */
    private void Start()
    {
        // Instanciar el puntaje
        playerScore = 0;
        timer = 0;
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
                    spawnFood(ref cereales, spawn);
                    break;
                case 1:
                    spawnFood(ref chatarra, spawn);
                    break;
                case 2:
                    spawnFood(ref frutas, spawn);
                    break;
                case 3:
                    spawnFood(ref leguminosas, spawn);
                    break;
                case 4:
                    spawnFood(ref origenAnimal, spawn);
                    break;
                case 5:
                    spawnFood(ref verduras, spawn);
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
    private void spawnFood(ref GameObject[] food, int spawnPosition)
    {
        int foodPosition = Random.Range(0, food.Length);
        // Instanciar elemento
        Instantiate(food[foodPosition], spawnPositions[spawnPosition].transform.position, spawnPositions[spawnPosition].transform.rotation);
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
     * Método a llamar cuando el jugador atrapa comida
     */
    public void cachedFood(string type)
    {
        if (type != "Chatarra")
        {
            setScore(1);
        }
        else
        {
            // Reducir la puntuación por atrapar comida chatarra
            setScore(-1);
        }
    }
}
