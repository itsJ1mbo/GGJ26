using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void nextScene(string s)
    {
        SceneManager.LoadScene("Level" + GameManager.Instance._currentLevel);

    }
}
