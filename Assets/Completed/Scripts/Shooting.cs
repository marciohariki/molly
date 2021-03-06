﻿using UnityEngine;
using System.Collections;

namespace Completed
{

	public class Shooting : MonoBehaviour 
	{

		public float firecadency = 0.5f;		//sets the time between shots
		public bool onreloadFlag = false;		//flag to hold on for when firing
		public Rigidbody2D Bullet;				// Prefab of the bullet
		public float bulletSpeed = 30f;

		private Player playerCtrl;				//Reference to the Player script.


		void Awake()
		{
			// Setting up the references.
			playerCtrl = transform.root.GetComponent<Player>();
		}

		// Use this for initialization
		void Start () {
			
			
		}
	
		// Update is called once per frame
		void Update () {
			if (playerCtrl.shootingflag == true && onreloadFlag == false) {
				Vector3 bulletPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
				Rigidbody2D bulletInstance = Instantiate(Bullet, bulletPos, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(bulletSpeed, 0);
				onreloadFlag = true;
				StartCoroutine (ShootAgainTimer ());
			}
		}

		IEnumerator ShootAgainTimer () 
		{
			yield return new WaitForSeconds(firecadency);
			onreloadFlag = false;
		}
	}
}