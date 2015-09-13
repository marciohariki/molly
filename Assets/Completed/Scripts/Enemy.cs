﻿using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed {

	public class Enemy : MovingObject {

		public bool waiting_update = false;
		public bool on_IA_wait = true;								//flag for checking if is waiting to act
		public float bulletEnemySpeed = -30f;
		public static Enemy instance = null;
		public float EnemyDef = 0f; 								//Enemy Life
		public Rigidbody2D Bullet_Enemy;						//Prefab of bullet

		private float MeanDelayActions = 0.5f;						//mean time between 2 actions
		private float CurrentDelay;									//delay for the current moment
		private int yDir;
		private int action_pick;

		//Awake is always called before any Start functions

		void Awake()
		{
			//Check if instance already exists
			if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);	
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad (gameObject);
			
		}

		// Use this for initialization
		void Start () {
			CurrentDelay = MeanDelayActions;
			base.Start ();
		}
	
		// Update is called once per frame
		void Update () {
			if (GameManager.instance.doingSetup == true)
				return;

			if (waiting_update == false && on_IA_wait == true) {
				StartCoroutine (EnemyTurnReset (CurrentDelay));
				waiting_update = true;
				return;
			}

			if (on_IA_wait == false) {

				action_pick = Random.Range(1, 5);
				IA_Choose (action_pick);
				on_IA_wait = true;
			}
			
		}

		IEnumerator EnemyTurnReset (float delay) 
		{
			yield return new WaitForSeconds(delay);
			on_IA_wait = false;
			waiting_update = false;
		}

		public void Die() {
			Destroy (gameObject);
		}

		void IA_Choose (int action) {

			Vector3 bulletPos;
			Rigidbody2D bulletInstance;

			switch (action) {
			case 1:
				if (transform.position.y == 0) yDir = 1;
				else yDir = -2;
				base.AttemptMove <Wall> (0, yDir);
				break;
			case 2:
				bulletPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
				bulletInstance = Instantiate(Bullet_Enemy, bulletPos, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(bulletEnemySpeed, 0);
				break;
			default:
				if (transform.position.y != Player.instance.transform.position.y) {
					if (transform.position.y == 0) yDir = 1;
					else yDir = -2;
					base.AttemptMove <Wall> (0, yDir);
				}
				else {
					bulletPos = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
					bulletInstance = Instantiate(Bullet_Enemy, bulletPos, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(bulletEnemySpeed, 0);
				}
				break;
			}
		}

		protected override void OnCantMove <T> (T component)
		{
			return;
		}
	}
}