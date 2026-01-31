using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MeltEffectController : MonoBehaviour
{
    public RawImage targetImage;
    public Material meltMaterial;

    // VARIABLES INTERNAS PARA CONTROLAR EL ESTADO
    private Material activeMat;

    public void Setup()
    {
        // CAMBIO IMPORTANTE: Usamos 'transform.root.gameObject'
        // Esto busca al objeto padre de todos (el Canvas) y lo salva.
        DontDestroyOnLoad(transform.root.gameObject);

        targetImage.enabled = false;
    }

    // FASE 1: SUBIR Y TAPAR
    public IEnumerator FillRoutine(float duration)
    {
        yield return new WaitForEndOfFrame();

        // Captura de pantalla de la escena vieja
        Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTex.Apply();

        // Configuración inicial del material
        activeMat = new Material(meltMaterial);
        activeMat.SetTexture("_MainTex", screenTex);
        activeMat.SetFloat("_FillLevel", -0.3f);
        activeMat.SetFloat("_ShowImage", 1.0f); // Mostramos la foto vieja

        targetImage.material = activeMat;
        targetImage.enabled = true;

        // Animación de subida (Llenado)
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float level = Mathf.Lerp(-0.3f, 1.3f, timer / duration);
            activeMat.SetFloat("_FillLevel", level);
            yield return null;
        }

        // Aseguramos que esté totalmente blanco
        activeMat.SetFloat("_FillLevel", 1.3f);

        // TRUCO CLAVE: Ahora que está todo blanco, hacemos invisible la "foto" vieja.
        // El líquido blanco (que cubre todo) sigue visible, pero el fondo del canvas es transparente.
        // Así, cuando el líquido baje, se verá la NUEVA escena que haya detrás.
        activeMat.SetFloat("_ShowImage", 0.0f);
    }

    // FASE 2: BAJAR Y REVELAR
    public IEnumerator DropRoutine(float duration)
    {
        // Animación de bajada (Vaciado)
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Bajamos de 1.3 (lleno) a -0.3 (vacío)
            float level = Mathf.Lerp(1.3f, -0.3f, timer / duration);
            activeMat.SetFloat("_FillLevel", level);
            yield return null;
        }

        // Limpieza final
        Destroy(activeMat);
        Destroy(this.gameObject); // Nos autodestruimos al terminar
    }
}