using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    //IDEA: hacer que aparezca un mensaje en pantalla cuando el jugador se acerque a un objeto interactuable
    //      y que se pueda interactuar con él pulsando una tecla, la E.
    public float interactRange;  

    private PlayerStats playerStats;
    private Rigidbody2D _physics;

    [SerializeField] private Transform interactiveCheck;

    private PlayerJumpController playerJumpController;
 
    // Start is called before the first frame update
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerJumpController = GetComponent<PlayerJumpController>();
        _physics = GetComponent<Rigidbody2D>();
    }
   

    public void Interact()
    {
        Collider2D interactedCollider = Physics2D.OverlapCapsule(_physics.position, interactiveCheck.localScale, CapsuleDirection2D.Horizontal, 0);

        if (interactedCollider != null)
        {
            if (interactedCollider.tag == "Lever")
            {
                interactedCollider.GetComponent<leverActivation>().Toggle();
            }

            if (interactedCollider.tag == "ItemDash")
            {
                Debug.Log("ItemDash");
                playerStats.hasDashPowerUp = true;

                // Puedes destruir el objeto ItemDash aquí si también deseas
                Destroy(interactedCollider.gameObject);
            }

            if (interactedCollider.tag == "ItemJump")
            {
                playerStats.hasJumpPowerUp = true;
                playerJumpController.maxJump = 2;

                Destroy(interactedCollider.gameObject);

            }

            if (interactedCollider.tag == "ItemShout")
            {
                playerStats.hasShoutPowerUp = true;

                Destroy(interactedCollider.gameObject);

            }
        }
    }
}
