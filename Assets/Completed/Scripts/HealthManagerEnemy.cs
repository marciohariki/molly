using UnityEngine;
using System.Collections;

namespace Completed {
	
	public class HealthManagerEnemy : MonoBehaviour {
		
		public float scale = 1f;
		
		public float barSize = -104f;
		
		public RectTransform rectTransform;
		public static HealthManagerEnemy instance = null;		
		// Use this for initialization
		
		//Awake is always called before any Start functions
		void Awake()
		{
			//Check if instance already exists
			if (instance == null)
				
				//if not, set instance to this
				instance = this;
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);	
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad (gameObject);
		}
		
		void Update() {
			//		rectTransform.localPosition = new Vector3 ((1 - scale) * (-barSize), rectTransform.localPosition.y, rectTransform.localPosition.z);
			rectTransform.localScale = new Vector3 (scale, rectTransform.localScale.y, rectTransform.localScale.z);
			
			
		}
		
		public void getHit(float PlayerTag){
			if (scale > 0) {
				scale -= PlayerTag;
			} 
			if (scale == 0) {
				Destroy (GameObject.FindWithTag("Enemy"));
				scale = 1;
				
			}
		}
	}
}