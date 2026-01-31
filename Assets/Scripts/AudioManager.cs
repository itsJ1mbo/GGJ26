using UnityEngine;
using FMOD;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public EventReference endLevelEventReference;
    public EventReference botonEventReference;
    public EventReference abrirCerrarPuertaEventReference;
    public EventReference botePinturaEventReference;
    public EventReference cambioParedColorEventReference;

    public void PlayEventReference(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }
    public void EndLevel()
    {
        PlayEventReference(endLevelEventReference);
    }

    public void Boton()
    {
        PlayEventReference(botonEventReference);
    }
    

    public void AbrirCerrarPuerta()
    {
        PlayEventReference(abrirCerrarPuertaEventReference);
    }

    public void BotePintura()
    {
        PlayEventReference(botePinturaEventReference);
    }

    public void CambioParedColor()
    {
        PlayEventReference(cambioParedColorEventReference);
    }
}
