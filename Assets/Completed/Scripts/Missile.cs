using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed {

	public class Missile : MonoBehaviour {

		public GameObject Target;

		private int[] RandomPick = new int[] {0, 0, 0};

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			if (transform.position.y > 3) {
				targeting ();
				Destroy(gameObject);
			}
		}

		void targeting () {
			RandomPick[0] = Random.Range (0, 7);
			RandomPick[1] = Random.Range (0, 7);
			while (RandomPick[0] == RandomPick[1]) 
				RandomPick[1] = Random.Range (0, 7);
			RandomPick[2] = Random.Range (0, 7);
			while (RandomPick[0] == RandomPick[2] | RandomPick[1] == RandomPick[2]) 
				RandomPick[2] = Random.Range (0, 7);

			for (int i = 0; i < 3 ; i++) PlaceSight (RandomPick[i]);
		}

		void PlaceSight (int hor) {
			for (int j = 0; j < 3; j += 1)
				Instantiate (Target, new Vector3 (hor, j, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
		}

	}
}