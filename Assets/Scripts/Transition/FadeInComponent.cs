using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInComponent : MonoBehaviour
{
    public Image img;

    [SerializeField] float duracion = 2.0f;

    void Start()
    {
        Color c = img.color;
        c.a = 0;
        img.color = c;

        StartCoroutine(Aparecer());
    }

    IEnumerator Aparecer()
    {
        for (float t = 0; t < duracion; t += Time.deltaTime)
        {
            Color c = img.color;
            c.a = Mathf.Lerp(0, 1, t / duracion);
            img.color = c;
            yield return null;
        }

        Color final = img.color;
        final.a = 1;
        img.color = final;
    }
}