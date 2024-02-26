using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    public Collider2D myCollider;
    [SerializeField] private PortalLogic PortalB;
    private bool canTeleport;
    private  WaitForSeconds delayBetweenPortals = new WaitForSeconds(0.5f);
    private GameObject objectBeingTeleported;
    [SerializeField] private GameObject lastEnteredPortal; // Referencia al último portal por el que entró el objeto

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (canTeleport && (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox")))
        {
            //StartCoroutine(TeleportDelay(collision.gameObject));

            canTeleport = false;
           // objectBeingTeleported = collision.gameObject;
            PassPortal(collision.gameObject);
           // lastEnteredPortal = gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canTeleport = true;
    }
    void PassPortal(GameObject obj)
    {
        
        obj.transform.position =PortalB.transform.position;
        //obj.transform.position = new Vector2((PortalB.transform.position.x+0.5f),PortalB.transform.position.y);
        //PortalB.StartTeleport(obj);
    }

    public void StartTeleport(GameObject obj)
    {
        if (obj == objectBeingTeleported)
        {
            StartCoroutine(TeleportDelay(obj));
        }
    }

    IEnumerator TeleportDelay(GameObject obj)
    {
        
        canTeleport = false;
        yield return delayBetweenPortals;
        if (lastEnteredPortal != gameObject)
        {            
            PortalB.myCollider.enabled = false;
            PassPortal(obj);
           
            PortalB.myCollider.enabled = true;
        }
    }
}
