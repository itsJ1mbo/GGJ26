using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
   
    [SerializeField] int colorID; 
    [SerializeField] float moveSpeed = 3f;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private Vector3 targetPoint;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _enemyCollider;

    private bool isHidden = false; 

    void Start()
    {
        if (pointA != null) targetPoint = pointA.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = (targetPoint == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AuraComponent lightSource = collision.GetComponent<AuraComponent>();

        if (lightSource != null)
        {
            if (lightSource.GetCurrentColorID() == this.colorID)
            {
                SetEnemyHidden(true);
            }
         
            else
            {
                SetEnemyHidden(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AuraComponent>() != null)
        {
            SetEnemyHidden(false);
        }
    }

    void SetEnemyHidden(bool hidden)
    {
        isHidden = hidden;

        if (isHidden)
        {
            Color c = _spriteRenderer.color;
            c.a = 0.2f; 
            _spriteRenderer.color = c;
        }
        else
        {
            Color c = _spriteRenderer.color;
            c.a = 1f;
            _spriteRenderer.color = c;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        PlayerComponent player = collision.gameObject.GetComponent<PlayerComponent>();
        AuraComponent playerAura = collision.gameObject.GetComponent<AuraComponent>();

        if (player != null || playerAura != null)
        {
            int playerColorID = -1;

            if (playerAura != null)
            {
                playerColorID = playerAura.GetCurrentColorID();
            }

            Debug.Log(playerColorID);

            if (playerColorID == this.colorID)
            {
                AbsorbEnemy();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void AbsorbEnemy()
    {
        Destroy(this.gameObject);
    }
}