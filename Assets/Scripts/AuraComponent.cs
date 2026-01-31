using UnityEngine;

public class AuraComponent : MonoBehaviour
{
    [SerializeField] int colorID;

    int startingColorID;

    CircleCollider2D AuraCollider;
    [SerializeField] CapsuleCollider2D PlayerCollider;

    public void SetInitialColor(int id)
    {
        colorID = id;
        startingColorID = id;
    }

    // cuando otro jugador entra en el aura, cambia el color a morado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colorID = 3; // MORADO
        }
    }

    // cuando otro jugador sale del aura, vuelve al color inicial
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colorID = startingColorID; // MORADO
        }
    }

    public void setColliderEnabled(bool enabled)
    {
        PlayerCollider.enabled = enabled;
    }

    // recoge el color del aura del jugador
    public int GetColorID()
    {
        return colorID;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startingColorID = colorID;
        AuraCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
