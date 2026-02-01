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
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponentInParent<PlayerMovementComponent>() != null)
            {
                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().canInteract = true;
                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().palancaReference = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.gameObject.GetComponentInParent<Rigidbody2D>() != null)
            {

                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().canInteract = false;
                collision.gameObject.GetComponentInParent<PlayerMovementComponent>().palancaReference = null;
            }
        }
    }

    public void Activate()
    {
        _isOpen = !_isOpen;

        activable.ChangeColor(_openColor);
        AudioManager.Instance.PalancaoBoton();

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
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Start()
    {
        _colorOriginal = activable.colorObject;
    }
}