using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    public int lifes;
    [SerializeField] private GameObject[] corazones;

    [Header("Conexion con la vida real del jugador")]
    [SerializeField] private SistemaVidas sistemaVidasJugador;

    // 90 vida total -> cada 30 se pierde un corazon (3 corazones)
    [SerializeField] private float vidaPorCorazon = 30f;

    void Start()
    {
        if (sistemaVidasJugador == null)
        {
            GameObject hitbox = GameObject.FindGameObjectWithTag("PlayerHitBox");
            if (hitbox != null)
            {
                sistemaVidasJugador = hitbox.GetComponent<SistemaVidas>();
            }
        }
    }
    void Update()
    {
        if (sistemaVidasJugador != null)
        {
            lifes = Mathf.CeilToInt(sistemaVidasJugador.VidaActual / vidaPorCorazon);
            lifes = Mathf.Clamp(lifes, 0, corazones.Length);
        }

        ShowHearth();
    }
    void ShowHearth()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lifes; i++)
        {
            corazones[i].gameObject.SetActive(true);
        }
    }
}
