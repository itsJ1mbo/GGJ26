using System.Collections;
using UnityEngine;

public class SplineParticlesController : MonoBehaviour
{
    [SerializeField] private SplineFollowerParticle[] particles;
    [Header("Configuracion de particulas")]
    [SerializeField] private float burstTime;
    [SerializeField] private Color color;


    private bool showingEffect = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!showingEffect && other.CompareTag("Player"))
            StartCoroutine(StartParticles());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StopParticles();
        }
    }

    private void StopParticles()
    {
        foreach (var particle in particles)
        {
            particle.progress = 0;
            particle.gameObject.SetActive(false);
            particle.transform.position = new Vector3(0, 0, 0);
        }

        showingEffect = false;
    }

    private IEnumerator StartParticles()
    {
        showingEffect = true;
        float interval = burstTime / particles.Length;
        int currentIndex = 0;

        while (currentIndex < particles.Length)
        {
            // Activamos la partícula actual
            // Asumo que SplineFollowerParticle tiene un método o puedes activar su GameObject
            particles[currentIndex].gameObject.SetActive(true);

            // Si el componente tiene un método para empezar el movimiento, llámalo aquí:
            // particles[currentIndex].Play(); 

            currentIndex++;

            // Esperamos el tiempo calculado antes de poner la siguiente
            yield return new WaitForSeconds(interval);
        }

        yield return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var particle in particles)
        {
            particle.progress = 0;
            particle.gameObject.GetComponent<SpriteRenderer>().sharedMaterial.color = color;
            particle.gameObject.GetComponent<TrailRenderer>().sharedMaterial.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
