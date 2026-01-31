using UnityEngine;

public class PalancaComponent : MonoBehaviour
{
    [SerializeField] private ColorObject activable;
    [SerializeField] private AuraComponent.AuraColor _openColor;

    private SpriteRenderer _spriteRenderer;

    private AuraComponent.AuraColor _colorOriginal;
    private bool _isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Activate();
        }
    }

    void Activate()
    {
        _isOpen = !_isOpen;

        activable.ChangeColor(_openColor);

        if (_isOpen)
        {
            activable.ChangeColor(_openColor);
            _spriteRenderer.flipX = false;
        }
        else
        {
            activable.ChangeColor(_colorOriginal);
            _spriteRenderer.flipX = true;
        }
    }


    private void Awake()
    {
        _colorOriginal = _openColor;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _colorOriginal = activable.colorObject;
    }
}