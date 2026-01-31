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

    private bool _split = false;
    public bool _isPlayer1; 

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
        
        // 1. Posicionamiento
        if(!_split)
            _object.position = new Vector3(midpoint.x, midpoint.y, _zOffset);
        else
            _object.position = _isPlayer1 ? new Vector3(pos1.x, pos1.y, _zOffset) : new Vector3(pos2.x, pos2.y, _zOffset);
        
        // 2. Cálculo de Zoom (Mantener proporciones)
        float deltaX = Mathf.Abs(pos1.x - pos2.x) + _padding;
        float deltaY = Mathf.Abs(pos1.y - pos2.y) + _padding;

        float sizeBasedOnY = deltaY * 0.5f;
        float sizeBasedOnX = (deltaX / _camera1.aspect) * 0.5f;

        float targetSize = Mathf.Max(sizeBasedOnY, sizeBasedOnX);
        float finalSize = Mathf.Clamp(targetSize, _threshHold * 0.5f, _maxSize);

        _camera1.orthographicSize = finalSize;
        _camera2.orthographicSize = finalSize;

        // 3. Lógica de Cambio de Estado
        if (!_split && targetSize >= _maxSize)
        {
            _split = true;
            GameManager.Instance.SplitScreen(true);
        }
        else if (_split && targetSize < _maxSize - 1.5f) // Histéresis para evitar parpadeo
        {
            _split = false;
            GameManager.Instance.SplitScreen(false);
        }

        // 4. Sincronizar escala de la textura (SIN aplastar)
        UpdateTextureScale(finalSize);
    }

    public void AdjustViewPort(float x, float y, bool isSplit)
    {
        float width = isSplit ? 0.5f : 1f;
        _camera1.rect = new Rect(x, y, width, 1f);
        _camera2.rect = new Rect(x, y, width, 1f);
        _split = isSplit;
    }

    void UpdateTextureScale(float currentSize)
    {
        if (_texture == null || _camera1 == null) return;

        // LA CLAVE: La malla debe representar el área TOTAL que la cámara vería 
        // a pantalla completa, incluso si el Viewport Rect está a 0.5f.
        // Si usamos _camera1.rect.width aquí, la textura se aplasta. 
        // Al NO usarlo, la textura mantiene su aspecto 16:9 y la cámara simplemente "recorta" los lados.
        
        float height = currentSize * 2f;
        float width = height * _camera1.aspect; // Usamos el aspect ratio total del monitor

        _texture.localScale = new Vector3(width, height, 1f);
    }
}