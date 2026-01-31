using UnityEngine;

public class AuraComponent : MonoBehaviour
{
    [SerializeField] int colorID;
    [SerializeField] CapsuleCollider2D PlayerCollider;

    private int startingColorID;
    private CircleCollider2D AuraCollider;


    // cuando otro jugador entra en el aura, cambia el color a morado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            colorID = collision.GetComponentInParent<AuraComponent>().GetOriginalColorID() + colorID; // SUMA IDS 1rojo +2azul = 3morado
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

    public int GetOriginalColorID()
    {
        return startingColorID;
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
