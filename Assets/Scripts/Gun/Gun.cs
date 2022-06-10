using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("Press and hold to keep shooting")]
    public bool isAuto;

    public float delayPerShot;

    public bool fire;

    public bool allowFire;
    
    [HideInInspector] public float currentDelay = 0;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private Bullet bullet;

    [SerializeField] private Transform bulletSpawnTransform;
    public virtual void Update()
    {
        if (fire)
        {
            if (currentDelay <= 0)
            {
                allowFire = true;
                currentDelay = delayPerShot;
            }
            else
            {
                currentDelay -= Time.deltaTime;
            }
        }
    }
    
    public virtual void FireOneShot()
    {
        if (allowFire)
        {
            allowFire = false;
            currentDelay = delayPerShot;
            muzzleFlash.SetActive(true);
            SpawnBullet();
        }
    }
    
    public virtual void KeepFiring(bool state)
    {
        fire = state;
        muzzleFlash.SetActive(state);
    }
    
    void SpawnBullet()
    {
        Bullet b = Instantiate(bullet, bulletSpawnTransform.position, bulletSpawnTransform.rotation,
            bullet.transform.parent);
        b.gameObject.SetActive(true);
    }
}
