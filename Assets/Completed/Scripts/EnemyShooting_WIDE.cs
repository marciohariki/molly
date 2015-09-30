using UnityEngine;
using System.Collections;

namespace Completed {
	
	public class EnemyShooting_WIDE: MonoBehaviour {
		
		public float bulletEnemySpeed = -30f;
		public Rigidbody2D Bullet_Enemy;						//Prefab of bullet

		private bool test=false;
		
		//Awake is always called before any Start functions
		
		void Awake() {
		}
		
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			if (test == false) {
				StartCoroutine (CreateBullet());
				test = true;
			}
		}
		
		IEnumerator CreateBullet () {
		
			yield return new WaitForSeconds (0.5f);

			Vector3 bulletPos;
			Rigidbody2D bulletInstance;
			for( int i = 0; i < 3; i++){
				bulletPos = new Vector3(transform.position.x - 1, i, transform.position.z);
				bulletInstance = Instantiate(Bullet_Enemy, bulletPos, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(bulletEnemySpeed, 0);
			}

			Destroy (gameObject);
		}
	}
}