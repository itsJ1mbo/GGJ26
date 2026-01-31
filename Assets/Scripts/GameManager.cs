using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Player
    {
        ONE,
        TWO
    }
    
    public static GameManager Instance { get; private set; }

    public GameObject _camera;
    public GameObject[] _players;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
