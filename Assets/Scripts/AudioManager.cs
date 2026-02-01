using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    // EJEMPLO USO DESDE DONDE QUIERA LLAMAR A LAS CLAPS: 
    // AudioManager.Instance.Claps(); desde donde quieras
    public static AudioManager Instance { get; private set; }

    public EventReference ostCantoGregorianoEvent;
    public EventReference sfxApagarLlamaEvent;
    public EventReference sfxClapsEvent;
    public EventReference sfxClinclinclinEvent;
    public EventReference sfxMecheroEvent;
    public EventReference sfxMonstruoEvent;
    public EventReference sfxMuerteVelaEvent;
    public EventReference sfxPalancaoBotonEvent;
    public EventReference sfxPapelArrugaoEvent;
    public EventReference sfxTizaEvent;

    private EventInstance musicInstance;

    void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEventReference(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }
    

    public void CantoGregoriano()
    {
        musicInstance = RuntimeManager.CreateInstance(ostCantoGregorianoEvent);
        musicInstance.start();
    }

    public void StopCanto()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }

    public void ApagarLlama()
    {
        PlayEventReference(sfxApagarLlamaEvent);
    }
    

    public void Claps()
    {
        PlayEventReference(sfxClapsEvent);
    }

    public void Clinclinclin()
    {
        PlayEventReference(sfxClinclinclinEvent);
    }

    public void Mechero()
    {
        PlayEventReference(sfxMecheroEvent);
    }

    public void Monstruo()
    {
        PlayEventReference(sfxMonstruoEvent);
    }

    public void MuerteVela()
    {
        PlayEventReference(sfxMuerteVelaEvent);
    }

    public void PalancaoBoton()
    {
        PlayEventReference(sfxPalancaoBotonEvent);
    }

    public void PapelArrugao()
    {
        PlayEventReference(sfxPapelArrugaoEvent);
    }

    public void Tiza()
    {
        PlayEventReference(sfxTizaEvent);
    }
    public void StopAllSounds()
    {
        FMOD.Studio.Bus masterBus = RuntimeManager.GetBus("bus:/");
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
