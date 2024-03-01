using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();

        col.size = new Vector2(sp.size.x, sp.size.y / 3);
        col.offset = new Vector2(0, -sp.size.y/3);
    }
}
