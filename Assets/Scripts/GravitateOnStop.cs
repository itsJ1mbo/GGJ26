using UnityEngine;
using UnityEngine.UIElements;

public class GravitateOnStop : MonoBehaviour
{
    private GameManager _gm;
    private Transform otherPlayer;
    private Rigidbody2D otherRb;
    public float minDistance = 2f;
    public float stillSpeedThreshold = 0.1f;
    public float attractionSpeed = 3f;

    private Rigidbody2D rb;

    private bool _gravitating;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        _gm = GameManager.Instance;
        
        otherPlayer = gameObject == _gm.GetPlayer1() ? _gm.GetPlayer2().transform : _gm.GetPlayer1().transform;
        
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (otherPlayer == null) return;

        // Comprobar si ambos est�n casi quietos
        bool bothStill =
            rb.linearVelocity.magnitude < stillSpeedThreshold &&
            otherRb.linearVelocity.magnitude < stillSpeedThreshold;

        float distance = Vector3.Distance(transform.position, otherPlayer.position);

        if (bothStill && distance <= minDistance)
        {
            _gravitating = true;
        }

        if (_gravitating)
        {
            if(distance > 0.1f)
            {
                // Punto medio entre los dos
                Vector2 direction = (otherRb.position - rb.position).normalized;

                // Movemos el cuerpo hacia el otro
                rb.linearVelocity = direction * attractionSpeed;
            }
            else
            {
                _gravitating = false;
            }
        }
    }
}
