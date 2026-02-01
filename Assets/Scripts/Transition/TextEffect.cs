using System.Collections;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float velocidadParpadeo = 2f;
    [Range(0f, 1f)][SerializeField] private float alphaMinimo = 0.1f;
    [Range(0f, 1f)][SerializeField] private float alphaMaximo = 1f;

    private void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null)
            StartCoroutine(DoBlink());
    }

    private IEnumerator DoBlink()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime * velocidadParpadeo;

            float onda = (Mathf.Sin(timer) + 1f) / 2f;

            float nuevoAlpha = Mathf.Lerp(alphaMinimo, alphaMaximo, onda);

            SetAlpha(nuevoAlpha);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = textComponent.color;
        c.a = alpha;
        textComponent.color = c;
    }
}