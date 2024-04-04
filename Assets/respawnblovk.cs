using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnblovk : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform respawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox")){
            collision.transform.position = respawnPoint.position;
        }
    }
    void Update()
    {
        
    }
}
