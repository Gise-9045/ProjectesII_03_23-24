using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    //IDEA: hacer que aparezca un mensaje en pantalla cuando el jugador se acerque a un objeto interactuable
    //      y que se pueda interactuar con él pulsando una tecla, la E.
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lever")
        {
            collision.GetComponent<leverActivation>().Toggle();
        }
    }
}
