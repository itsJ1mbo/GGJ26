using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }


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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
}