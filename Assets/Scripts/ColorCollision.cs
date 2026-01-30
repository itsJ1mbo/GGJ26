using UnityEngine;

public class ColorCollision : MonoBehaviour
{
    [SerializeField] int colorID;

    CircleCollider2D AuraCollider;
    [SerializeField] CircleCollider2D PlayerCollider;
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
        AuraCollider = GetComponent<CircleCollider2D>();

        Debug.Log("Player color: " + colorID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
