using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ColorReceiver : MonoBehaviour
{
   [SerializeField] private SpriteRenderer _spriteRenderer;
    private PlayerMovement boolManaging;
   
   
    private void Awake()
    {
       boolManaging = GetComponent<PlayerMovement>();

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
            }
            else
            {
                boolManaging.SetDoubleJump(false);
            }
        }
    }
    public void ChangeColor(Color color)
    {
        if (color == null) return;
        _spriteRenderer.color = color;
    }
}

