using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton

    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    #endregion
    public PlayerManager player;
    public List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SomeoneDied()
    {
        if (PlayerManager.instance.playerHealth.died)
        {
            PlayerManager.instance.PlayerDied();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].StopChasingStartWandering();
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
