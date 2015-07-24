﻿using UnityEngine;
using System.Collections;

namespace Completed {

	public class BulletControler : MonoBehaviour {
			
		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			if (transform.position.x > 10 || transform.position.x < 0 || Mathf.Abs (transform.position.y) > 3)
				Destroy (gameObject);
		}
	}
}