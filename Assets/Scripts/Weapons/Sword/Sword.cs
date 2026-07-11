using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount = 2;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private SpriteRenderer swordSpriteRenderer;
    [SerializeField] private float spriteDisableTime = 0.3f;
    [SerializeField] private float cooldownPolygon = 0.7f;
    private float nextAttackTime = 0f;
    private float activeTime = 0.1f;
    private bool canPolygon = true;



    public event EventHandler OnSwordSwing;

    private Coroutine hideCoroutine;

    private PolygonCollider2D _polygonCollider2D;

    private void Start()
    {
        swordSpriteRenderer.enabled = false;
    }

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        swordSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Attack()
    {
        ActivateCollider();
        ShowAndResetHideTimer();

        if (Time.time > nextAttackTime)
        {
            OnSwordSwing?.Invoke(this, EventArgs.Empty);

            nextAttackTime = Time.time + attackRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity) ){
            enemyEntity.TakeDamage(damageAmount);
        } 
    }


    private void ShowAndResetHideTimer()
    {
        // Всегда включаем спрайт при вызове
        swordSpriteRenderer.enabled = true;

        // Если уже есть корутина скрытия — останавливаем
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        // Запускаем новую задержку перед скрытием
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(spriteDisableTime);
        swordSpriteRenderer.enabled = false;
        hideCoroutine = null; // очищаем ссылку
    }

    public void ActivateCollider()
    {
        if (!canPolygon) return; // Если кулдаун — выходим
        StartCoroutine(ActivateWithCooldown());
    }

    private IEnumerator ActivateWithCooldown()
    {
        canPolygon = false;               // Запрещаем повторное включение
        _polygonCollider2D.enabled = true;    // Включаем коллайдер

        yield return new WaitForSeconds(activeTime); // Ждём время активности

        _polygonCollider2D.enabled = false;   // Выключаем коллайдер

        yield return new WaitForSeconds(cooldownPolygon); // Ждём кулдаун

        canPolygon = true;                // Снова можно включать
    }
}
