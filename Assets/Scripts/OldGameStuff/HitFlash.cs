using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }
    public void Flash(float duration)
    {
        StartCoroutine(FlashTime(duration));
    }

    private IEnumerator FlashTime(float d)
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSecondsRealtime(d);
        spriteRenderer.material = originalMaterial;

    }
}
