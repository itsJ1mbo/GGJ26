using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ConveyorObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool canBeMoved = true;

    private Rigidbody2D rb;
    private ConveyorBelt currentBelt;

    public bool CanBeMoved => canBeMoved;
    public ConveyorBelt CurrentBelt => currentBelt;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    public void SetCurrentBelt(ConveyorBelt belt)
    {
        currentBelt = belt;
    }

    public void ClearBelt(ConveyorBelt belt)
    {
        // Solo limpiar si es la cinta actual
        if (currentBelt == belt)
        {
            currentBelt = null;
        }
    }

    public void ApplyConveyorMovement(Vector2 movement)
    {
        if (!canBeMoved) return;

        if (rb != null)
        {
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            transform.position += (Vector3)movement;
        }
    }

    public void SetCanBeMoved(bool value)
    {
        canBeMoved = value;
    }
}
