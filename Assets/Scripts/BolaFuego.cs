
using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private float impulsoDisparo;
    [SerializeField] private float danhoAtaque;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //transform.forward --> Mi Eje Z. (AZUL)
        //transform.forward --> Mi Eje Y. (Verde)
        //transform.forward --> Mi Eje X. (ROJO)
        rb2D.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }
    }
}
