using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControle : MonoBehaviour
{

    private ControleBola _controleBola;
    public GameObject[] prefabs;

    private Transform spawPosicao;
    public Transform A, B, C, D, E, F, G, H;

    internal GameObject tempMina, tempBola, tempExplosao,tempPicareta;
    private Animator minaAnimator;
    public bool MinaAberta;
    public float tempoAberto, intervaloSpawn, tempoSpawnBola;

    public bool isPicareta;
   

    // Start is called before the first frame update
    void Start()
    {
        _controleBola = FindObjectOfType(typeof(ControleBola)) as ControleBola;
        StartCoroutine("SpawnMina");
        

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
            case 6:
                spawPosicao = G;
                break;
            case 7:
                spawPosicao = H;
                break;

        }

        yield return new WaitForSeconds(0);
        tempMina = Instantiate(prefabs[0], spawPosicao.position, transform.rotation);
        MinaAberta = true;
        StartCoroutine("SpawnBola");

        yield return new WaitForSeconds(tempoAberto);
        minaAnimator = tempMina.GetComponent<Animator>();
        minaAnimator.SetTrigger("closed");
        MinaAberta = false;

        yield return new WaitForSeconds(intervaloSpawn);
        Destroy(tempMina);
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





}
