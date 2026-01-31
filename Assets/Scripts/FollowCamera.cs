using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float _zOffset;
    public float _threshHold;
    
    private GameObject _p1;
    private GameObject _p2;
    
    private Transform _object;
    
    public Camera _camera1;
    public Camera _camera2;

    private float _lastDistance;

    public void SetPlayers(GameObject[] players)
    {
        _p1 = players[(int)GameManager.Player.ONE];
        _p2 = players[(int)GameManager.Player.TWO];
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _object = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 dis = _p1.transform.position - _p2.transform.position;
        Vector3 pos = (_p1.transform.position + _p2.transform.position) * 0.5f;

        _object.position = new Vector3(pos.x, pos.y, _zOffset);

        if(dis.magnitude > _threshHold && dis.magnitude < _threshHold + 10)
        {
            if (dis.magnitude > _lastDistance)
            {
                _camera1.orthographicSize += Time.deltaTime;
                _camera2.orthographicSize += Time.deltaTime;
            }
            else if (dis.magnitude < _lastDistance)
            {
                _camera1.orthographicSize -= Time.deltaTime;
                _camera2.orthographicSize -= Time.deltaTime;
            }
        }
        
        _lastDistance = dis.magnitude;
    }
}
