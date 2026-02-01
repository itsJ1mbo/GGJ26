using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void nextScene(string s)
    {
        if(SceneManager.GetActiveScene().name=="MainMenu")
            SceneManager.LoadScene("Level" + SaveManager.Instance._currentLevel);
        else
            SceneManager.LoadScene(s);
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Level1");
        SaveManager.Instance._currentLevel = 0;
        SaveManager.Instance.SaveGame();
    }
}
