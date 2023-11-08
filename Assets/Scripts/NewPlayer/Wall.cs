using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _wallSlideSpeed = 5f;
    
    private Rigidbody2D _physics;
    private Collisions _collision;
    private Jump _jump;

    private void Awake() {
        _physics = GetComponent<Rigidbody2D>();
        _collision = GetComponent<Collisions>();
        _jump = GetComponent<Jump>();
    }

    public void WallJump(int jumpCount,  int maxJump)
    {
        StopCoroutine(DisableMovement(0 )); 
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = _collision.onRightWall ? Vector2.left : Vector2.right;

        _jump.Jump_player(jumpCount, maxJump);
    }
    public void WallSlide()
    {
        bool pushingWall = (_physics.velocity.x > 0 && _collision.onRightWall) || 
                           (_physics.velocity.x < 0 && _collision.onLeftWall );

        float push = pushingWall ? 0 : _physics.velocity.x;

        _physics.velocity = new Vector2(push, -_wallSlideSpeed);
    }

    private IEnumerator DisableMovement(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
