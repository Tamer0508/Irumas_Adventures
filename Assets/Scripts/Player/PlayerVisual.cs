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
        Vector2 moveInput = GameInput.Instance.GetMovementVector();
        float moveX = moveInput.x;
        float moveY = moveInput.y;

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = moveX;
        input.y = moveY;
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
