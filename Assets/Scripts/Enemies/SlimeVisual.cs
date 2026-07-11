using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]
public class SlimeVisual : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash(IsDie);
    private static readonly int Hit = Animator.StringToHash(TakeHit);
    private static readonly int ChasingSpeed = Animator.StringToHash(ChasingSpeedMultiplier);
    private static readonly int Running = Animator.StringToHash(IsRunning);
    private static readonly int Attacking = Animator.StringToHash(Attack);
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject enemyShadow;
    private Animator _animator;

    private const string IsRunning = "IsRunning";
    private const string TakeHit = "TakeHit";
    private const string IsDie = "IsDie";
    private const string ChasingSpeedMultiplier = "ChasingSpeedMultiplier";
    private const string Attack = "Attack";

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += _enemyAI_OnEnemyAttack;
        enemyEntity.onTakeHit += _enemyEntity_onTakeHit;
        enemyEntity.onDeath += _enemyEntity_onDeath;
    }

    private void _enemyEntity_onDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(Die, true);
        _spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }

    private void _enemyEntity_onTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Hit);
    }

    private void Update()
    {
        _animator.SetBool(Running, value: enemyAI.IsRunning);
        _animator.SetFloat(ChasingSpeed, enemyAI.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }

    private void _enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(Attacking);
    }
    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= _enemyAI_OnEnemyAttack;
        enemyEntity.onTakeHit -= _enemyEntity_onTakeHit;
        enemyEntity.onDeath -= _enemyEntity_onDeath;
    }
}
