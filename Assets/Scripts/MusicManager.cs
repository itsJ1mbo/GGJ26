using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new MusicManager();
            return _instance;
        }
    }


    public EventReference musicEventReference;
    private EventInstance musicInstance;

    public enum MusicState
    {
        MENU = 0,
        GAME = 1
    }

    public enum PlayerState
    {
        QUIETO = 0,
        CAMINANDO = 1
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _instance = this;
    }
    void Start()
    {
        InitializeMusic();
    }

    private void InitializeMusic()
    {
        Debug.Log("InicializamosMusica");
        musicInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicInstance.start();
    }

    public void SetParameter(string paramName, float newValue)
    {
        if (musicInstance.isValid())
        {
            musicInstance.setParameterByName(paramName, newValue);
        }
    }

    public void SetMusicState(MusicState newState)
    {
        SetParameter("GameState", (float)newState);
    }

    public void SetPlayer1State(PlayerState newState)
    {
        SetParameter("P1Movement", (float)newState);
    }

    public void SetPlayer2State(PlayerState newState)
    {
        SetParameter("P2Movement", (float)newState);
    }

    public void SetMusicStateByIndex(int index)
    {
        SetMusicState((MusicState)index);
    }

    public void SetPlayer1StateByIndex(int index)
    {
        SetPlayer1State((PlayerState)index);
    }

    public void SetPlayer2StateByIndex(int index)
    {
        SetPlayer2State((PlayerState)index);
    }
    void Update()
    {
        if (Keyboard.current == null) return;

        // Tecla M -> Cambia a Menu
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SetMusicState(MusicState.MENU);
            Debug.Log("Música cambiada a Menu");
        }

        // Tecla C -> Cambia a Caminando
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SetMusicState(MusicState.GAME);
            Debug.Log("Música cambiada a Game");
        }
        // Tecla Q -> Cambia a Quieto
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SetPlayer1State(PlayerState.QUIETO);
            Debug.Log("Música cambiada a Quieto");
        }

        // Tecla C -> Cambia a Caminando
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SetPlayer1State(PlayerState.CAMINANDO);
            Debug.Log("Música cambiada a Caminando");
        }
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}