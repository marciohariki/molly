using UnityEngine;
using System.Collections;

namespace Completed {

	public class BulletControler : MonoBehaviour {

		public float EnemyDam = 0.10f;
		public float PlayerDam = 0.25f;

		void Start () {
		
		}

		void Awake () {
			PlayerDam = GameManager.instance.PlayerPower * (1-Enemy.instance.EnemyDef);
		}

		// Update is called once per frame
		void Update () {
			if (transform.position.x > 10 || transform.position.x < 0 || Mathf.Abs (transform.position.y) > 3)
				Destroy (gameObject);
		}

		void OnCollisionEnter2D(Collision2D collision) {

			switch (collision.gameObject.tag) {
			
			case "Enemy":
				HealthManagerEnemy.instance.getHit(PlayerDam);
				Destroy (gameObject);
				break;
			
			case "Wall":
				Destroy (gameObject);
				break;

			case "Player":
				HealthManager.instance.getHit(EnemyDam);
				Destroy (gameObject);
				break;
			case "Bullet":
				Destroy (gameObject);
				break;
			}

		}
	}
}