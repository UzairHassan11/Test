using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth, currentHealth;

    public bool died;

    public Healthbar healthBar;

    [SerializeField] private Animator animator;
    
    private void Start()
    {
        currentHealth = totalHealth;
        healthBar.SetMaxHealth(totalHealth);
    }

    [ContextMenu("Hit")]
    public void Hit(int damage = 5)
    {
        if(died)
            return;
        currentHealth -= damage;
        
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            GameManager.instance.SomeoneDied();
        }
    }

    void Die()
    {
        died = true;
        animator.SetTrigger("Die");
        animator.SetLayerWeight(1, 0);
        healthBar.gameObject.SetActive(false);
        
        if(GetComponent<Enemy>()!=null)
            GetComponent<Enemy>().Died();
    }
}
