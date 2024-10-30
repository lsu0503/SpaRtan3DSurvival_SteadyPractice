using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum AISTATE
{
    IDLE,
    WANDERING,
    ATTACKING
}

public class NPC : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AISTATE aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float lastAttackTime;
    public float attackDistance;

    private float PlayerDistance;

    public float fieldOfView = 120f;

    public Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(AISTATE.WANDERING);
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != AISTATE.IDLE);

        switch (aiState)
        {
            case AISTATE.IDLE:
            case AISTATE.WANDERING:
                PassiveUpdate();
                break;

            case AISTATE.ATTACKING:
                AttackingUpdate();
                break;
        }
    }

    public void SetState(AISTATE state)
    {
        aiState = state;

        switch (aiState)
        {
            case AISTATE.IDLE:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AISTATE.WANDERING:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AISTATE.ATTACKING:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / walkSpeed;
    }

    private void PassiveUpdate()
    {
        if (aiState == AISTATE.WANDERING && agent.remainingDistance < 0.1f)
        {
            SetState(AISTATE.IDLE);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (PlayerDistance < detectDistance)
        {
            SetState(AISTATE.ATTACKING);
        }
    }

    private void WanderToNewLocation()
    {
        if (aiState != AISTATE.IDLE) return;

        SetState(AISTATE.WANDERING);
        agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        int i = 0;

        do
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
                                   out hit, maxWanderDistance, NavMesh.AllAreas);

            i++;
            if (i == 30)
                break;
        } while (Vector3.Distance(transform.position, hit.position) < detectDistance);

        return hit.position;
    }

    private void AttackingUpdate()
    {
        if (PlayerDistance < attackDistance && isPlayerIsFieldView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                CharacterManager.Instance.Player.controller.GetComponent<IDamageable>().TakePhysicalDamage(damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }

        else
        {
            if (PlayerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                }

                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AISTATE.WANDERING);
                }
            }

            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AISTATE.WANDERING);
            }
        }
    }

    private bool isPlayerIsFieldView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < (fieldOfView * 0.5f);
    }

    public void TakePhysicalDamage(int damage)
    {
        Debug.Log(damage);
        health -= damage;

        if (health <= 0)
            Die();

        StartCoroutine(DamageFlash());
    }

    private void Die()
    {
        foreach (ItemData dropItem in dropOnDeath)
            Instantiate(dropItem.dropPrefab, transform.position + (Vector3.up * 2), Quaternion.identity);

        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white;
        }
    }
}