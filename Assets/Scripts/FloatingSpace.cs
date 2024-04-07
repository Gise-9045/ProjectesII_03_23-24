using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSpace : MonoBehaviour
{
    private BoxCollider2D spaceCollider;
    private SpriteRenderer spaceSprite;
    // Start is called before the first frame update
    void Start()
    {
        spaceCollider = GetComponent<BoxCollider2D>();
        spaceSprite = GetComponent<SpriteRenderer>();
        spaceCollider.size = new Vector2(spaceSprite.size.x, spaceSprite.size.y);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = 35;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 6f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().drag = 5f;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 9.81f;

        }
    }
}
