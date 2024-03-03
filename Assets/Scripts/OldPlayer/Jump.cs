using System;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D _physics;

    [SerializeField] private float jumpForce;

    [Space]
    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask waterLayer;
    public bool inWater;
    [SerializeField] private float gravityOnFall;
    [SerializeField] private float gravityOnJump;
    [SerializeField, Range(0f, 1f)] private float volumeAudio = 0.2f;
    float oldVelocity;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip swimClip;
    [SerializeField] private AudioSource jumpAudio;

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        jumpAudio = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        jumpAudio.clip = jumpClip;
        
    }

    private void Update()
    {
        jumpAudio.volume = volumeAudio;
        if (_physics.velocity.y >= 0f)
        {
            _physics.gravityScale = gravityOnJump;
        }
        else
        {
            _physics.gravityScale = gravityOnFall;
        }
        oldVelocity = _physics.velocity.y;
    }

    public void Jump_player(int jumpCount, int maxJump)
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            jumpAudio.clip = swimClip;
            ControlOnWater();
        }
        else
        {
            jumpAudio.clip = jumpClip;
            if (jumpCount >= maxJump) return;

            // Play the jumping audio
            PlayJumpAudio();

            Debug.Log(jumpCount + " | " + maxJump);
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void ControlOnWater()
    {
        jumpAudio.Play();
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void PlayJumpAudio()
    {
        if (jumpAudio != null)
        {
            jumpAudio.Play();
        }
    }
}
