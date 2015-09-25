using UnityEngine;
using System.Collections;

namespace Completed {

	public class Target_Explosion : MonoBehaviour {

		public GameObject Explosion;

		private bool check = false;

		
		void Start () {
			
		}
		
		void Update () {
			if(check == false) StartCoroutine (EndOfAnim ());
		}
		
		IEnumerator EndOfAnim () {
			check = true;
			yield return new WaitForSeconds(1f);
			Instantiate (Explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
			Destroy (gameObject);
		}
	}
}