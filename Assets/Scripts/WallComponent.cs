using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class WallComponent : ColorObject
{
    // ID: 0=NEGRO 1=AZUL 2=ROJO 3=VERDE
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
                if (playerAura.GetCurrentColor() == colorObject)
                {
                    Physics2D.IgnoreCollision(collision, muroCollider, true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, muroCollider, false);
    }


    void Start()
    {
        muroCollider = GetComponents<Collider2D>()[1];
    }
}
