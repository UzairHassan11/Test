using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Gun> guns;

    private int currentGunIndex;
    
    [HideInInspector] public Gun currentGun;

    private void Start()
    {
        AssignCurrentGun();
    }

    public void NextGun()
    {
        if (PlayerManager.instance.isDead)
            return;
        
        currentGun.gameObject.SetActive(false);
        currentGunIndex++;
        if (currentGunIndex == guns.Count)
            currentGunIndex = 0;
        AssignCurrentGun();
    }

    void AssignCurrentGun()
    {
        currentGun = guns[currentGunIndex];
        currentGun.gameObject.SetActive(true);
    }
}
