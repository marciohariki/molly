using UnityEngine;
using System.Collections;

namespace Completed {
	
	public class Explosion_FX : MonoBehaviour {

		public AnimationClip Explosm;
		public float Dam = 0.2f;

		private bool check = false;

		void Start () {

		}

		void Update () {
			if(check == false) StartCoroutine (EndOfAnim ());
		}

		IEnumerator EndOfAnim () {
			check = true;
			yield return new WaitForSeconds(Explosm.length);
			Destroy (gameObject);
		}
		
	}
}