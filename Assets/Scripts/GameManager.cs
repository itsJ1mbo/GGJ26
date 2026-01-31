using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum Player
    {
        ONE,
        TWO
    }
    
    public static GameManager Instance { get; private set; }

    public GameObject _camera;
    private GameObject[] _players = new GameObject[2];
    [HideInInspector] public bool _gamepad;
    
    public TutorialComponent _tutorial;

    public void SetFirstMove(bool p1)
    {
        if (!_tutorial) return; 
        
        if(p1)
            _tutorial._p1Move = true;
        else
            _tutorial._p2Move = true;
    }
    
    public void SetUpPlayers(GameObject player1, GameObject player2)
    {
        _players[(int)Player.ONE] = player1;
        _players[(int)Player.TWO] = player2;
        _camera.GetComponent<FollowCamera>().SetPlayers(_players);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnRestart(InputAction.CallbackContext obj)
    {
        LevelManager.Instance.RestartLevel();

    }
}
