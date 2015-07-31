using UnityEngine;
using System.Collections;

namespace Completed {

	public class BulletControler : MonoBehaviour {


		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			if (transform.position.x > 10 || transform.position.x < 0 || Mathf.Abs (transform.position.y) > 3)
				Destroy (gameObject);
		}

		void OnCollisionEnter2D(Collision2D collision) {

			switch (collision.gameObject.tag) {
			
			case "Enemy":
				Enemy.instance.Die();
				Destroy (gameObject);
				break;
			
			case "Wall":
				Destroy (gameObject);
				break;

			case "Player":
				HealthManager.instance.getHit("square");
				Destroy (gameObject);
				break;
			
			}

		}
	}
}