using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public float fadeDuration = 2f;
    public Color fadeColor;
    private Material material;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        material = rend.material;
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, t / fadeDuration);

            material.color = newColor;

            t += Time.deltaTime / fadeDuration;
            yield return null;
        }

        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        material.color = finalColor;
    }
}
