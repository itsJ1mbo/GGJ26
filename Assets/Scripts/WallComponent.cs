using UnityEngine;

public class WallComponent : MonoBehaviour
{
    // ID: 0=NEGRO 1=AZUL 2=ROJO 3=VERDE
    [SerializeField] int wallColorID;
    [SerializeField] private Collider2D muroCollider;

    // area de deteccion (trigger)
    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();
            if (playerAura != null)
            {
                Debug.Log("ASDF " + wallColorID + " " + playerAura.GetColorID());

                //comprobamos si los colores coinciden
                if (playerAura.GetColorID() == wallColorID)
                {
                    Physics2D.IgnoreCollision(collision, muroCollider, true);
                    Debug.Log("Pared " + wallColorID + " dejando pasar al jugador.");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, muroCollider, false);
        Debug.Log("Pared " + wallColorID + " bloqueando al jugador.");
    }

    public int getWallColorID()
    {
        return wallColorID;
    }

    void Start()
    {
        //muroCollider = GetComponent<Collider2D>();
    }
}
