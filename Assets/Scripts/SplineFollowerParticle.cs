using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using System.Collections; // Necesario para la corrutina

public class SplineFollowerParticle : MonoBehaviour
{
    public SplineContainer splineContainer;

    [Header("Ajustes de Movimiento")]
    public float speed = 5f;
    [Range(0, 1)] public float progress = 0f;
    public bool loop = true;
    public bool faceForward = true;

    private float splineLength;
    private TrailRenderer trail;

    void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void Start()
    {
        if (splineContainer != null)
        {
            splineLength = splineContainer.CalculateLength();
        }
    }

    void Update()
    {
        if (splineContainer == null || splineLength <= 0) return;

        progress += (speed / splineLength) * Time.deltaTime;

        if (loop && progress >= 1f)
        {
            progress %= 1f;
            // Usamos una corrutina para limpiar el rastro de forma segura
            StartCoroutine(ClearTrailNextFrame());
        }
        else if (!loop)
        {
            progress = Mathf.Clamp01(progress);
        }

        // Obtener posición y dirección del Spline
        float3 pos, tangent, up;
        splineContainer.Evaluate(progress, out pos, out tangent, out up);

        transform.position = (Vector3)pos;

        if (faceForward)
        {
            float angle = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // El truco definitivo: apagar, limpiar y encender
    private IEnumerator ClearTrailNextFrame()
    {
        if (trail == null) yield break;

        trail.emitting = false; // Deja de generar rastro
        trail.Clear();          // Borra lo viejo

        yield return null;      // Espera un frame a que Unity procese la nueva posición

        trail.emitting = true;  // Vuelve a generar rastro ya en el sitio nuevo
    }

    public void ResetParticle()
    {
        progress = 0;
        if (trail != null)
        {
            trail.Clear();
            trail.emitting = true;
        }
    }
}