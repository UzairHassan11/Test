using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	public float force;
	public float lifetime;
	
	[HideInInspector]
	public PlayerController player;
	
	void OnEnable(){
		StartCoroutine("DestroySelf");
	}
	
	void Update(){
		transform.Translate(Vector3.forward * Time.deltaTime * force);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Player"))
		{
			PlayerManager.instance.playerHealth.Hit(5);
			BulletHit();
		}
		else if (other.gameObject.CompareTag("Enemy"))
		{
			if (other.GetComponent<Health>())
			{
				other.GetComponent<Health>().Hit(5);
				BulletHit();
			}
		}else
			BulletHit(false);
	}

	void BulletHit(bool withParticle = true)
	{
		gameObject.SetActive(false);
		StopCoroutine("DestroySelf");
		if (withParticle)
			ParticlesController.instance.SpawnParticle(ParticlesNames.Blood, transform);
	}

	IEnumerator DestroySelf(){
		yield return new WaitForSeconds(lifetime);
		gameObject.SetActive(false);
	}
}
