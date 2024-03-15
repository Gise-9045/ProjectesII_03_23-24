using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerLogic : MonoBehaviour
{
    [SerializeField]private float velocity;
    [SerializeField] private bool flip;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite endSprite;

    private BoxCollider2D col;
    private SpriteRenderer sp;
    private PlatformEffector2D platform;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();

        col.size = new Vector2(sp.size.x, sp.size.y);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (flip)
        {
            collision.transform.localPosition += Vector3.right * velocity * Time.deltaTime;

        }
        else
        {
            collision.transform.localPosition += Vector3.left * velocity * Time.deltaTime;

        }

    }
}
