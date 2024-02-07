using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorReceiver : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChange"))
        {
            Debug.Log("BEEP");
            ChangeColor(collision.GetComponent<ColorSpreader>().GetColor());
        }
    }
    public void ChangeColor(Color color)
    {
        if (color == null) return;
        _spriteRenderer.color = color;
    }
}

