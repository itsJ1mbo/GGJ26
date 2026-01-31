using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class ConveyorBelt : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("Conveyor Settings")]
    [SerializeField] private Direction direction = Direction.Right;
    [SerializeField] private float speed = 3f;

    private List<ConveyorObject> objectsOnBelt = new List<ConveyorObject>();
    private Collider2D col;

    public Vector2 DirectionVector
    {
        get
        {
            switch (direction)
            {
                case Direction.Up: return Vector2.up;
                case Direction.Down: return Vector2.down;
                case Direction.Left: return Vector2.left;
                case Direction.Right: return Vector2.right;
                default: return Vector2.right;
            }
        }
    }

    void Awake()
    {
        col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void FixedUpdate()
    {
        objectsOnBelt.RemoveAll(obj => obj == null);

        Vector2 movement = DirectionVector * speed * Time.fixedDeltaTime;

        foreach (var obj in objectsOnBelt)
        {
            // Solo mover si esta cinta es la actual del objeto
            if (obj != null && obj.CanBeMoved && obj.CurrentBelt == this)
            {
                obj.ApplyConveyorMovement(movement);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ConveyorObject conveyorObj = other.GetComponent<ConveyorObject>();
        if (conveyorObj != null)
        {
            if (!objectsOnBelt.Contains(conveyorObj))
            {
                objectsOnBelt.Add(conveyorObj);
            }
            // Siempre tomar control cuando entra
            conveyorObj.SetCurrentBelt(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ConveyorObject conveyorObj = other.GetComponent<ConveyorObject>();
        if (conveyorObj != null)
        {
            objectsOnBelt.Remove(conveyorObj);
            conveyorObj.ClearBelt(this);
        }
    }

    public void ReverseDirection()
    {
        switch (direction)
        {
            case Direction.Up: direction = Direction.Down; break;
            case Direction.Down: direction = Direction.Up; break;
            case Direction.Left: direction = Direction.Right; break;
            case Direction.Right: direction = Direction.Left; break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 pos = transform.position;
        Vector3 dir = (Vector3)DirectionVector * 0.5f;

        Gizmos.DrawLine(pos - dir * 0.5f, pos + dir * 0.5f);

        Vector3 right = Quaternion.Euler(0, 0, 30) * -dir * 0.3f;
        Vector3 left = Quaternion.Euler(0, 0, -30) * -dir * 0.3f;
        Gizmos.DrawLine(pos + dir * 0.5f, pos + dir * 0.5f + right);
        Gizmos.DrawLine(pos + dir * 0.5f, pos + dir * 0.5f + left);
    }
}
