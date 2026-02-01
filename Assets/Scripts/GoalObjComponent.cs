using UnityEngine;

public class GoalObjComponent : MonoBehaviour
{

    [SerializeField]
    int necessaryPlayers;
    [SerializeField]
    int playerCount;

    [SerializeField]
    public bool complete;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCount++;
            AudioManager.Instance.Clinclinclin();
            if (playerCount >= necessaryPlayers)
                complete = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playerCount--;
            if (playerCount < necessaryPlayers)
                complete = false;
        }


    }

}
