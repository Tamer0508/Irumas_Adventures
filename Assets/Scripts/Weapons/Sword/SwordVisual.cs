using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    public static SwordVisual Instance { get; private set; }

    private static readonly int AttackHash = Animator.StringToHash(Attack);
    [SerializeField] private Sword sword;

    private Animator _animator;
    private const string Attack = "Attack";

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sword.OnSwordSwing += Sword_OnSwordSwing;
    }

    private void Sword_OnSwordSwing(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(AttackHash);
    }

    private void OnDestroy()
    {
        sword.OnSwordSwing -= Sword_OnSwordSwing;
    }
}
