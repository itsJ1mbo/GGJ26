using UnityEngine;

public class InteractorComponent : ColorObject
{
    private AuraComponent.AuraColor oldColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si te acercas recoges tu color y dejas el tuyo
        if (collision.CompareTag("Player"))
        {
            AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();
            if (playerAura != null)
            {
                if (playerAura.GetBaseColor() != colorObject)
                {
                    Debug.Log("Color diferente, player: " + playerAura.GetBaseColor() + ", objeto: " + colorObject);

                    colorObject = playerAura.GetBaseColor();

                    // cambiar color del player al del objeto
                    playerAura.ForceChangeColor(oldColor);
                    playerAura.ChangeColor(oldColor);


                    oldColor = colorObject;

                    ChangeColor(colorObject);
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldColor = colorObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

}