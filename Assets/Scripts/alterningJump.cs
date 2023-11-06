using TMPro;
using UnityEngine;

public class alternatingJump : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private PlayerJump controllerJump;
    public TextMeshProUGUI touchCounterText;

    void FixedUpdate()
    {
        if (controllerJump != null)
        {
            bool isPlayerJumping = controllerJump.isJumping;
            if (isPlayerJumping == true)
            {
                isActive = !isActive;
                Debug.Log("CAMBIO: " + isActive);
            }
            


                if (isActive == true)
            {
                GetComponent<Collider2D>().enabled = true;
                touchCounterText.text = ":)";
            }
            else
            {
                GetComponent<Collider2D>().enabled = false;
                touchCounterText.text = ":(";
            }
        }
    }
}
