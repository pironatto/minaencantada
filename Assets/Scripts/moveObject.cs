using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{

    private Rigidbody2D RbObjeto;
    public float    forca;
    private GameObject temp;
    public float forcaGiro;
    public GameObject objeto;

    // Start is called before the first frame update
    void Start()
    {
        RbObjeto = GetComponent<Rigidbody2D>();
        RbObjeto.AddForce(new Vector2(0,forca));
      
        
    }

    // Update is called once per frame
    void Update()
    {
        //PARA ROTACIONAR O OBJETO
        float smooth = Time.deltaTime * forcaGiro;
         transform.Rotate(new Vector3(0,0,2) * smooth);

         if (transform.position.y >= 4.5f)
         {
            forca =0;
            forcaGiro = 0;
           objeto.transform.position = new Vector2(transform.position.x,4.5f);
         }
   }
}
