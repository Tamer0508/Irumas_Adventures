using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash(IsDie);
    private Vector2 input;

    Animator anim;
    private Vector2 lastMoveDirection;
    private FlashBlink _flashBlink;

    private const string IsDie = "IsDie";

 

    private void Start()
    {
        anim = GetComponent<Animator>();
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        _flashBlink = GetComponent<FlashBlink>();
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        anim.SetBool(Die, true);
        _flashBlink.StopBlinking();
    }

    private void Update()
    {
        if (Player.Instance.IsAlive())
        {
            ProccessInputs();
            Animate();
        }
    }

    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void OnDestroy()
    {
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
