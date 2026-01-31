using UnityEngine;

public class WallComponent : MonoBehaviour
{
    [SerializeField] int colorID;

    private void OnTriggerStay2D(Collider2D collision)
    {
        AuraComponent other = collision.GetComponent<AuraComponent>();
        if (other != null)
        {
            if (other.GetColorID() == colorID)
            {
                Debug.Log("Colision con color igual detectada");

                // desactivar el collider del player para permitir el paso
                other.setColliderEnabled(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AuraComponent other = collision.GetComponent<AuraComponent>();
        if (other != null)
        {
            // reactivar el collider del player al salir del trigger
            other.setColliderEnabled(true);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
