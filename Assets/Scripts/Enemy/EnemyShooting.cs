using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float delayPerShot;

    public bool fire;

    [SerializeField] float currentDelay = 0;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private Bullet bullet;

    [SerializeField] private Transform bulletSpawnTransform;
    
    public virtual void Update()
    {
        if (fire)
        {
            if (currentDelay <= 0)
            {
                FireOneShot();
                currentDelay = delayPerShot;
            }
            else
            {
                currentDelay -= Time.deltaTime;
            }
        }
    }

    public void StartFiring(bool state)
    {
        fire = state;
        if(fire != state)
            currentDelay = 0;
    }
    
    void FireOneShot()
    {
        muzzleFlash.SetActive(true);
        SpawnBullet();
    }

    void SpawnBullet()
    {
        Bullet b = Instantiate(bullet, bulletSpawnTransform.position, bulletSpawnTransform.rotation,
            bullet.transform.parent);
        b.gameObject.SetActive(true);
    }
}
