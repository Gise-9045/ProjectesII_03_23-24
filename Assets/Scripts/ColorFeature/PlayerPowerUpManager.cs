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




            //BORRAR
            // if(collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.BLUE ) {
            //     boolManaging.SetDoubleJump(true);
            //     boolManaging.SetDash(false);
            // }
            // else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.GREEN)
            // {
            //     boolManaging.SetDash(true);
            //     boolManaging.SetDoubleJump(false);
            //     boolManaging.SetPickUp(false);
            // }
            // else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.PINK)
            // {
            //     boolManaging.SetDash(false);
            //     boolManaging.SetDoubleJump(false);
            //     boolManaging.SetPickUp(true);
            // }
            // else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.NULL)
            // {
            //     boolManaging.SetDash(false);
            //     boolManaging.SetDoubleJump(false);
            //     boolManaging.SetPickUp(false);
            // }
            // else
            // {
            //     boolManaging.SetDoubleJump(false);
            //     boolManaging.SetDash(false);
            //     boolManaging.SetPickUp(false);

            // }
        }
    }
}

