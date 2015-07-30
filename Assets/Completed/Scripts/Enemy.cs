using UnityEngine;
using System.Collections;

namespace Completed {

	public class Enemy : MovingObject {

		public bool waiting_update = false;
		public bool on_IA_wait = true;								//flag for checking if is waiting to act


		private float MeanDelayActions = 1.5f;						//mean time between 2 actions
		private float CurrentDelay;									//delay for the current moment
		private int yDir;
			
		// Use this for initialization
		void Start () {
			CurrentDelay = MeanDelayActions;
			base.Start ();
		}
	
		// Update is called once per frame
		void Update () {
			if (waiting_update == false && on_IA_wait == true) {
				StartCoroutine (EnemyTurnReset (CurrentDelay));
				waiting_update = true;
				return;
			}

			if (on_IA_wait == false) {
				if (transform.position.y == 0) yDir = 1;
				else yDir = -2;

				base.AttemptMove <Wall> (0, yDir);
				on_IA_wait = true;
			}
			
		}

		IEnumerator EnemyTurnReset (float delay) 
		{
			yield return new WaitForSeconds(delay);
			on_IA_wait = false;
			waiting_update = false;
		}

		protected override void OnCantMove <T> (T component)
		{
			return;
		}
	}
}