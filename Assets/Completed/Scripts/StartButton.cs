using UnityEngine;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.


	public class StartButton : MonoBehaviour {
		
		private GameObject gameManager;		

		// Use this for initialization
		void Start () {


		}
		
		// Update is called once per frame
		void Update () {

		}

		public void startGame(){
			GameManager.instance.finishSetup ();
		
		}
	}
}
