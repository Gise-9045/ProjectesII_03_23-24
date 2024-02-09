using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();

        col.size = sp.size;
    }
}
