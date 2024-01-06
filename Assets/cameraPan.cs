using UnityEngine;
using Cinemachine;
using System.Collections;

public class cameraPan : MonoBehaviour
{
    public CinemachineVirtualCamera cameraToPan;
    public Transform targetTransform;
    public Transform playerTransform;
    public float smoothSpeed = 0.005f;
    public Vector2 offset;

    private bool isFollowingPlayer = true;
    private bool ignoreTarget = false;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !ignoreTarget)
        {
            yield return new WaitForSeconds(0.25f);

            // Change camera settings to follow the new target smoothly
            SmoothCameraTransition(targetTransform);
            isFollowingPlayer = false;

            // Ignore the target for a specified time
            ignoreTarget = true;
            yield return new WaitForSeconds(2.0f); // Adjust the time as needed
            ignoreTarget = false;
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
        yield return new WaitForSeconds(0.25f);
        if (other.CompareTag("Player"))
        {
            // Revert to the default behavior when the player exits the trigger
            SmoothCameraTransition(playerTransform);
        }
    }

    void Start()
    {
        ResetCamera(); // Set the default camera behavior
    }

    void Update()
    {

    }

    void SmoothCameraTransition(Transform target)
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, cameraToPan.transform.position.z);

        // Smoothly interpolate the position without rotating
        cameraToPan.transform.position = Vector3.Lerp(cameraToPan.transform.position, desiredPosition, smoothSpeed);

        cameraToPan.m_Follow = target;
    }
}
