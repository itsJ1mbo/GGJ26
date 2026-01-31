using UnityEngine;

public class GoalComponent : MonoBehaviour
{
    private int _playersInside = 0;
    private bool _levelDone = false;


    
    private void TransitionNextLevel()
    {
        // AQUI METER TRANSICION SI QUEREMOS Y TAL

        LevelManager.Instance.NextLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_levelDone && other.CompareTag("Player"))
        {
            _playersInside++;
            
            if (_playersInside == 2)
            {
                _levelDone = true;
                TransitionNextLevel();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playersInside--;
        }
    }
}
