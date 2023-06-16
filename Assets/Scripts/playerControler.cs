using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour
{
    private Rigidbody2D RbPlayer;
    private SpriteRenderer SrPlayer;
    public Sprite[] Personagem;
    public float velocidade, velocidadeY;
    public float forcaPulo;
    public string objetoColisao;
    public Transform groundCheckR, groundCheckL;
    private bool isGround;
    public LayerMask whatIsGround;
    private GameControle _gameControle;
    private bool isLeft;



    // Start is called before the first frame update
    void Start()
    {
        _gameControle = FindObjectOfType(typeof(GameControle)) as GameControle;
        RbPlayer = GetComponent<Rigidbody2D>();
        SrPlayer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapArea(groundCheckL.position, groundCheckR.position, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        if (isLeft == true & h > 0)
        {
            flip();
        }
        if (isLeft == false & h < 0)
        {
            flip();
        }

        RbPlayer.velocity = new Vector2(h * velocidade, RbPlayer.velocity.y);

        if (Input.GetButtonDown("Jump") && isGround == true)
        {
            RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, 0);
            RbPlayer.AddForce(new Vector2(0, forcaPulo));
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Escada")
        {
            float v = Input.GetAxis("Vertical");
            RbPlayer.velocity = new Vector2(RbPlayer.velocity.x, v * velocidadeY);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //PEGAR PICARETA
        if (col.gameObject.tag == "Picareta")
        {
            SrPlayer.sprite = Personagem[0];
            _gameControle.isPicareta = true;
            Destroy(_gameControle.tempPicareta);
            StartCoroutine("TempoPicareta");

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        objetoColisao = col.gameObject.name;

        //MORTE DO PERSONAGEM
        if (col.gameObject.tag == "Bola")
        {
            //Destroy(this.gameObject);
            //StartCoroutine("RecarregaCena");
        }

    }

    IEnumerator RecarregaCena()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    IEnumerator TempoPicareta()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine("Piscar");

        yield return new WaitForSeconds(4);
        _gameControle.isPicareta = false;
        SrPlayer.sprite = Personagem[1];
        StopCoroutine("Piscar");

    }


    IEnumerator Piscar()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
        StartCoroutine("Piscar");
    }

    // PARA VIRAR O PERSONAGEM
    void flip()
    {
        isLeft = !isLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }



}
