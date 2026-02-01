using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] AuraComponent.AuraColor enemyColor;
    [SerializeField] float moveSpeed = 3f;

    [SerializeField] Transform[] patrolPoints;

    private int currentPointIndex = 0;
    private int direction = 1; 
    private Vector3 targetPoint;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _enemyCollider;
    private bool isHidden = false;

    private Animator _animator;

    void Awake() 
    {
        // Unity busca el componente Animator que está pegado en este mismo objeto
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyCollider = GetComponent<Collider2D>();

        UpdateEnemyColorVisuals();

        if (patrolPoints.Length > 0 && patrolPoints[0] != null)
        {
         
            transform.position = patrolPoints[0].position;
            targetPoint = patrolPoints[0].position;
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (patrolPoints.Length == 0) return;

      
        if (targetPoint.x > transform.position.x)
        {
            _spriteRenderer.flipX = false; 
        }
        else if (targetPoint.x < transform.position.x)
        {
            _spriteRenderer.flipX = true; 
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            if (patrolPoints.Length > 1)
            {
                if (currentPointIndex >= patrolPoints.Length - 1)
                {
                    direction = -1;
                }
                else if (currentPointIndex <= 0)
                {
                    direction = 1;
                }

                currentPointIndex += direction;
            }

            if (patrolPoints[currentPointIndex] != null)
            {
                targetPoint = patrolPoints[currentPointIndex].position;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        AuraComponent lightSource = collision.GetComponent<AuraComponent>();

        if (lightSource != null)
        {
            AuraComponent.AuraColor lightColor = lightSource.GetCurrentColor();

            if ((lightColor & enemyColor) == enemyColor)
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
        Color c = _spriteRenderer.color;
        c.a = isHidden ? 0.2f : 1f;
        _spriteRenderer.color = c;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerComponent player = collision.gameObject.GetComponent<PlayerComponent>();
        AuraComponent playerAura = collision.gameObject.GetComponentInChildren<AuraComponent>();

        if (player != null || playerAura != null)
        {
            AuraComponent.AuraColor playerColor = AuraComponent.AuraColor.NONE;

            if (playerAura != null)
            {
                playerColor = playerAura.GetCurrentColor();
            }
       
            if (LevelManager.Instance != null)
            {
                Debug.Log("bye bye");
                AudioManager.Instance.ApagarLlama();
                LevelManager.Instance.RestartLevel();
            }
            Destroy(collision.gameObject);

        }
    }

    void AbsorbEnemy()
    {
        //AudioManager.Instance.Monstruo();
        Destroy(this.gameObject);
    }

    void UpdateEnemyColorVisuals()
    {
        if (_spriteRenderer == null) return;

        switch (enemyColor)
        {
            case AuraComponent.AuraColor.RED: _spriteRenderer.color = Color.red; break;
            case AuraComponent.AuraColor.GREEN: _spriteRenderer.color = Color.green; break;
            case AuraComponent.AuraColor.BLUE: _spriteRenderer.color = Color.blue; break;
            case AuraComponent.AuraColor.YELLOW: _spriteRenderer.color = Color.yellow; break;
            case AuraComponent.AuraColor.PURPLE: _spriteRenderer.color = new Color(0.5f, 0, 0.5f); break;
            case AuraComponent.AuraColor.CYAN: _spriteRenderer.color = Color.cyan; break;
        }
    }
}