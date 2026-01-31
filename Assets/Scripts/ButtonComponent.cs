using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    [SerializeField] private ColorObject activable;
    [SerializeField] private AuraComponent.AuraColor _openColor;
    [SerializeField] private Sprite pushedSprite;
    [SerializeField] private Sprite unpushedSprite;

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Deactivate();
        }
    }

    void Activate()
    {
        _isOpen = true;

        activable.ChangeColor(_openColor);
        _spriteRenderer.sprite = pushedSprite;
    }

    void Deactivate()
    {
        _isOpen = false;

        activable.ChangeColor(_colorOriginal);
        _spriteRenderer.sprite = unpushedSprite;
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