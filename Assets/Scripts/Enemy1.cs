using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy1 : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danhoAtaque;
    private Vector3 destinoActual;
    private int indiceActual = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() //S = V * t
    {

    }

    IEnumerator Patrulla()
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            //Llegamos a este punto de código.
            DefinirNuevoDestino();
        }
    }

    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;  
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        if(destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;   
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("PlayerDeteccion"))
        {
            Debug.Log("Player detectado!!!");
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }
    }
}
