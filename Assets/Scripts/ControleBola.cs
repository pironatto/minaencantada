using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleBola : MonoBehaviour { 

    private Vector2 lastFrameVelocidade;
    private Rigidbody2D bolaRb;
    public float Velocidade,VelocidadeInicial;
    public GameObject prefabExplosao;
    public GameObject[] Itens;

 
    // Start is called before the first frame update
    void Start()
    {
        bolaRb = GetComponent<Rigidbody2D>();
        bolaRb.velocity = new Vector2(VelocidadeInicial,0);
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
        if (collision.gameObject.tag == "Bola")
        {

            if (GetInstanceID() < collision.gameObject.GetInstanceID())
            {
               GameObject temp = Instantiate(prefabExplosao, transform.position, transform.rotation);
                Destroy (temp,0.5f);

                
                int IdItens = Random.Range(0,10);
                if (IdItens < 2)
                {
               Instantiate(Itens[0],transform.position,transform.rotation); 
                }
                else if(IdItens >8){
               Instantiate(Itens[1],transform.position,transform.rotation);
                }
                
            }
           
            Destroy(this.gameObject);                      
        }

    }

    void Bater(Vector2 colNormal)
    {
        var speed = lastFrameVelocidade.magnitude;
        var dir = Vector2.Reflect(lastFrameVelocidade.normalized, colNormal);
        bolaRb.velocity = dir * Mathf.Max(speed, Velocidade);
    }


}
