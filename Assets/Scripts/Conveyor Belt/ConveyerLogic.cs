using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerLogic : MonoBehaviour
{
    [SerializeField] private float velocity = 100;
    [SerializeField] private bool goesLeft = true;
    [SerializeField] private SpriteRenderer conveyorSprite;
    [SerializeField] public SpriteRenderer directionSprite;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private BoxCollider2D effCol;
    [SerializeField] private AreaEffector2D effector;

    //Buttons
    [SerializeField] private StopConveyor stopConveyor;
    [SerializeField] private FlipConveyor flipConveyor;
    [SerializeField] private Animator animator;

    void Start()
    {
        col.size = new Vector2(conveyorSprite.size.x, col.size.y);
        effCol.size = new Vector2(conveyorSprite.size.x, col.size.y);
        StartMoving();
        effector.forceAngle = goesLeft ? 180f : 0f;
        directionSprite.flipX = !goesLeft;
        conveyorSprite.flipX = !goesLeft;
    }

    private void Update()
    {
        if (flipConveyor != null && flipConveyor.GetIsToggled())
        {
            Flip();
            flipConveyor.SetisToggled(false);
        }
        if (stopConveyor != null && stopConveyor.GetIsACtive())
        {
            StopMoving();
        }
        else
        {
            StartMoving();
        }
    }


    private void StopMoving()
    {
        effector.forceMagnitude = 0;
        animator.speed = 0f;
    }

    private void StartMoving()
    {
        effector.forceMagnitude = velocity;
        animator.speed = velocity / 65; //100 is the base speed for the animator (mentira)
    }

    private void Flip()
    {
        goesLeft = !goesLeft;
        effector.forceAngle = goesLeft ? 180f : 0f;
        directionSprite.flipX = !goesLeft;
        conveyorSprite.flipX = !goesLeft;
    }
}
