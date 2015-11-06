using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed {
	
	public class Enemy_Boss : Enemy {
		
		public bool waiting_update = false;
		public bool on_IA_wait = true;								//flag for checking if is waiting to act
		public GameObject EnemyShootingObj;						//Prefab of bomb source
		public GameObject EnemyShootingObj2;						//Prefab of bomb source
		
		private float CurrentDelay;									//delay for the current moment
		private int action_pick;
		
		//Awake is always called before any Start functions
		void Awake()
		{
			base.Awake ();
		}
		
		// Use this for initialization
		void Start () {
			base.MeanDelayActions = 1f;
			base.EnemyDef = 0f; 								//Enemy Life
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
				
				action_pick = Random.Range(1, 4);
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
		
		void IA_Choose (int action) {
			
			switch (action) {
			case 1:
				base.MoveAction ();
				break;
			default:
				ShootAction ();
				break;
			}
		}
		
		void ShootAction () {
			
			int rand_check = Random.Range (1, 3);

			if (rand_check == 1) {
				Instantiate (EnemyShootingObj, new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
			
			} else {

				Instantiate(EnemyShootingObj2, transform.position, Quaternion.Euler(new Vector3(0,0,0)));


			}
				
		}
		
		protected override void OnCantMove <T> (T component)
		{
			return;
		}
	}
}