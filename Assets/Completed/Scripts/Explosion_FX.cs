using UnityEngine;
using System.Collections;

namespace Completed {
	
	public class Explosion_FX : MonoBehaviour {

		public float Anim_Length;

		private Animation Explosm;

		void Awake () {
			Explosm = GetComponent<Animation> ();
				//GetComponent<AnimationClip>;
		}

		void Update () {
			StartCoroutine (EndOfAnim ());
		}

		IEnumerator EndOfAnim () {
			yield return new WaitForSeconds(0.3f);
			Destroy (gameObject);
		}
		
	}
}