using System.Collections.Generic;
using UnityEngine;




public class AuraComponent : ColorObject
{
    // 1. Definimos los colores como BITS (Potencias de 2)
    // RED=1, GREEN=2, BLUE=4. Los demás son combinaciones automáticas.
    [System.Flags]
    public enum AuraColor
    {
        NONE = 0,
        RED = 1,
        GREEN = 2,
        YELLOW = 3,  // 1 | 2
        BLUE = 4,
        PURPLE = 5,  // 1 | 4
        CYAN = 6,    // 2 | 4
        WHITE = 7    // 1 | 2 | 4
    }

    [Header("Configuración")]
    [SerializeField] AuraColor baseColor; // El color nativo de ESTE objeto
    [SerializeField] bool isPlayer;

    // Variables internas
    private CircleCollider2D AuraCollider;

    // DICCIONARIO CONTADOR: 
    // Guarda cuántos objetos de cada color primario nos están tocando ahora mismo.
    // Clave: Color Primario (Rojo, Verde, Azul) -> Valor: Cantidad (0, 1, 2...)
    private Dictionary<AuraColor, int> colorCounters = new Dictionary<AuraColor, int>()
    {
        { AuraColor.RED, 0 },
        { AuraColor.GREEN, 0 },
        { AuraColor.BLUE, 0 }
    };

    public AuraColor CombineColor(AuraColor auraColor1, AuraColor auraColor2)
    {
        if (auraColor1 == auraColor2)
            return auraColor1; // mismo color, no psaa nada
        else if ((auraColor1 == AuraColor.RED && auraColor2 == AuraColor.BLUE) || (auraColor1 == AuraColor.BLUE && auraColor2 == AuraColor.RED))
            return AuraColor.PURPLE; // rojo + azul
        else if ((auraColor1 == AuraColor.RED && auraColor2 == AuraColor.GREEN) || (auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.RED))
            return AuraColor.YELLOW; // rojo + verde
        else if ((auraColor1 == AuraColor.RED && auraColor2 == AuraColor.PURPLE) || (auraColor1 == AuraColor.PURPLE && auraColor2 == AuraColor.RED))
            return AuraColor.PURPLE; // rojo + MORADO
        else if ((auraColor1 == AuraColor.RED && auraColor2 == AuraColor.YELLOW) || (auraColor1 == AuraColor.YELLOW && auraColor2 == AuraColor.RED))
            return AuraColor.YELLOW; // rojo + AMARILLO
        else if ((auraColor1 == AuraColor.RED && auraColor2 == AuraColor.CYAN) || (auraColor1 == AuraColor.CYAN && auraColor2 == AuraColor.RED))
            return AuraColor.WHITE; // rojo + CYAN

        else if ((auraColor1 == AuraColor.BLUE && auraColor2 == AuraColor.GREEN) || (auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.BLUE))
            return AuraColor.CYAN; // azul + verde
        else if ((auraColor1 == AuraColor.BLUE && auraColor2 == AuraColor.PURPLE) || (auraColor1 == AuraColor.PURPLE && auraColor2 == AuraColor.BLUE))
            return AuraColor.PURPLE; // azul + MORADO
        else if ((auraColor1 == AuraColor.BLUE && auraColor2 == AuraColor.YELLOW) || (auraColor1 == AuraColor.YELLOW && auraColor2 == AuraColor.BLUE))
            return AuraColor.WHITE; // azul + AMARILLO
        else if ((auraColor1 == AuraColor.BLUE && auraColor2 == AuraColor.GREEN) || (auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.BLUE))
            return AuraColor.CYAN; // azul + CYAN

        else if ((auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.PURPLE) || (auraColor1 == AuraColor.PURPLE && auraColor2 == AuraColor.GREEN))
            return AuraColor.WHITE; // verde + MORADO
        else if ((auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.YELLOW) || (auraColor1 == AuraColor.YELLOW && auraColor2 == AuraColor.GREEN))
            return AuraColor.YELLOW; // verde + AMARILLO
        else if ((auraColor1 == AuraColor.GREEN && auraColor2 == AuraColor.CYAN) || (auraColor1 == AuraColor.CYAN && auraColor2 == AuraColor.GREEN))
            return AuraColor.PURPLE; // verde + CYAN

        else
            return AuraColor.WHITE; //  resto son blancos
    }

    // cuando otro jugador entra en el aura, cambia el color a morado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddColorToCounters(collision.GetComponentInParent<AuraComponent>().GetBaseColor());
            RecalculateFinalColor();
        }
        else if (isPlayer && collision.CompareTag("StaticLight"))
        {
            AddColorToCounters(collision.GetComponent<AuraComponent>().GetBaseColor());
            RecalculateFinalColor();
        }
    }

    // cuando otro jugador sale del aura, vuelve al color inicial
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RemoveColorFromCounters(collision.GetComponentInParent<AuraComponent>().GetBaseColor());
            RecalculateFinalColor();
        }
        else if (isPlayer && collision.CompareTag("StaticLight"))
        {
            RemoveColorFromCounters(collision.GetComponent<AuraComponent>().GetBaseColor());
            RecalculateFinalColor();
        }
    }

    // Desglosa un color (incluso si es mezclado) y suma a los contadores
    void AddColorToCounters(AuraColor colorToAdd)
    {
        // El operador & verifica si el color contiene ese bit
        if ((colorToAdd & AuraColor.RED) != 0) colorCounters[AuraColor.RED]++;
        if ((colorToAdd & AuraColor.GREEN) != 0) colorCounters[AuraColor.GREEN]++;
        if ((colorToAdd & AuraColor.BLUE) != 0) colorCounters[AuraColor.BLUE]++;
    }

    // Desglosa un color y resta de los contadores
    void RemoveColorFromCounters(AuraColor colorToRemove)
    {
        if ((colorToRemove & AuraColor.RED) != 0) colorCounters[AuraColor.RED]--;
        if ((colorToRemove & AuraColor.GREEN) != 0) colorCounters[AuraColor.GREEN]--;
        if ((colorToRemove & AuraColor.BLUE) != 0) colorCounters[AuraColor.BLUE]--;
    }

    void RecalculateFinalColor()
    {
        int finalMask = 0;

        // AQUÍ ESTÁ LA MAGIA:
        // Si el contador de Rojo es mayor que 0 (da igual si es 1, 5 o 20),
        // activamos el bit de Rojo.
        if (colorCounters[AuraColor.RED] > 0) finalMask |= (int)AuraColor.RED;
        if (colorCounters[AuraColor.GREEN] > 0) finalMask |= (int)AuraColor.GREEN;
        if (colorCounters[AuraColor.BLUE] > 0) finalMask |= (int)AuraColor.BLUE;

        // Guardamos el resultado
        colorObject = (AuraColor)finalMask;
    }

    public void ForceChangeColor(AuraColor newColor)
    {
        RemoveColorFromCounters(baseColor);

        // 2. Actualizamos el color base al nuevo
        baseColor = newColor;

        // 3. Añadimos el nuevo color base a los contadores
        AddColorToCounters(baseColor);

        // 4. Refrescamos el color final resultante
        RecalculateFinalColor();
    }

    public AuraColor GetBaseColor() => baseColor;
    public AuraColor GetCurrentColor() => colorObject;
    public int GetCurrentColorID() => (int)colorObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AuraCollider = GetComponent<CircleCollider2D>();

        // Inicializamos los contadores con nuestro propio color base
        AddColorToCounters(baseColor);
        RecalculateFinalColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
