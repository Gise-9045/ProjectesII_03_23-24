using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
   private SpriteRenderer _spriteRenderer;
   
   ColorTypes actualColor;
   
    void Start ()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

   public ColorTypes GetPowerUp()
   {
        return actualColor;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ColorChange"))
        {
            //Debug.Log("BEEP");
            Color color = collision.collider.GetComponent<ColorSpreader>().GetColor();
            
            if (color == null) 
            {
                return;
            }

            actualColor = collision.collider.GetComponent<ColorSpreader>().GetColorType();
            _spriteRenderer.color = color;
        }
    }
}

