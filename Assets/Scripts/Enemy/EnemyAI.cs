using System;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyAI : MonoBehaviour
{
    public static Action<GameObject, Attack> onTryAttack;
    public static Action<GameObject, Attack> onTryAttackTower;
    public Enemy_SO enemyDef;
    GameObject target;
    public float speed = 0f;
    public float nextWaypointDistance = 2f;
    public Transform[] towersPosition;
    public EventGameObject OnClickAttackable;
    public GameObject player;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;
    Vector2 enemyMovement;
    public int tower;
    TowerStats towerStats;
    bool changeTower = true;
    float nextAttackTime = 0f;
    private void OnEnable()
    { 
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (!enemyDef.attackEnemy) 
        {
            this.speed = UnityEngine.Random.Range(125, 180);
            target = towersPosition[0].gameObject;
            towerStats = target.GetComponentInChildren<TowerStats>();
        }
        else { 
            this.speed = UnityEngine.Random.Range(150, 200);
            target = GameObject.FindGameObjectWithTag("Player");
        }
        target = towersPosition[2].gameObject;
        InvokeRepeating("UpdatePath", 0f, 1f);
    }
    void UpdateTower() 
    {
        target = towersPosition[1].gameObject;
    }
    void UpdatePath() {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
    }
    void OnPathComplete(Path p) 
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;
        
        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;

        float distanceEnemyPlayer = Vector2.Distance(this.gameObject.transform.position, target.transform.position);

        if (Time.time >= nextAttackTime)
        {
            if (!enemyDef.attackEnemy)
            {
                if (towerStats.towerDef.health <= 0 && changeTower && (tower < towersPosition.Length))
                {
                    target = towersPosition[tower++].gameObject;
                    towerStats = target.GetComponentInChildren<TowerStats>();
                    changeTower = false;
                }
                
                if (distanceEnemyPlayer < 5f)
                {
                    Attack();
                    nextAttackTime = Time.time + 0.7f;
                }
            }
            else
            {
                
                if (distanceEnemyPlayer < 1.5f)
                {
                    Attack();
                    nextAttackTime = Time.time + 0.1f;
                }
            }
        }
        changeTower = true;
        //Animation Section
        enemyMovement.x = rb.velocity.x;
        enemyMovement.y = rb.velocity.y;
        enemyMovement = enemyMovement.normalized;
        anim.SetFloat("Horizontal", enemyMovement.x);
        anim.SetFloat("Vertical", enemyMovement.y);
    }
    public void Attack() {
        var attackables = target.gameObject.GetComponentsInChildren(typeof(IAttackable));

        if (attackables != null && enemyDef.attackEnemy)
        {
            var attack = enemyDef.CreateAttack();
            OnClickAttackable.Invoke(target.gameObject);
        }
        if (attackables != null && !enemyDef.attackEnemy)
        {
            OnClickAttackable.Invoke(target.gameObject);
            
        }
    }
    public void AttackTarget(GameObject target)
    {
        var attack = enemyDef.CreateAttack();
        var attackables = target.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable attackable in attackables)
        {
            attackable.OnAttack(gameObject, attack);
        }
    }
    [System.Serializable]
    public class EventGameObject : UnityEvent<GameObject> { }

}
