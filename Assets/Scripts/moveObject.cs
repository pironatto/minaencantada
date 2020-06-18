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

        if (transform.position.y >= 4.5f)
        {
            forca = 0;
            forcaGiro = 0;
            transform.position = new Vector2(transform.position.x, 4.5f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine("TempoPicareta");
        }
    }

    IEnumerator TempoPicareta()
    {
        yield return new WaitForSeconds(5);
        _gameControle.isPicareta = false;
        Destroy(this.gameObject);
    }




}




