using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float _zOffset = -10f;
    public float _minSize = 5f;     // Este será siempre el tamaño base original
    public float _maxSize = 8f;    
    public float _padding = 3f;

    [Header("Movimiento Continuo")]
    public float _zoomSpeed = 2f; 
    [Tooltip("Margen antes de encoger. Si los jugadores están muy cerca, se ignorará para volver al MinSize")]
    public float _shrinkThreshold = 1.0f; 

    [Header("Referencias")]
    private GameObject _p1;
    private GameObject _p2;
    private Transform _object;
    
    public Camera _camera1;
    public Camera _camera2;
    public Transform _texture; 

    private float _targetSize;
    private float _currentSize;

    public void SetPlayers(GameObject[] players)
    {
        _p1 = players[0];
        _p2 = players[1];
    }
    
    void Awake()
    {
        _object = transform;
        _currentSize = _minSize;
        _targetSize = _minSize;
    }

    void LateUpdate()
    {
        if (!_p1 || !_p2) return;

        // 1. Posicionamiento
        Vector3 pos1 = _p1.transform.position;
        Vector3 pos2 = _p2.transform.position;
        Vector3 midpoint = (pos1 + pos2) * 0.5f;
        _object.position = new Vector3(midpoint.x, midpoint.y, _zOffset);
        
        // 2. Cálculo del tamaño ideal
        float deltaX = Mathf.Abs(pos1.x - pos2.x) + _padding;
        float deltaY = Mathf.Abs(pos1.y - pos2.y) + _padding;
        float calculatedSize = Mathf.Max(deltaY * 0.5f, (deltaX / _camera1.aspect) * 0.5f);

        // 3. Lógica de Thresholds con Retorno al Origen
        if (calculatedSize > _targetSize)
        {
            // Expandir si superamos el tamaño actual
            _targetSize = Mathf.Min(calculatedSize, _maxSize);
        }
        else if (calculatedSize < _minSize + 0.1f) 
        {
            // REGLA ESPECIAL: Si los jugadores están lo suficientemente cerca del mínimo,
            // forzamos el target al _minSize original para que siempre vuelva a la base.
            _targetSize = _minSize;
        }
        else if (calculatedSize < _targetSize - _shrinkThreshold)
        {
            // Encogimiento normal por umbral
            _targetSize = Mathf.Max(calculatedSize, _minSize);
        }

        // 4. Movimiento Continuo (MoveTowards)
        // Esto asegura que la cámara siempre se mueva de forma fluida hacia el _targetSize
        _currentSize = Mathf.MoveTowards(_currentSize, _targetSize, _zoomSpeed * Time.deltaTime);

        // 5. Aplicar
        _camera1.orthographicSize = _currentSize;
        _camera2.orthographicSize = _currentSize;
        
        UpdateTextureScale(_currentSize);
    }

    void UpdateTextureScale(float currentSize)
    {
        if (_texture == null || _camera1 == null) return;
        float height = currentSize * 2f;
        float width = height * _camera1.aspect; 
        _texture.localScale = new Vector3(width, height, 1f);
    }
}