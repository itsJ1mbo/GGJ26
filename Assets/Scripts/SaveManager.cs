using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    
    public int _levels;
    [HideInInspector] public int _currentLevel = 1;
    
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Level", _currentLevel);
        PlayerPrefs.Save();
        
        _currentLevel++;
        if (_currentLevel > _levels)
        {
            _currentLevel = 1;
        }
    }

    private void Awake()
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 1);
    }
}
