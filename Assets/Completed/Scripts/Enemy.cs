using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed {

	public class Enemy : MovingObject {

		public static Enemy instance = null;
		public float EnemyDef;
		public int yDir;
		public float MeanDelayActions;						//mean time between 2 actions

		//Awake is always called before any Start functions

		public void Awake()
		{
			//Check if instance already exists
			if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);
			
		}

		public void Die() {
			Destroy (gameObject);
		}

		public void MoveAction () {
			if (transform.position.y == 0) yDir = 1;
			else yDir = -2;
			base.AttemptMove <Wall> (0, yDir);
		}

		protected override void OnCantMove <T> (T component)
		{
			return;
		}
	}
}