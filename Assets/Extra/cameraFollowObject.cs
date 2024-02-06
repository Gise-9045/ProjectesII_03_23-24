using System.Collections;
using UnityEngine;

public class cameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float flipRotationTime = 0.5f;
    private Coroutine turnCoroutine;
    private PlayerMovementController player;
    private bool facingright;

    // Start is called before the first frame update
    void Awake()
    {
        player = playerTransform.GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime / flipRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        facingright = !player.facingRight;

        if (facingright)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
