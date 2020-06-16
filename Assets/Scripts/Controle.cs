using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour
{

    public GameObject prefabMina;
    public GameObject[] prefabBola;
    private GameObject tempMina;
    public Transform A, B, C, D, E, F, G, H;
    private Transform spawPosicao;
    private Animator minaAnimator;
    public float tempoAberto,intervaloSpawn, tempoSpawnBola;
    private bool MinaAberta;



    // Start is called before the first frame update
    void Start()
    {
        minaAnimator = GetComponent<Animator>();
        StartCoroutine("SpawnMina");

    }

    // Update is called once per frame
    void Update()
    {
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
        
       

    }
    IEnumerator SpawnMina()
    {
        yield return new WaitForSeconds(0);
        tempMina = Instantiate(prefabMina, spawPosicao.position, transform.rotation);
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
            int idBola = Random.Range(0, prefabBola.Length);            
            Instantiate(prefabBola[idBola], new Vector2(tempMina.transform.position.x,tempMina.transform.position.y + 0.5f), transform.rotation);
            StartCoroutine("SpawnBola");
        }
    }



}

