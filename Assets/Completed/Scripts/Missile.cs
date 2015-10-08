using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed {

	public class Missile : MonoBehaviour {

		public GameObject Target;

		private int RandomPick;

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
			RandomPick = Random.Range (0, 2);

			if (RandomPick < 3) {
				RandomPick = Random.Range (1, 6);
				for (int i = -1; i < 2; i++)
					PlaceSight (RandomPick + i);
			} else {
				int PlayerPos = Mathf.FloorToInt(Player.instance.transform.position.x);
				switch (PlayerPos){
				case 0:
					for (int i = -1; i < 2; i++)
						PlaceSight (1);
					break;
				case 6:
					for (int i = -1; i < 2; i++)
						PlaceSight (5);
					break;
				default:
					for (int i = -1; i < 2; i++)
						PlaceSight (PlayerPos);
					break;
				}
			}
		}

		void PlaceSight (int hor) {
			for (int j = 0; j < 3; j += 1)
				Instantiate (Target, new Vector3 (hor, j, transform.position.z), Quaternion.Euler (new Vector3 (0, 0, 0)));
		}

	}
}