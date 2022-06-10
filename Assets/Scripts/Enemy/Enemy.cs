using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ChasePlayer chasePlayer;

    [SerializeField] private FieldOfView fov;

    [SerializeField] private WanderingAI wanderingAI;

    [SerializeField] private EnemyShooting enemyShooting;
    
    public Health health;

    public bool isDead;
    
    public void ChasePlayer(Transform t)
    {
        chasePlayer.StartChasing(t);
        fov.StopEverything();
        wanderingAI.DoWander(false);
    }

    public void StopChasingStartWandering()
    {
        chasePlayer.StopChasing();
        enemyShooting.StartFiring(false);
        wanderingAI.DoWander();
    }

    public void Died()
    {
        GetComponent<Collider>().enabled = false;
        isDead = true;
        fov.StopEverything();
    }
}
