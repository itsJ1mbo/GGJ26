using UnityEngine;

public class PalancaTransportadora : MonoBehaviour
{
    [SerializeField] private ConveyorBelt[] cintas;

    private SpriteRenderer _spriteRenderer;
    private bool _isActivated = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Activate();
        }
    }

    void Activate()
    {
        _isActivated = !_isActivated;

        // Invertir dirección de todas las cintas
        foreach (var cinta in cintas)
        {
            if (cinta != null)
            {
                cinta.ReverseDirection();
            }
        }

        // Flip visual de la palanca
        if (_spriteRenderer != null)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
