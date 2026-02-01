using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Configuración Transición")]
    public GameObject meltCanvasPrefab;
    public float velocidadTransicion = 2.0f;

    /// <summary>
    /// Poner el nombre de las escenas en orden como queramos que se vayan mostrando.
    /// Cuando se pasa un nivel, se llega a un GoalComponent, que llama a este metodo, 
    /// mira la escena en la que estamos y busca el siguiente indice en la lista, si
    /// ya esta en el final, vuelve a la primera.
    /// </summary>
    public string[] sceneNamesInOrder;

    private MeltEffectController currentEffect;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Reinicia el nivel actual
    /// </summary>
    public void RestartLevel()
    {
        if (currentEffect != null) return;
        StartCoroutine(TransitionSequence(SceneManager.GetActiveScene().name));
    }

    /// <summary>
    /// Manda al siguiente nivel de la lista
    /// </summary>
    public void NextLevel()
    {
        Debug.Log("Cumeado");
        if (currentEffect != null) return;

        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentIndex = System.Array.IndexOf(sceneNamesInOrder, currentSceneName);

        string nextSceneName = (currentIndex != -1 && currentIndex + 1 < sceneNamesInOrder.Length)
            ? sceneNamesInOrder[currentIndex + 1]
            : sceneNamesInOrder[0];

        StartCoroutine(TransitionSequence(nextSceneName));
    }

    private IEnumerator TransitionSequence(string sceneToLoad)
    {
        AudioManager.Instance.PapelArrugao();

        if (meltCanvasPrefab != null)
        {
            GameObject canvasObj = Instantiate(meltCanvasPrefab);
            currentEffect = canvasObj.GetComponentInChildren<MeltEffectController>();

            if (currentEffect != null)
            {
                currentEffect.Setup();

                yield return StartCoroutine(currentEffect.FillRoutine(velocidadTransicion));
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.Instance.PapelArrugao();
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (this != null && currentEffect != null)
        {
            StartCoroutine(currentEffect.DropRoutine(velocidadTransicion));
            StartCoroutine(ClearEffectReference(velocidadTransicion));
        }
    }

    private IEnumerator ClearEffectReference(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentEffect = null;
    }
}