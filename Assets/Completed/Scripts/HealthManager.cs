using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	public float scale = 0f;

	public float barSize = -104f;

	public RectTransform rectTransform;

	void Update() {
//		rectTransform.localPosition = new Vector3 ((1 - scale) * (-barSize), rectTransform.localPosition.y, rectTransform.localPosition.z);
		rectTransform.localScale = new Vector3 (scale, rectTransform.localScale.y, rectTransform.localScale.z);

	}
	
	public void getHit(string enemyTag){
		print (enemyTag);
		if (scale > 0) {
			switch (enemyTag) {
				
			case "square":
				scale -= 0.1f;
				break;
			case "circle":
				scale -= 0.2f;
				break;
				
			}
		}
		
	}
}
