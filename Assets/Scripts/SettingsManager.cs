using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class SettingsManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    // Nombres por defecto, puedes cambiarlos en el Inspector de Unity
    public string masterBusPath = "bus:/";
    public string musicBusPath = "bus:/OST"; // Ojo: Verifica el nombre exacto en FMOD
    public string effectsBusPath = "bus:/SFX";

    // Variables privadas para controlar los buses
    private Bus masterBus;
    private Bus musicBus;
    private Bus effectsBus;

    void Start()
    {
        // 1. Inicializar los Buses buscando sus rutas
        masterBus = RuntimeManager.GetBus(masterBusPath);
        musicBus = RuntimeManager.GetBus(musicBusPath);
        effectsBus = RuntimeManager.GetBus(effectsBusPath);

        float volume;

        masterBus.getVolume(out volume);
        masterSlider.value = volume;

        musicBus.getVolume(out volume);
        musicSlider.value = volume;

        effectsBus.getVolume(out volume);
        effectsSlider.value = volume;

    masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }


    public void SetMasterVolume(float value)
    {
        masterBus.setVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        musicBus.setVolume(value);
    }

    public void SetEffectsVolume(float value)
    {
        effectsBus.setVolume(value);
    }
}