using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public TMP_Text _start;
    public GameObject _continue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SaveManager.Instance._currentLevel > 1) _continue.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
