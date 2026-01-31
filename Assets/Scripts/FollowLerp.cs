using UnityEngine;

public class FollowLerp : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] float smoothness = 0.125f;
    [SerializeField] Vector3 offset;

    private Vector3 currentVelocity = Vector3.zero;

    void FixedUpdate()
    {
        if (target == null) 
            return;

        // Calculamos la posición deseada
        Vector3 desiredPosition = target.position + offset;

        // SmoothDamp crea un efecto de amortiguación muy natural
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            smoothness
        );
    }
}
