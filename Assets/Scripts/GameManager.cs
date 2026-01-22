using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool iniciarDirecto = false;

    [Header("UI")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuJuegoTerminado;
    [SerializeField] private GameObject menuGanaste;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game")]
    [SerializeField] private GameObject player;

    [Header("Vidas (Jugador)")]
    [SerializeField] private SistemaVidas sistemaVidasJugador;
    private bool gameOverLanzado = false;

    [Header("Puntos")]
    [SerializeField] private int puntosParaGanar = 50;
    private int puntos = 0;

    private void Start()
    {
        gameOverLanzado = false;

        if (iniciarDirecto)
        {
            // Venimos de un "Reintentar"
            menuPrincipal.SetActive(false);
            menuJuegoTerminado.SetActive(false);

            player.SetActive(true);

            // Importantísimo: reseteamos el flag
            iniciarDirecto = false;
        }
        else
        {
            // Inicio normal (cuando abres el juego / escena por primera vez)
            menuPrincipal.SetActive(true);
            menuJuegoTerminado.SetActive(false);

            player.SetActive(false);
        }

        if (menuGanaste != null)
        {
            menuGanaste.SetActive(false);
        }

        CacheSistemaVidasJugador();
        ActualizarUI();
    }

    private void Update()
    {
        if (gameOverLanzado) return;
        if (player == null || !player.activeSelf) return;

        if (sistemaVidasJugador != null && sistemaVidasJugador.EstaMuerto)
        {
            gameOverLanzado = true;
            GameOver();
        }
    }

    private void CacheSistemaVidasJugador()
    {
        if (sistemaVidasJugador != null) return;
        if (player == null) return;

        SistemaVidas[] vidasEncontradas = player.GetComponentsInChildren<SistemaVidas>(true);

        if (vidasEncontradas == null || vidasEncontradas.Length == 0) return;

        // Preferimos el que tenga tag PlayerHitBox
        for (int i = 0; i < vidasEncontradas.Length; i++)
        {
            if (vidasEncontradas[i] != null && vidasEncontradas[i].CompareTag("PlayerHitBox"))
            {
                sistemaVidasJugador = vidasEncontradas[i];
                return;
            }
        }

        // Si no hay tag, tomamos el primero
        sistemaVidasJugador = vidasEncontradas[0];
    }

    public void OnPlayButton()
    {
        // Cerrar Menu
        menuPrincipal.SetActive(false);

        // Activar Player
        player.SetActive(true);

        // Aseguramos referencia a vida y reseteamos flag
        CacheSistemaVidasJugador();
        gameOverLanzado = false;
    }

    public void GameOver()
    {
        menuJuegoTerminado.SetActive(true);

        if (player != null)
        {
            player.SetActive(false);
        }
    }

    public void OnRestartButton()
    {
        iniciarDirecto = true;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();

        if (puntos >= puntosParaGanar)
        {
            Ganaste();
        }
    }

    private void ActualizarUI()
    {
        if (scoreText != null)
            scoreText.text = $"Puntos: {puntos}/{puntosParaGanar}";
    }

    public void Ganaste()
    {
        if (menuGanaste != null) menuGanaste.SetActive(true);

        // detener juego (simple)
        if (player != null) player.SetActive(false);
    }
}
