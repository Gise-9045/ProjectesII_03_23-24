using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ColorReceiver : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerMovement boolManaging;
   
   
    private void Awake()
    {
      // boolManaging = GetComponent<PlayerMovement>();

    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ColorChange"))
        {
            Debug.Log("BEEP");
            ChangeColor(collision.collider.GetComponent<ColorSpreader>().GetColor());

            //ENCONTRAR MANERA MAS LIMPIA DE HACER ESTO
            if(collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.BLUE ) {
                boolManaging.SetDoubleJump(true);
                boolManaging.SetDash(false);
            }
            else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.GREEN)
            {
                boolManaging.SetDash(true);
                boolManaging.SetDoubleJump(false);
                boolManaging.SetPickUp(false);
            }
            else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.RED)
            {
                boolManaging.SetDash(false);
                boolManaging.SetDoubleJump(false);
                boolManaging.SetPickUp(true);
            }
            else if (collision.collider.GetComponent<ColorSpreader>().GetColorType() == ColorTypes.NULL)
            {
                boolManaging.SetDash(false);
                boolManaging.SetDoubleJump(false);
                boolManaging.SetPickUp(false);
            }
            else
            {
                boolManaging.SetDoubleJump(false);
                boolManaging.SetDash(false);
                boolManaging.SetPickUp(false);

            }
        }
    }
    public void ChangeColor(Color color)
    {
        if (color == null) return;
        _spriteRenderer.color = color;
    }
}

