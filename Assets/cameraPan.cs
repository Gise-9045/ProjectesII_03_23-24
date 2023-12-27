using UnityEngine;
using Cinemachine;
using System.Collections;

public class cameraPan : MonoBehaviour
{
    public CinemachineVirtualCamera cameraToPan;
    public Transform targetTransform; // Set this in the inspector to the object you want to follow
    public Transform playerTransform;
    public float smoothSpeed = 0.005f; // Adjust smooth speed for slower transition
    public Vector2 offset; // Add a 2D offset variable

    private bool isFollowingPlayer = true;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(0.5f);
            // Change camera settings to follow the new target smoothly
            SmoothCameraTransition(targetTransform);
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
        yield return new WaitForSeconds(0.33f);
        if (other.CompareTag("Player"))
        {
            // Revert to the default behavior when the player exits the trigger
            SmoothCameraTransition(playerTransform);
        }
    }

    // Other methods like Start and Update remain unchanged
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

        while (Vector2.Distance(new Vector2(cameraToPan.transform.position.x, cameraToPan.transform.position.y), new Vector2(desiredPosition.x, desiredPosition.y)) > 0.001f)
        {
            // Explicitly use Vector2.Lerp to ignore the z-axis
            Vector2 newPosition = Vector2.Lerp(cameraToPan.transform.position, desiredPosition, smoothSpeed);
            cameraToPan.transform.position = new Vector3(newPosition.x, newPosition.y, cameraToPan.transform.position.z);
        }

        cameraToPan.transform.position = desiredPosition;
        cameraToPan.m_Follow = target;
        // Esto es para ignorar la rotación en el eje z y evitar un incidente de Gise
        float angle = Mathf.Atan2(target.position.y - cameraToPan.transform.position.y, target.position.x - cameraToPan.transform.position.x) * Mathf.Rad2Deg;
        cameraToPan.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
