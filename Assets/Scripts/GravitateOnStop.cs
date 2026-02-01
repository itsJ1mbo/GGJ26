using UnityEngine;
using UnityEngine.UIElements;

public class GravitateOnStop : MonoBehaviour
{
    private GameManager _gm;
    private Transform otherPlayer;
    public float minDistance = 2f;
    public float stillSpeedThreshold = 0.1f;
    public float attractionSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        _gm = GameManager.Instance;
    }

    void Update()
    {
        if (gameObject == _gm.GetPlayer1())
        {
            Debug.Log(_gm.GetPlayer2().transform);
            otherPlayer = _gm.GetPlayer2().transform;
        }
        else
        {
            Debug.Log(_gm.GetPlayer1().transform);
            otherPlayer = _gm.GetPlayer1().transform;
        }

        if (otherPlayer == null) return;
         
        Rigidbody2D otherRb = otherPlayer.GetComponent<Rigidbody2D>();

        // Comprobar si ambos est�n casi quietos
        bool bothStill =
            rb.linearVelocity.magnitude < stillSpeedThreshold &&
            otherRb.linearVelocity.magnitude < stillSpeedThreshold;

        float distance = Vector3.Distance(transform.position, otherPlayer.position);

        if (bothStill && distance <= minDistance)
        {
            // Punto medio entre los dos
            Vector3 midpoint = (transform.position + otherPlayer.position) / 2f;
            Debug.Log("ENTRAMOS AL mid  "+ midpoint);

            // Mover suavemente hacia el punto medio
            //transform.position = otherPlayer.position
            //
            transform.position = Vector3.Lerp(transform.position, otherPlayer.position, attractionSpeed * Time.deltaTime);
        }
    }
}
