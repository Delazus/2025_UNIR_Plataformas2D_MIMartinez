using UnityEngine;

public class UIProximidad : MonoBehaviour
{
    [SerializeField] private GameObject panelUI;

    void Start()
    {
        if (panelUI != null)
            panelUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Usa el tag que ya tienes en tu jugador. Te dejo compatibles los 3 más comunes.
        if (other.CompareTag("PlayerHitBox") )
        {
            if (panelUI != null)
                panelUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            if (panelUI != null)
                panelUI.SetActive(false);
        }
    }
}

