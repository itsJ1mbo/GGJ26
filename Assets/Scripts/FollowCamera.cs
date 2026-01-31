using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float _zOffset = -10f;
    public float _threshHold = 11f; 
    public float _maxSize = 8f;    
    public float _padding = 3f;     
    
    [Header("Referencias")]
    private GameObject _p1;
    private GameObject _p2;
    private Transform _object;
    
    public Camera _camera1;
    public Camera _camera2;
    public Transform _texture; 

    public void SetPlayers(GameObject[] players)
    {
        _p1 = players[0];
        _p2 = players[1];
    }
    
    void Awake()
    {
        _object = transform;
    }

    void LateUpdate()
    {
        if (_p1 == null || _p2 == null) return;

        Vector3 pos1 = _p1.transform.position;
        Vector3 pos2 = _p2.transform.position;
        Vector3 midpoint = (pos1 + pos2) * 0.5f;
        
        _object.position = new Vector3(midpoint.x, midpoint.y, _zOffset);
        
        // 2. Cálculo de Zoom (Mantener proporciones)
        float deltaX = Mathf.Abs(pos1.x - pos2.x) + _padding;
        float deltaY = Mathf.Abs(pos1.y - pos2.y) + _padding;

        float sizeBasedOnY = deltaY * 0.5f;
        float sizeBasedOnX = (deltaX / _camera1.aspect) * 0.5f;

        float targetSize = Mathf.Max(sizeBasedOnY, sizeBasedOnX);
        float finalSize = Mathf.Clamp(targetSize, _threshHold * 0.5f, _maxSize);

        _camera1.orthographicSize = finalSize;
        _camera2.orthographicSize = finalSize;
        

        // 4. Sincronizar escala de la textura (SIN aplastar)
        //UpdateTextureScale(finalSize);
    }

    void UpdateTextureScale(float currentSize)
    {
        if (_texture == null || _camera1 == null) return;
        
        float height = currentSize * 2f;
        float width = height * _camera1.aspect; // Usamos el aspect ratio total del monitor

        _texture.localScale = new Vector3(width, height, 1f);
    }
}