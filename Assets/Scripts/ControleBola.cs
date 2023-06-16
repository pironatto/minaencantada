using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBola : MonoBehaviour
{

    private GameControle _gameControle;
    private Vector2 lastFrameVelocidade;
    private Rigidbody2D bolaRb;
    public float Velocidade, VelocidadeInicial;

    public bool isBateu;

    // Start is called before the first frame update
    void Start()
    {
        _gameControle = FindObjectOfType(typeof(GameControle)) as GameControle;
        bolaRb = GetComponent<Rigidbody2D>();
        bolaRb.velocity = new Vector2(VelocidadeInicial, 0);
        isBateu = false;
    }

    // Update is called once per frame
    void Update()
    {
        lastFrameVelocidade = bolaRb.velocity;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "fundo")
        {
            Bater(collision.contacts[0].normal);
        }
        //PARA GERAR A EXPLOSAO AO ENCONTRAR OUTRA BOLA
        if (collision.gameObject.tag == "Bola")
        {
            if (GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                _gameControle.tempExplosao = Instantiate(_gameControle.prefabs[3], transform.position, transform.rotation);
                _gameControle.StartCoroutine("SpawnPicareta");
                Destroy(_gameControle.tempExplosao, 0.5f);
            }
            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }



    void Bater(Vector2 colNormal)
    {
        var speed = lastFrameVelocidade.magnitude;
        var dir = Vector2.Reflect(lastFrameVelocidade.normalized, colNormal);
        bolaRb.velocity = dir * Mathf.Max(speed, Velocidade);
    }





}
