using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
   private SpriteRenderer _spriteRenderer;
   
   ColorTypes actualColor;
   Color actualColorRGB;
   
    void Start ()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        actualColor = ColorTypes.NULL;
        actualColorRGB = new Color(255, 255, 255, 1);
    }

   public ColorTypes GetPowerUp()
   {
        return actualColor;
   }

   public Color GetColorRGB()
   {
        return actualColorRGB;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ColorChange"))
        {
            //Debug.Log("BEEP");
            actualColorRGB = collision.collider.GetComponent<ColorSpreader>().GetColor();
            
            if (actualColorRGB == null) 
            {
                return;
            }

            actualColor = collision.collider.GetComponent<ColorSpreader>().GetColorType();
            _spriteRenderer.color = actualColorRGB;
        }
    }
}

