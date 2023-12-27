using UnityEngine;
using Cinemachine;
using System.Collections;

public class cameraPan : MonoBehaviour
{
    public CinemachineVirtualCamera cameraToPan;
    public Transform targetTransform; // Set this in the inspector to the object you want to follow
    public Transform playerTransform;

    private bool isFollowingPlayer = true;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(0.5f);
            // Change camera settings to follow the new target
            cameraToPan.m_Follow = targetTransform;
            cameraToPan.m_LookAt = targetTransform;
            isFollowingPlayer = false;
        }
    }

    void ResetCamera()
    {
      
        cameraToPan.m_Follow = playerTransform;
        cameraToPan.m_LookAt = playerTransform;
        isFollowingPlayer = true;
    }

    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        if (other.CompareTag("Player"))
        {
            // Revert to the default behavior when the player exits the trigger
            ResetCamera();
        }
    }

    // Other methods like Start and Update remain unchanged
    void Start()
    {
        ResetCamera(); // Set the default camera behavior
    }

    void Update()
    {
        //if(isFollowingPlayer)
        //{
        //    ResetCamera();
        //}
    }
}
