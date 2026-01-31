using UnityEngine;

public class AuraComponent : MonoBehaviour
{
    [SerializeField] int colorID;

    int startingColorID;

    CircleCollider2D AuraCollider;
    [SerializeField] CircleCollider2D PlayerCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerComponent other = collision.GetComponent<PlayerComponent>();
        if (other != null)
        {
            colorID = 3;
            Debug.Log("Colision con aura detectada, color cambiado a 3");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerComponent other = collision.GetComponent<PlayerComponent>();
        if (other != null)
        {
            colorID = startingColorID;
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
    void Start()
    {
        startingColorID = colorID;
        AuraCollider = GetComponent<CircleCollider2D>();

        Debug.Log("Player color: " + colorID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
