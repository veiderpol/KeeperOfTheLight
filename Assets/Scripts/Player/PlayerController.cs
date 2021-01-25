using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public EventGameObject onClickAttackable;
    public AttackDefinition attackDefinition;
    public LayerMask enemyLayers;
    public LayerMask ui;
    public float attackRange = 0.5f;
    public Rigidbody2D rb;
    public float speed = 5f;
    float nextAttackTime = 0f;
    CharacterStats stats;
    Transform attackPoint;
    Vector2 playerMovemnt;
    Animator animator;
    RaycastHit2D hit;
        // Start is called before the first frame update
    void Start()
    {
        attackPoint = gameObject.transform;
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerMovemnt.x = Input.GetAxisRaw("Horizontal");
        playerMovemnt.y = Input.GetAxisRaw("Vertical");

        playerMovemnt = playerMovemnt.normalized;
        animator.SetFloat("Horizontal", playerMovemnt.x);
        animator.SetFloat("Vertical", playerMovemnt.y);
        animator.SetFloat("Speed", playerMovemnt.sqrMagnitude);

        if (Time.time >= nextAttackTime){
            
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    Debug.Log("Clicked on the UI");
                else
                    Attack();
                nextAttackTime = Time.time + 0.5f;
            }
        }
        
        
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerMovemnt * speed * Time.fixedDeltaTime);
    }
    public void Attack() 
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent(typeof(IAttackable)) != null)
            {
                GameObject attackable = enemy.gameObject;
                onClickAttackable.Invoke(attackable);
            }
        }
    }
    public void AttackTarget(GameObject target) 
    {
        var attack = attackDefinition.CreateAttack(stats, target.GetComponent<CharacterStats>());
        var attackables = target.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable attackable in attackables)
        {
            attackable.OnAttack(gameObject, attack);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position,attackRange);
    }
    [Serializable]
    public class EventGameObject : UnityEvent<GameObject> { }
}
