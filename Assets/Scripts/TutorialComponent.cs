using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : MonoBehaviour
{
    public float _inSpeed;
    public float _outSpeed;
    
    public Image _wasd;
    public Image _arrows;

    private Color _wasdColor;
    private Color _arrowsColor;

    [HideInInspector] public bool _p1Move;
    [HideInInspector] public bool _p2Move;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _wasdColor = _wasd.color;
        _arrowsColor = _arrows.color;
    }

    // Update is called once per frame
    void Update()
    {
        _wasd.color = !_p1Move ? new Color(_wasdColor.r, _wasdColor.g, _wasdColor.b, Mathf.Lerp(_wasd.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_wasdColor.r, _wasdColor.g, _wasdColor.b, Mathf.Lerp(_wasd.color.a, 0f, Time.deltaTime * _outSpeed));
        _arrows.color = !_p2Move ? new Color(_arrowsColor.r, _arrowsColor.g, _arrowsColor.b, Mathf.Lerp(_arrows.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_arrowsColor.r, _arrowsColor.g, _arrowsColor.b, Mathf.Lerp(_arrows.color.a, 0f, Time.deltaTime * _outSpeed));
    }
}
