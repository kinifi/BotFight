using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private float explosionTimer = 2.0f;
	private int Health = 3;
	private Animator _anim;

	// Use this for initialization
	void Start () {

		_anim = GetComponent<Animator>();

		Invoke("Damage", explosionTimer);


	}
	
	public void Damage()
	{
		Health = 3;
		OnExplode();
	}

	private void OnExplode()
	{
		_anim.SetTrigger("Explosion");
		this.transform.localScale = new Vector2(3.0f, 3.0f);
		Destroy(this.gameObject, 1.0f);
	}

}
