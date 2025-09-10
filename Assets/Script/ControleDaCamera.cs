using TMPro;
using UnityEngine;

public class ControleDaCamera : MonoBehaviour
{

    public float velocidade = 5;
    float velocidadeCorrendo;
    float velocidadeAndando;

    Vector3 PosicaoInicial;

    public float velocidadeMaxima = 5;
    public float distanciaRaycast = 0.6f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidadeAndando = velocidade;
        velocidadeCorrendo = velocidade * 2;
        PosicaoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidade = velocidadeCorrendo;
        }
        else
        {
            velocidade = velocidadeAndando;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0, Time.deltaTime * velocidade, 0);
        }

        if (Input.GetKey(KeyCode.E) == true)
        {
            transform.position += new Vector3(0, Time.deltaTime * velocidade, 0);
        }

        if (Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.position += new Vector3(0, 0, Time.deltaTime * velocidade);
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            transform.position += new Vector3(0, 0, -(Time.deltaTime * velocidade));
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            transform.position += new Vector3(-(Time.deltaTime * velocidade), 0, 0);
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            transform.position += new Vector3(Time.deltaTime * velocidade, 0, 0);
        }

        if(Input.GetKey(KeyCode.Space) == true)
        {
            transform.position = PosicaoInicial;
        }

    }

        private void OnTriggerEnter2D(Collider2D contato)
        {
            if (contato.name.Contains("PI"))
            {
            }

            if (contato.name.Contains("Fim"))
            {
                Debug.ClearDeveloperConsole();
                Debug.Log("------- PARAB�NS, VOC� TERMINOU O JOGO -------");
            }

    }


}

