using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Parámetros")]
    public int playerScore;
    [Header("UI")]
    public GameObject playerScoreText;
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

    /*
     * Iniciar juego
     */
    private void Start()
    {
        // Instanciar el puntaje
        playerScore = 0;
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
