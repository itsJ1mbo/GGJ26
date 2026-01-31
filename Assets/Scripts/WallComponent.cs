using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class WallComponent : MonoBehaviour
{
    // ID: 0=NEGRO 1=AZUL 2=ROJO 3=VERDE
    [SerializeField] AuraComponent.AuraColor wallColorID;
    private Collider2D muroCollider;

    // area de deteccion (trigger)
    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();
            if (playerAura != null)
            {

                //comprobamos si los colores coinciden
                if (playerAura.GetCurrentColor() == wallColorID)
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


    void Start()
    {
        muroCollider = GetComponents<Collider2D>()[1];
    }
}
