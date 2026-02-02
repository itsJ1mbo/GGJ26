using UnityEngine;
using UnityEngine.SceneManagement;
using static MusicManager;

public class ScenesManager : MonoBehaviour
{
    public void nextScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void SetMusicStateByIndex(int index)
    {
        MusicManager.Instance.SetMusicState((MusicState)index);
    }
}
