using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public AuraComponent.AuraColor colorObject;
    public SpriteRenderer coloredSprite;
    public void ChangeColor(AuraComponent.AuraColor newColor)
    {
        colorObject = newColor;
        float alpha = coloredSprite.color.a;

        switch (colorObject)
        {
            case AuraComponent.AuraColor.RED:
                coloredSprite.color = new Color(1f, 0f, 0f, alpha);
                break;

            case AuraComponent.AuraColor.GREEN:
                coloredSprite.color = new Color(0f, 1f, 0f, alpha);
                break;

            case AuraComponent.AuraColor.BLUE:
                coloredSprite.color = new Color(0f, 0f, 1f, alpha);
                break;

            case AuraComponent.AuraColor.PURPLE:
                // Rojo + Azul
                coloredSprite.color = new Color(1f, 0f, 1f, alpha);
                break;

            case AuraComponent.AuraColor.YELLOW:
                // Rojo + Verde
                coloredSprite.color = new Color(1f, 1f, 0f, alpha);
                break;

            case AuraComponent.AuraColor.CYAN:
                // Verde + Azul
                coloredSprite.color = new Color(0f, 1f, 1f, alpha);
                break;

            case AuraComponent.AuraColor.WHITE:
                coloredSprite.color = new Color(1f, 1f, 1f, alpha);
                break;

            default:
                coloredSprite.color = new Color(1f, 1f, 1f, alpha); // Por defecto blanco
                break;
        }
    }
}
