using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialComponent : MonoBehaviour
{
    public float _inSpeed;
    public float _outSpeed;
    
    public Image _wasd;
    public Image _arrows;
    public Image _jl;
    public Image _jr;
    public TMP_Text _textL;
    public TMP_Text _textR;

    private Color _wasdColor;
    private Color _arrowsColor;
    private Color _jlColor;
    private Color _jrColor;
    private Color _textLColor;
    private Color _textRColor;

    [HideInInspector] public bool _p1Move;
    [HideInInspector] public bool _p2Move;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _wasdColor = _wasd.color;
        _arrowsColor = _arrows.color;
        _jlColor = _jl.color;
        _jrColor = _jr.color;
        _textLColor = _textL.color;
        _textRColor = _textR.color;
    }

    // Update is called once per frame
    void Update()
    {
        _wasd.color = !_p1Move ? new Color(_wasdColor.r, _wasdColor.g, _wasdColor.b, Mathf.Lerp(_wasd.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_wasdColor.r, _wasdColor.g, _wasdColor.b, Mathf.Lerp(_wasd.color.a, 0f, Time.deltaTime * _outSpeed));
        _arrows.color = !_p2Move ? new Color(_arrowsColor.r, _arrowsColor.g, _arrowsColor.b, Mathf.Lerp(_arrows.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_arrowsColor.r, _arrowsColor.g, _arrowsColor.b, Mathf.Lerp(_arrows.color.a, 0f, Time.deltaTime * _outSpeed));

        if (GameManager.Instance._gamepad == 1)
        {
            _jl.color = !_p1Move ? new Color(_jlColor.r, _jlColor.g, _jlColor.b, Mathf.Lerp(_jl.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_jlColor.r, _jlColor.g, _jlColor.b, Mathf.Lerp(_jl.color.a, 0f, Time.deltaTime * _outSpeed));
            _jr.color = !_p2Move ? new Color(_jrColor.r, _jrColor.g, _jrColor.b, Mathf.Lerp(_jr.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_jrColor.r, _jrColor.g, _jrColor.b, Mathf.Lerp(_jr.color.a, 0f, Time.deltaTime * _outSpeed));
            _textL.color = !_p1Move ? new Color(_textLColor.r, _textLColor.g, _textLColor.b, Mathf.Lerp(_textL.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_textLColor.r, _textLColor.g, _textLColor.b, Mathf.Lerp(_textL.color.a, 0f, Time.deltaTime * _outSpeed));
            _textR.color = !_p2Move ? new Color(_textRColor.r, _textRColor.g, _textRColor.b, Mathf.Lerp(_textR.color.a, 1f, Time.deltaTime * _inSpeed)) : new Color(_textRColor.r, _textRColor.g, _textRColor.b, Mathf.Lerp(_textR.color.a, 0f, Time.deltaTime * _outSpeed));
        }
        else if (GameManager.Instance._gamepad == 2)
        {
            
        }
    }
}
