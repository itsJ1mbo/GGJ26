using UnityEngine;
using static GameManager;

public class SprintEnemy : MonoBehaviour
{
    [SerializeField] AuraComponent.AuraColor enemyColor;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] LayerMask visionMask;

    [SerializeField] float sprintSpeed = 15f;
    [SerializeField] float chargeTime = 0.5f;
    [SerializeField] float cooldownTime = 1.5f;

    private enum State { Idle, Charging, Sprinting, Cooldown }
    [SerializeField] private State currentState = State.Idle;

    private Transform targetTransform; 
    private Vector3 targetPosition;
    private float timer;

    private SpriteRenderer spriteRenderer;
    private bool isHidden = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateEnemyColorVisuals();
    }

    void Update()
    {
        if (isHidden) return;

        if (currentState == State.Idle)
        {
            FindClosestPlayer();
        }

        if (targetTransform == null && currentState != State.Sprinting) return;

        switch (currentState)
        {
            case State.Idle:
                HandleFlip(targetTransform.position);
                CheckForPlayer();
                break;
            case State.Charging:
                HandleFlip(targetTransform.position);
                HandleCharging();
                break;
            case State.Sprinting:
                HandleFlip(targetPosition);
                HandleSprinting();
                break;
            case State.Cooldown:
                HandleCooldown();
                break;
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject p in players)
        {
            float dist = Vector2.Distance(transform.position, p.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestPlayer = p.transform;
            }
        }
        targetTransform = closestPlayer;
    }

    void HandleFlip(Vector3 focusPoint)
    {
        if (spriteRenderer == null) return;
        spriteRenderer.flipX = focusPoint.x < transform.position.x;
    }

    void CheckForPlayer()
    {
        if (targetTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, targetTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, visionMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                timer = chargeTime;
                currentState = State.Charging;
            }
        }
    }

    void HandleCharging()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetPosition = targetTransform.position;
            currentState = State.Sprinting;
        }
    }

    void HandleSprinting()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, sprintSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            timer = cooldownTime;
            currentState = State.Cooldown;
        }
    }

    void HandleCooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) currentState = State.Idle;
    }

    private void OnTriggerExit2D(Collider2D collision) { if (collision.GetComponent<AuraComponent>() != null) SetEnemyHidden(false); }

    void SetEnemyHidden(bool hidden)
    {
        if (isHidden == hidden) return;
        isHidden = hidden;
        Color c = spriteRenderer.color;
        c.a = isHidden ? 0.05f : 1f;
        spriteRenderer.color = c;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            PlayerComponent playerComp = collision.gameObject.GetComponent<PlayerComponent>();
            AuraComponent playerAura = collision.gameObject.GetComponentInChildren<AuraComponent>();

            if (playerComp != null || playerAura != null)
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
        if (spriteRenderer == null) return;
        switch (enemyColor)
        {
            case AuraComponent.AuraColor.RED: spriteRenderer.color = Color.red; break;
            case AuraComponent.AuraColor.GREEN: spriteRenderer.color = Color.green; break;
            case AuraComponent.AuraColor.BLUE: spriteRenderer.color = Color.blue; break;
            case AuraComponent.AuraColor.YELLOW: spriteRenderer.color = Color.yellow; break;
            case AuraComponent.AuraColor.PURPLE: spriteRenderer.color = new Color(0.5f, 0, 0.5f); break;
            case AuraComponent.AuraColor.CYAN: spriteRenderer.color = Color.cyan; break;
        }
    }
}