using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }


    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private Color transitionColor = new Color(0.2f, 0.6f, 1f);

    /// <summary>
    /// Poner el nombre de las escenas en orden como queramos que se vayan mostrando.
    /// Cuando se pasa un nivel, se llega a un GoalComponent, que llama a este metodo, 
    /// mira la escena en la que estamos y busca el siguiente indice en la lista, si
    /// ya est� en el final, vuelve a la primera.
    /// </summary>
    public string[] sceneNamesInOrder;



    /// <summary>
    /// Reinicia el nivel actual
    /// </summary>
    public void RestartLevel()
    {
        StartCoroutine(FadeAndRestart());
    }

    private IEnumerator FadeAndRestart()
    {
        if (fadeImage != null)
        {
            float elapsed = 0;
            float stepDuration = fadeDuration / 2; // Dividimos los 2 segundos

            // 1. Barrida hacia ABAJO (Cerrar)
            fadeImage.fillOrigin = (int)Image.OriginVertical.Top;
            while (elapsed < stepDuration)
            {
                elapsed += Time.deltaTime;
                fadeImage.fillAmount = Mathf.Lerp(0, 1, elapsed / stepDuration);
                yield return null;
            }
            fadeImage.fillAmount = 1;
        }

        // 2. Cargar escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Espera un frame para que la nueva escena se asiente
        yield return new WaitForEndOfFrame();

        if (fadeImage != null)
        {
            float elapsed = 0;
            float stepDuration = fadeDuration / 2;

            // 3. Barrida hacia ABAJO (Se va por el fondo)
            fadeImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            while (elapsed < stepDuration)
            {
                elapsed += Time.deltaTime;
                fadeImage.fillAmount = Mathf.Lerp(1, 0, elapsed / stepDuration);
                yield return null;
            }
            fadeImage.fillAmount = 0;
        }
    }


    /// <summary>
    /// Manda al siguiente nivel de la lista
    /// </summary>
    public void NextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        int currentIndex = System.Array.IndexOf(sceneNamesInOrder, currentSceneName);

        if (currentIndex != -1)
        {
            int nextIndex = currentIndex + 1;

            if (nextIndex < sceneNamesInOrder.Length)
            {
                SceneManager.LoadScene(sceneNamesInOrder[nextIndex]);
            }
            else
            {
                Debug.LogWarning("No hay siguiente escena. Cargando primera escena");
                SceneManager.LoadScene(sceneNamesInOrder[0]);
            }
        }
        else
        {
            Debug.LogError("La escena actual no est� en la lista de sceneNamesInOrder.");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (fadeImage != null)
        {
            fadeImage.color = transitionColor;
            fadeImage.fillAmount = 0;
        }
    }
}