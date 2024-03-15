using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerLogic : MonoBehaviour
{
    [SerializeField]private float velocity;
    [SerializeField] private bool flip;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite endSprite;
    [SerializeField] private StopConveyor stopConveyor;
    [SerializeField] private FlipConveyor flipConveyor;

    private BoxCollider2D col;
    private float dt;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
       
        col.size = new Vector2(sp.size.x, sp.size.y);
        
    }
    private void Update()
    {
        if (stopConveyor.GetIsACtive())
        {
            dt = 0;
        }
        else
        {
            dt = Time.deltaTime;
        }
        flip = flipConveyor.GetIsACtive();
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (flip)
        {
            collision.transform.localPosition += Vector3.right * velocity * dt;

        }
        else
        {
            collision.transform.localPosition += Vector3.left * velocity * dt;

        }

    }
}
