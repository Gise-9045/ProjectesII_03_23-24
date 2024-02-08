using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorReceiver : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ColorChange"))
        {
            Debug.Log("BEEP");
            ChangeColor(collision.collider.GetComponent<ColorSpreader>().GetColor());
        }
    }
    public void ChangeColor(Color color)
    {
        if (color == null) return;
        _spriteRenderer.color = color;
    }
}

