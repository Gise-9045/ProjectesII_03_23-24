using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLogic : MonoBehaviour
{
    public Collider2D myCollider;
    [SerializeField] private PortalLogic PortalB;
    private bool isTeleporting;
    private GameObject objectBeingTeleported;
    [SerializeField] private GameObject lastEnteredPortal; // Referencia al último portal por el que entró el objeto

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTeleporting && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PuzzleBox")))
        {
            objectBeingTeleported = collision.gameObject;
            PassPortal(collision.gameObject);
            lastEnteredPortal = gameObject;
        }
    }

    void PassPortal(GameObject obj)
    {
        isTeleporting = true;
      
        obj.transform.position = new Vector2(PortalB.transform.position.x+1,PortalB.transform.position.y);
        PortalB.StartTeleport(obj);
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
        yield return new WaitForSeconds(0.1f);
        isTeleporting = false;
        if(lastEnteredPortal != null)
        {
            PassPortal(obj);
        }
    }
}
