using UnityEngine;

public class InteractorComponent : ColorObject
{
    private AuraComponent.AuraColor oldColor;

    private GameObject activePlayer = null;
    private bool colorExchanged = false; // El "seguro" para no repetir

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Validar que es un jugador
        if (!collision.CompareTag("Player")) return;

        // 2. Validar que el objeto esté libre y no hayamos intercambiado ya
        if (activePlayer == null && !colorExchanged)
        {
            activePlayer = collision.gameObject;
            TryExchangeColor(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Al salir el jugador que tenía el control, reseteamos todo
        if (collision.gameObject == activePlayer)
        {
            activePlayer = null;
            colorExchanged = false; // Permitimos un nuevo intercambio para el siguiente
            Debug.Log("Reset: Listo para el próximo jugador");
        }
    }

    private void TryExchangeColor(Collider2D collision)
    {
        AuraComponent playerAura = collision.GetComponentInParent<AuraComponent>();

        if (playerAura != null && !colorExchanged)
        {
            AuraComponent.AuraColor playerColor = playerAura.GetBaseColor();

            // Solo si los colores son distintos, hacemos el cambio
            if (playerColor != colorObject)
            {
                // Marcamos como intercambiado ANTES de cambiar los datos para evitar re-entrada
                colorExchanged = true;

                AuraComponent.AuraColor tempColor = colorObject;

                // Aplicar cambios
                colorObject = playerColor;
                ChangeColor(colorObject);

                playerAura.ForceChangeColor(tempColor);
                playerAura.ChangeColor(tempColor);

                Debug.Log($"Intercambio exitoso con {collision.name}. Objeto ahora es {colorObject}");
            }
        }
    }

    void Start()
    {
        oldColor = colorObject;
    }
}