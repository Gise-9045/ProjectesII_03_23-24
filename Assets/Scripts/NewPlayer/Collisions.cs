using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collisions : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    public bool collectingJump;
    public bool collectingDash;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    [Header("Dialog Box")]
    public GameObject dialogBoxPrefab; 
    
    private GameObject currentDialogBox;
   
    public TextMeshProUGUI dialogText;
   
    public GameObject itemContainer;
    

    private Rigidbody2D _physics; 

    // Start is called before the first frame update
    void Start()
    {
        dialogText.gameObject.SetActive(false);
        if (dialogText != null)
        {
            dialogText.gameObject.SetActive(false);
            if (itemContainer != null)
            {
                itemContainer.SetActive(true);
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ItemJump"))
        {
            collectingJump = true;
            DisplayPopup("Jump Power-Up Acquired");
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("ItemDash"))
        {
            collectingDash = true;
            DisplayPopup("Dash Power-Up Acquired");
            Destroy(collision.gameObject);
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
        
        
            Destroy(currentDialogBox); // Destroy the dialog box after the delay
            itemContainer.gameObject.SetActive(false);
            
        
    }
}