using UnityEngine;

public class InteractorComponent : MonoBehaviour
{
    [SerializeField] AuraComponent.AuraColor interactorColor;

    AuraComponent.AuraColor oldColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si te acercas recoges tu color y dejas el tuyo
        if (collision.CompareTag("Player"))
        {
            AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();
            if (playerAura != null)
            {
                if (playerAura.GetBaseColor() != interactorColor)
                {
                    Debug.Log("Color diferente, player: " + playerAura.GetBaseColor() + ", objeto: " + interactorColor);

                    interactorColor = playerAura.GetBaseColor();

                    // cambiar color del player al del objeto
                    playerAura.ForceChangeColor(oldColor);

                    oldColor = interactorColor;
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldColor = interactorColor;
        Debug.Log("Coso ID al principio: " + interactorColor);
    }

    // Update is called once per frame
    void Update()
    {
        // el switch mas guarro de mi vida
        switch(interactorColor)
        {
            case AuraComponent.AuraColor.NONE:
                GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case AuraComponent.AuraColor.BLUE:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case AuraComponent.AuraColor.RED:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case AuraComponent.AuraColor.PURPLE:
                GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case AuraComponent.AuraColor.GREEN:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case AuraComponent.AuraColor.CYAN:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case AuraComponent.AuraColor.YELLOW:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case AuraComponent.AuraColor.WHITE:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }

    }

}