using UnityEngine;

public class SprintEnemyRaycast : MonoBehaviour
{
    [SerializeField] AuraComponent.AuraColor enemyColor;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] LayerMask visionMask;

    [SerializeField] float sprintSpeed = 15f;
    [SerializeField] float chargeTime = 0.5f;
    [SerializeField] float cooldownTime = 1.5f;

    private enum State { Idle, Charging, Sprinting, Cooldown }
    [SerializeField] private State currentState = State.Idle;

    private Transform playerTransform;
    private Vector3 targetPosition;
    private float timer;

    private SpriteRenderer spriteRenderer;
    private bool isHidden = false;
    private GameObject player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
 

        UpdateEnemyColorVisuals();
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
       
        }

        if (playerTransform == null)
        {
            return;
        }

        if (isHidden)
        {
            return;
        }

        switch (currentState)
        {
            case State.Idle: CheckForPlayer(); break;
            case State.Charging: HandleCharging(); break;
            case State.Sprinting: HandleSprinting(); break;
            case State.Cooldown: HandleCooldown(); break;
        }
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);


        if (distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, visionMask);


            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(transform.position, direction * distanceToPlayer, Color.green); 

                    timer = chargeTime;
                    currentState = State.Charging;
                }
                else
                {
                    Debug.DrawRay(transform.position, direction * distanceToPlayer, Color.red); 
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * detectionRange, Color.white);
            }
        }
    }


    void HandleCharging()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            targetPosition = playerTransform.position;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //AuraComponent lightSource = collision.GetComponent<AuraComponent>();
        //if (lightSource != null)
        //{
        //    AuraComponent.AuraColor lightColor = lightSource.GetCurrentColor();
        //    if ((lightColor & enemyColor) == enemyColor) SetEnemyHidden(true);
        //    else SetEnemyHidden(false);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<AuraComponent>() != null) SetEnemyHidden(false);
    }

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
        PlayerComponent player = collision.gameObject.GetComponent<PlayerComponent>();
        AuraComponent playerAura = collision.gameObject.GetComponentInChildren<AuraComponent>();

        if (player != null || playerAura != null)
        {
            AuraComponent.AuraColor playerColor = AuraComponent.AuraColor.NONE;
            if (playerAura != null) playerColor = playerAura.GetCurrentColor();

            if ((playerColor & enemyColor) == enemyColor) AbsorbEnemy();
            else if (!isHidden) Destroy(collision.gameObject);
        }
    }

    void AbsorbEnemy() { Destroy(this.gameObject); }

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