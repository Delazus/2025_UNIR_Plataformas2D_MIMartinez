using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas = 90f;
    [SerializeField] private bool destruirAlMorir = true;

    public float VidaActual => vidas;
    public bool EstaMuerto => vidas <= 0f;

    private void Awake()
    {
        if (CompareTag("PlayerHitBox"))
        {
            destruirAlMorir = false;
        }
    }

    public void RecibirDanho(float danhoRecibido)
    {
        if (EstaMuerto) return;

        vidas -= danhoRecibido;

        if (vidas < 0f)
            vidas = 0f;

        if (EstaMuerto && destruirAlMorir)
        {
            Destroy(this.gameObject);
        }
    }
}
