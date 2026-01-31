using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Color _colorOpen = Color.white;

    private SpriteRenderer _doorSprite;
    private Color _colorOriginal; 

    void Start()
    {
   
        if (_door != null)
        {
            _doorSprite = _door.GetComponent<SpriteRenderer>();

            if (_doorSprite != null)
            {
                _colorOriginal = _doorSprite.color;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenDoor();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (_doorSprite != null) _doorSprite.color = _colorOpen;

    }

    void CloseDoor()
    {
        if (_doorSprite != null) _doorSprite.color = _colorOriginal;

    }
}