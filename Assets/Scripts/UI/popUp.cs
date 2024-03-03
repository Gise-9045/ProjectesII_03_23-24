using System.Collections;
using UnityEngine;

public class popUp : MonoBehaviour
{
    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float delay;

    // Start is called before the first frame update
    void Start()
    {
        _canvas.enabled = false;
        StartCoroutine(EnableCanvasAfterDelay(delay));
    }

    IEnumerator EnableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canvas.enabled = true;
    }
}
