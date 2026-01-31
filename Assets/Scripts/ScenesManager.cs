using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void nextScene(string s)
    {
        SceneManager.LoadScene("Level" + SaveManager.Instance._currentLevel);
    }
}
