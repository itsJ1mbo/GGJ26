using UnityEngine;

public class WallComponent : MonoBehaviour
{
    [SerializeField] int wallColorID;
    [SerializeField] Collider2D muroCollider;

    // area de deteccion (trigger)
    private void OnTriggerStay2D(Collider2D collision)
    { 
        AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();

        if (playerAura != null)
        {
            //comprobamos si los colores coinciden
            if (playerAura.GetColorID() == wallColorID)
            {
                Physics2D.IgnoreCollision(collision, muroCollider, true);
                //Debug.Log("Pared " + wallColorID + " dejando pasar al jugador.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, muroCollider, false);
        //Debug.Log("Pared " + wallColorID + " bloqueando al jugador.");
    }

    public int getWallColorID()
    {
        return wallColorID;
    }

    void Start()
    {
        
    }
}
