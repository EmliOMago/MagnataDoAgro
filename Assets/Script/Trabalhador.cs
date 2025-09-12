using System.Collections.Generic;
using UnityEngine;

public class Trabalhador : MonoBehaviour
{

    public List<Transform> pontos;
    public float velocidade = 5;

    Transform destino;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EscolherNovoDestino();
    }

    // Update is called once per frame
    void Update()
    {

        if (destino == null)
            return;

        if (Vector3.Distance(transform.position, destino.position) >= 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidade * Time.deltaTime);
        }
        else 
        {
            EscolherNovoDestino();
        }
    }

    void EscolherNovoDestino()
    {
        destino = pontos[Random.Range(0, pontos.Count)];
    }

}
