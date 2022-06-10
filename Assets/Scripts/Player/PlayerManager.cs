using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton

    public static PlayerManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    public Health playerHealth;
    public PlayerInventory playerInventory;

    public bool isDead;

    public void PlayerDied()
    {
        isDead = true;
    }
}
