using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class moveObject : MonoBehaviour
{

    private Rigidbody2D RbObjeto;
    public float forca;
    public float forcaGiro;
    private GameControle _gameControle;
    private bool isPiscar = false;




    // Start is called before the first frame update
    void Start()
    {
        RbObjeto = GetComponent<Rigidbody2D>();
        RbObjeto.AddForce(new Vector2(0, forca));
        _gameControle = FindObjectOfType(typeof(GameControle)) as GameControle;

    }

    // Update is called once per frame
    void Update()
    {
        //PARA ROTACIONAR O OBJETO
        float smooth = Time.deltaTime * forcaGiro;
        transform.Rotate(new Vector3(0, 0, 2) * smooth);

        //PARA O OBJETO PARAR NO TOPO
        if (transform.position.y >= 3.5f)
        {
            forca = 0;
            forcaGiro = 0;
            transform.position = new Vector2(transform.position.x, 3.5f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine("TempoPicareta");
            
        }

    }

    IEnumerator TempoPicareta()
    {
        //PARA PERMITIR QUE O OBJETO PISQUE CORRETAMENTE
        yield return new WaitForSeconds(5);
        if (isPiscar == false)
        {
            StartCoroutine("Piscar");
            isPiscar = true;
        }
        yield return new WaitForSeconds(3);
        _gameControle.isPicareta = false;
        Destroy(this.gameObject);
    }


    IEnumerator Piscar()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
        StartCoroutine("Piscar");
    }




}




