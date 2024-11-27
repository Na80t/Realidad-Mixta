using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar el juego
using TMPro; // Para manejar el texto del contador de vidas

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton para acceso global

    public GameObject[] hearts;          // Los objetos de imagen de los corazones en la UI
    public TextMeshProUGUI livesText;    // Texto opcional para mostrar el número de vidas
    public int lives = 3;                // Número inicial de vidas
    public GameObject gameOverMenu;      // Menú de Game Over

    private void Awake()
    {
        // Implementación del singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destruye duplicados
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;  // Asegúrate de que el juego comience en tiempo normal
        UpdateLivesUI();  // Actualiza el texto y corazones al iniciar
    }

    // Método para restar una vida cuando una rata alcanza el queso
    public void RatReachedCheese()
    {
        if (lives > 0)
        {
            lives--;  // Reduce las vidas
            UpdateLivesUI();  // Actualiza las vidas visualmente
        }

        if (lives <= 0)
        {
            GameOver();  // Llama al Game Over cuando las vidas se terminan
        }
    }

    private void UpdateLivesUI()
    {
        // Actualizar corazones: desactiva los corazones según el número de vidas restantes
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);  // Activa/desactiva corazones
        }

        // Actualizar el texto (opcional)
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
    }

    // Método que se llama cuando se termina el juego
    private void GameOver()
    {
        gameOverMenu.SetActive(true);  // Muestra el menú de Game Over
        Time.timeScale = 0f;  // Pausa el juego
    }

    // Reinicia el juego
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Restablece el tiempo normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reinicia la escena actual
    }
}
