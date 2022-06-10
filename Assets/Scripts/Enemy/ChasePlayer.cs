using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private float distanceFromTarget;
    private NavMeshAgent agent;
    [SerializeField] private float chaseSpeed, shootingRange;
    private bool inShootingRange, chasePlayer;
    private Transform target;
    private Animator animator;
    [SerializeField] private EnemyShooting enemyShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent> ();
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(!chasePlayer)
            return;
        
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        if (distanceFromTarget <= shootingRange)
        {
            agent.speed = 0;
            inShootingRange = true;
            transform.LookAt(target);
            enemyShooting.StartFiring(true);
        }
        else
        {
            enemyShooting.StartFiring(false);
            agent.destination = target.position;
            agent.speed = chaseSpeed;
            inShootingRange = false;
        }

        HandleAnimation();
    }
    
    void HandleAnimation()
    {
        animator.SetFloat("speed", Mathf.InverseLerp(0, chaseSpeed, agent.speed));
    }
    
    
    public void StartChasing(Transform player)
    {
        chasePlayer = true;
        agent.stoppingDistance = 5;
        agent.speed = chaseSpeed;
        target = player;
    }

    public void StopChasing()
    {
        chasePlayer = false;
    }
}
