using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Patrol")]
    public float patrolRadius = 4f;
    public float patrolSpeed = 2f;
    public float patrolPointReachedDistance = 0.2f;
    public float waitTimeAtPoint = 1f;

    [Header("Chase")]
    public float chaseSpeed = 3.5f;
    private float detectionRange = 3f;
    public float stopDistance = 1f;

    [Header("Attack")]
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;

    [Header("Optional")]
    public SpriteRenderer spriteRenderer;

    private Vector3 startPosition;
    private Vector3 patrolTarget;
    private float waitTimer;
    private float attackTimer;

    private HealthManager playerHealth;

    private enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    private EnemyState currentState;

    void Start()
    {
        startPosition = transform.position;
        ChooseNewPatrolPoint();
        currentState = EnemyState.Patrol;

        if (player != null)
        {
            playerHealth = player.GetComponent<HealthManager>();
        }
    }

    void Update()
    {
        if (player == null)
            return;

        if (playerHealth != null && playerHealth.IsDead())
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // State decision
        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }

        // State execution
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                ChasePlayer();
                break;

            case EnemyState.Attack:
                AttackPlayer();
                break;
        }
    }

    void Patrol()
    {
        float distanceToPatrolPoint = Vector2.Distance(transform.position, patrolTarget);

        if (distanceToPatrolPoint > patrolPointReachedDistance)
        {
            Vector2 direction = (patrolTarget - transform.position).normalized;

            transform.position += (Vector3)(direction * patrolSpeed * Time.deltaTime);

            FaceDirection(direction);
        }
        else
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                ChooseNewPatrolPoint();
            }
        }
    }

    void ChooseNewPatrolPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * patrolRadius;

        patrolTarget = startPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);

        waitTimer = waitTimeAtPoint;
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            transform.position += (Vector3)(direction * chaseSpeed * Time.deltaTime);

            FaceDirection(direction);
        }
    }

    void AttackPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        FaceDirection(direction);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy attacked player");
            }

            attackTimer = attackCooldown;
        }
    }

    void FaceDirection(Vector2 direction)
    {
        if (spriteRenderer == null)
            return;

        if (direction.x > 0.05f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < -0.05f)
        {
            spriteRenderer.flipX = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Patrol Area
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        // Detection Area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Attack Area
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Stop Distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}