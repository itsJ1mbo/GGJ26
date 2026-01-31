using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class SplineFollowerParticle : MonoBehaviour
{
    public SplineContainer splineContainer;

    [Header("Ajustes de Movimiento")]
    public float speed = 5f;
    [Range(0, 1)] public float progress = 0f;
    public bool loop = true;
    public bool faceForward = true;

    private float splineLength;

    void Start()
    {
        if (splineContainer != null)
        {
            // Calculamos la longitud para que la velocidad sea constante en metros/segundo
            splineLength = splineContainer.CalculateLength();
        }
    }

    void Update()
    {
        if (splineContainer == null) return;

        // 1. Actualizar el progreso basado en el tiempo y la velocidad
        // Progreso = (Velocidad / Longitud) * tiempo
        progress += (speed / splineLength) * Time.deltaTime;

        if (loop)
        {
            progress %= 1f; // Vuelve al inicio al llegar a 1
        }
        else
        {
            progress = Mathf.Clamp01(progress);
        }

        // 2. Obtener posición y dirección del Spline
        float3 pos, tangent, up;
        splineContainer.Evaluate(progress, out pos, out tangent, out up);

        // 3. Aplicar al Transform
        transform.position = (Vector3)pos;

        if (faceForward)
        {
            // Orientar el sprite hacia la dirección del movimiento
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}