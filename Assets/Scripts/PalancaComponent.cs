using UnityEngine;

public class PalancaComponent : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _player;
    [SerializeField] private Color _openColor = Color.white;

    private SpriteRenderer _doorSpriteComp;
    private Color _colorOriginal;
    private bool _isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        _isOpen = !_isOpen; 

        if (_isOpen)
        {
             _doorSpriteComp.color = _openColor;
        }
        else
        {
             _doorSpriteComp.color = _colorOriginal;

        }

    }

    void Start()
    {
        _doorSpriteComp = _door.GetComponent<SpriteRenderer>();
        _colorOriginal = _doorSpriteComp.color;
        

    }


}