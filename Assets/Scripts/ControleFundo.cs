using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleFundo : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float width = Camera.main.orthographicSize * Screen.width / Screen.height;
        transform.localScale = new Vector3(width / 16, 0.5f, 0.5f);
    }
}
