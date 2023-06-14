using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControle : MonoBehaviour
{

    private ControleBola _controleBola;
    private playerControler _playerControle;
    public GameObject[] prefabs;

    private Transform spawPosicao;
    public Transform A, B, C, D, E, F;

    internal GameObject tempMina, tempBola, tempExplosao, tempPicareta, tempEscada;
    private Animator minaAnimator;
    private Animator escadaAnimator;
    public bool MinaAberta;
    public float tempoAberto, intervaloSpawn, tempoSpawnBola;

    public bool isPicareta;

    public float pos, posA;


    // Start is called before the first frame update
    void Start()
    {
        _controleBola = FindObjectOfType(typeof(ControleBola)) as ControleBola;
        _playerControle = FindObjectOfType(typeof(playerControler)) as playerControler;
        StartCoroutine("SpawnMina");
        StartCoroutine("SpawnEscada");

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnMina()
    {
        //PERMITIR QUE A MINA SPAWNE ALEATORIAMENTE EM VARIAS POSIÇÕES DETERMINADAS
        int posicao = Random.Range(0, 8);
        switch (posicao)
        {
            case 0:
                spawPosicao = A;
                break;
            case 1:
                spawPosicao = B;
                break;
            case 2:
                spawPosicao = C;
                break;
            case 3:
                spawPosicao = D;
                break;
            case 4:
                spawPosicao = E;
                break;
            case 5:
                spawPosicao = F;
                break;

        }

        //yield return new WaitForSeconds(0);
        tempMina = Instantiate(prefabs[0], spawPosicao.position, transform.rotation);
        MinaAberta = true;
        StartCoroutine("SpawnBola");

        yield return new WaitForSeconds(tempoAberto);
        minaAnimator = tempMina.GetComponent<Animator>();
        minaAnimator.SetTrigger("closed");
        MinaAberta = false;
        Destroy(tempMina, 0.65f);

        yield return new WaitForSeconds(intervaloSpawn);

        StartCoroutine("SpawnMina");
    }



    IEnumerator SpawnBola()
    {
        if (MinaAberta == true)
        {
            yield return new WaitForSeconds(tempoSpawnBola);

            // PERMITIR O SPAWN DE BOLAS COM EFEITOS DIFERENTES
            int idBola = Random.Range(0, 10);
            if (idBola < 5)
            {
                tempBola = Instantiate(prefabs[1], new Vector2(tempMina.transform.position.x, tempMina.transform.position.y + 0.5f), transform.rotation);
            }
            else if (idBola > 5)
            {
                Instantiate(prefabs[2], new Vector2(tempMina.transform.position.x, tempMina.transform.position.y + 0.5f), transform.rotation);
            }

            StartCoroutine("SpawnBola");
        }
    }

    void SpawnPicareta()
    {
        if (isPicareta == false)
        {
            tempPicareta = Instantiate(prefabs[4], tempExplosao.transform.position, tempExplosao.transform.rotation);
            isPicareta = true;
        }

    }

    IEnumerator SpawnEscada()
    {
        yield return new WaitForSeconds(1);
        switch (_playerControle.objetoColisao)
        {
            case "plataforma":
                posA = -3.32f;
                break;
            case "plataforma1":
                posA = -1.82f;
                break;
            case "plataforma2":
                posA = -0.31f;
                break;
            case "plataforma3":
                posA = 1.17f;
                break;
            case "plataforma4":
                posA = 2.67f;
                break;


        }


        {
            pos = Random.Range(-6.2f, 6.2f);
            tempEscada = Instantiate(prefabs[5], new Vector2(pos, posA), transform.rotation);
        }
        yield return new WaitForSeconds(5);
        minaAnimator = tempEscada.GetComponent<Animator>();
        minaAnimator.SetTrigger("Fecha");
        Destroy(tempEscada, 0.3f);
        yield return new WaitForSeconds(2);
        StartCoroutine("SpawnEscada");
    }



}
