using UnityEngine;

public class AuraComponent : MonoBehaviour
{
    [SerializeField] int colorID;

    int startingColorID;

    CircleCollider2D AuraCollider;
    [SerializeField] CircleCollider2D PlayerCollider;

    public void SetInitialColor(int id)
    {
        colorID = id;
        startingColorID = id;

        //mientras q no tengamos sprites lo pongo aqui manual:
        if(id == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(id == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // cuando otro jugador entra en el aura, cambia el color a morado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerComponent other = collision.GetComponent<PlayerComponent>();
        if (other != null)
        {
            colorID = 3; // MORADO
        }
    }

    // cuando otro jugador sale del aura, vuelve al color inicial
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
