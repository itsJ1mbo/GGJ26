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

    public GameObject _cameraP1;
    private FollowCamera _followP1;
    public GameObject _cameraP2;
    private FollowCamera _followP2;
    public GameObject[] _players;

    public void SetUpPlayers(GameObject player1, GameObject player2)
    {
        Debug.Log("tengo sueño");

        _players[(int)Player.ONE] = player1;
        _players[(int)Player.TWO] = player2;
        _cameraP1.GetComponent<FollowCamera>().SetPlayers(_players);
        _cameraP2.GetComponent<FollowCamera>().SetPlayers(_players);
        _cameraP2.SetActive(false);
    }

    public void SplitScreen(bool b)
    {
        _cameraP2.SetActive(b);
        if (b)
        {
            _followP1.AdjustViewPort(0, 0, true);
            _followP2.AdjustViewPort(0.5f, 0, true);
        }
        else
        {
            _followP1.AdjustViewPort(0, 0, false);
        }
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
        _followP1 = _cameraP1.GetComponent<FollowCamera>();
        _followP2 = _cameraP2.GetComponent<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
