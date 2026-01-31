using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    public enum Player
    {
        ONE,
        TWO
    }
    
    public static GameManager Instance { get; private set; }

    public GameObject _camera;
    private GameObject[] _players = new GameObject[2];

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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // provisionallll
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            levelManager.RestartLevel();
        }

    }
}
