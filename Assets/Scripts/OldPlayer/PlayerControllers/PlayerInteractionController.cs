using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    //IDEA: hacer que aparezca un mensaje en pantalla cuando el jugador se acerque a un objeto interactuable
    //      y que se pueda interactuar con él pulsando una tecla, la E.
    public float interactRange;
    private bool interacting = false;
    private PlayerStats playerStats;
    private Rigidbody2D _physics;

    [SerializeField] private Transform interactiveCheck;
    [Header("Dialog Box")]
    public GameObject dialogBoxPrefab;
    private GameObject currentDialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject itemContainer;
    private PlayerJumpController playerJumpController;

    // Start is called before the first frame update
    void Awake()
    {
        if (dialogText != null)
        {
            dialogText.gameObject.SetActive(false);

        }
        playerStats = GetComponent<PlayerStats>();
        playerJumpController = GetComponent<PlayerJumpController>();
        _physics = GetComponent<Rigidbody2D>();
    }


    public void Interact()
    {
        interacting = true;
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
                DisplayPopup("Dash Power-Up Acquired");
                Destroy(interactedCollider.gameObject);
                playerStats.hasDashPowerUp = true;

                // Puedes destruir el objeto ItemDash aquí si también deseas
                Destroy(interactedCollider.gameObject);
            }

            if (interactedCollider.tag == "ItemJump")
            {
                playerStats.hasJumpPowerUp = true;
                DisplayPopup("Jump Power-Up Acquired");
                Destroy(interactedCollider.gameObject);

            }

            if (interactedCollider.tag == "ItemShout")
            {
                playerStats.hasShoutPowerUp = true;

                Destroy(interactedCollider.gameObject);

            }
        }
    }
    void DisplayPopup(string message)
    {
        if (dialogText != null)
        {
            dialogText.text = message;
            dialogText.gameObject.SetActive(true); // Show the popup
            itemContainer.gameObject.SetActive(true);
            // You can modify this duration as needed
            StartCoroutine(HidePopup(2f)); // Hide after 2 seconds
        }
    }

    IEnumerator HidePopup(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Destroy(dialogBoxPrefab);
        Destroy(currentDialogBox);
        Destroy(dialogText);

        //itemContainer.gameObject.SetActive(false);
        //Destroy(currentDialogBox2); // Destroy the dialog box after the delay
        //itemContainer2.gameObject.SetActive(false);

    }
    private void OnCollisionEnter2D(Collision2D interactedCollider)
    {
        if(interacting) { 
        if (interactedCollider != null)
        {
            if (interactedCollider.gameObject.CompareTag("Lever"))
            {
                interactedCollider.gameObject.GetComponent<leverActivation>().Toggle();
                    interacting = false;
            }

                if (interactedCollider.collider.tag == "ItemDash")
                {
                    playerStats.hasDashPowerUp = true;
                    Debug.Log("ItemDash");
                    DisplayPopup("Dash Power-Up Acquired");
                    Destroy(interactedCollider.gameObject);
                    

                    // Puedes destruir el objeto ItemDash aquí si también deseas
                    Destroy(interactedCollider.gameObject);
                }

                if (interactedCollider.collider.tag == "ItemJump")
                {
                    playerStats.hasJumpPowerUp = true;
                    DisplayPopup("Jump Power-Up Acquired");
                    Destroy(interactedCollider.gameObject);

                }

                if (interactedCollider.gameObject.CompareTag("ItemShout"))
            {
                playerStats.hasShoutPowerUp = true;
                    interacting = false;
                    Destroy(interactedCollider.gameObject);

            }
        }
        }
    }
}
