using TMPro;
using UnityEngine;

public class ButtonActivateFountain : MonoBehaviour
{
    public GrayZoneData grayDatabase;
    [SerializeField] private SpriteRenderer sprite;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PuzzleBox"))
        {
            grayDatabase.dashActive = true;
        }
    }
   
}
