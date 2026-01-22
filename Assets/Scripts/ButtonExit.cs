using UnityEngine;

public class ApagarEncenderPorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objetoAApagar;
    [SerializeField] private GameObject objetoAEncender;

    [Header("Cambiar sprite del objeto Trigger (solo 1 vez)")]
    [SerializeField] private SpriteRenderer spriteRendererDelTrigger;
    [SerializeField] private Sprite nuevoSprite;

    private bool yaSeActivo = false;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            if (objetoAApagar != null)
                objetoAApagar.SetActive(false);

            if (objetoAEncender != null)
                objetoAEncender.SetActive(true);

            if (!yaSeActivo)
            {
                yaSeActivo = true;

                if (spriteRendererDelTrigger != null && nuevoSprite != null)
                    spriteRendererDelTrigger.sprite = nuevoSprite;
            }
        }
    }
}

