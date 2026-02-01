using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToFadeIn;

    [SerializeField] private float delayBeforeFade = 2f;
    [SerializeField] private float fadeInDuration = 1.5f;

    [SerializeField] private bool activarParpadeo = true;
    [Range(0f, 1f)][SerializeField] private float alphaMinimo = 0.3f;
    [SerializeField] private float velocidadParpadeo = 2f;

    private void Start()
    {
        if (textToFadeIn != null)
        {
            Color c = textToFadeIn.color;
            c.a = 0f;
            textToFadeIn.color = c;

            StartCoroutine(Sequence());
        }

        AudioManager.Instance.CantoGregoriano();
    }

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, timer / fadeInDuration));
            yield return null;
        }
        SetAlpha(1f);

        if (activarParpadeo)
        {
            float pito = 0f;
            while (true)
            {
                pito += Time.deltaTime * velocidadParpadeo;
                float nuevoAlpha = Mathf.Lerp(alphaMinimo, 1f, (Mathf.Sin(pito) + 1f) / 2f);
                SetAlpha(nuevoAlpha);
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = textToFadeIn.color;
        c.a = alpha;
        textToFadeIn.color = c;
    }

    private void Update()
    { 
        if (Time.timeSinceLevelLoad > 15f && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MainMenu");
            AudioManager.Instance.StopCanto();
        }
        else if(Time.timeSinceLevelLoad > 90f) // pasar al menu despues de 80 seconds
        {
            SceneManager.LoadScene("MainMenu");
            AudioManager.Instance.StopCanto();
        }
    }
}