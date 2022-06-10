using UnityEngine;
using UnityEngine.AI;
 
public class WanderingAI : MonoBehaviour
{
    [SerializeField]
    private bool wander = true;
    public float wanderRadius;
    public float wanderTimer;
 
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    [SerializeField] private float moveSpeed = 3.5f;
    private Animator animator;
    
    // Use this for initialization
    void OnEnable () {
        agent = GetComponent<NavMeshAgent> ();
        animator = GetComponent<Animator> ();
        agent.speed = moveSpeed;
        timer = wanderTimer;
        DoWander();
    }
 
    // Update is called once per frame
    void Update () {
        if(!wander)
            return;
        
        HandleAnimation();
        timer += Time.deltaTime;
 
        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
 
    static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }

    public void DoWander(bool state = true)
    {
        wander = state;
        enabled = state;
    }

    void HandleAnimation()
    {
        animator.SetFloat("speed", Mathf.InverseLerp(0, moveSpeed, agent.speed / 2));
    }
}