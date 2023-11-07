using TMPro;
using UnityEngine;

public class alternatingJump : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private PlayerJump controllerJump;
    public TextMeshProUGUI touchCounterText;

    public void Start()
    {
        controllerJump.onJump += Toggle;//add toggle to the onjump action
        touchCounterText.text = isActive ? ":)" : ":(";
    }

    public void OnDestroy()
    {
        controllerJump.onJump -= Toggle;
    }

    public void Toggle()
    {
        isActive = !isActive;
        GetComponent<Collider2D>().enabled = isActive;
        touchCounterText.text = isActive ? ":)" : " :(";
    }
}
