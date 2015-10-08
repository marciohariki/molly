using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.
	
	public class GameManager : MonoBehaviour
	{
		public float levelStartDelay = 2f;						//Time to wait before starting level, in seconds.
		public float turnDelay = 2f;							//Delay between each Player turn.
		public int playerPoints = 0;							//Starting value for Player food points.
		public static GameManager instance = null;				//Static instance of GameManager which allows it to be accessed by any other script.
		public bool playersTurn = true;		//Boolean to check if it's players turn, hidden in inspector but public.
		public bool waiting_update = false;
		public GameObject Enemy;								//Prefab to spawn for exit.
		public float PlayerPower = 0.25f;
		public GameObject[] enemies_list;						//List of enemies prefab

		private Text foodText;
		private Text levelText;									//Text to display current level number.
		private Text orderText;
		private Image enemy1;
		private Image enemy2;
		private Image enemy3;
		private GameObject slot1;
		private GameObject slot2;
		private GameObject slot3;
		private GameObject startButton;
		private GameObject levelImage;							//Image to block out level as levels are being set up, background for levelText.
		private GameObject orderImage;							//Image to block out level as levels are being set up, background for levelText.

		private BoardManager boardScript;						//Store a reference to our BoardManager which will set up the level.
		private int level = 0;									//Current level number, expressed in game as "Day 1".
		private bool enemiesMoving;								//Boolean to check if enemies are moving.
		public bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
		private Player playerCtrl;				//Reference to the Player script.
		private BoardManager BoardMngr;
		private int rand_check;
		private int rand_check2;

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
			DontDestroyOnLoad(gameObject);
			
			//Get a component reference to the attached BoardManager script
			boardScript = GetComponent<BoardManager>();
			
			//Call the InitGame function to initialize the first level 
			InitGame();
		}
		
		//This is called each time a scene is loaded.
		void OnLevelWasLoaded(int index)
		{
			//Add one to our level number.
			level++;
			playerPoints = 0;
			//Call InitGame to initialize our level.
			InitGame();
		}
		
		//Initializes the game for each level.
		void InitGame()
		{
			//While doingSetup is true the player can't move, prevent player from moving while title card is up.
			doingSetup = true;
			
			//Get a reference to our image LevelImage by finding it by name.
			levelImage = GameObject.Find("LevelImage");
			
			//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
			levelText = GameObject.Find("LevelText").GetComponent<Text>();
			orderText = GameObject.Find("OrderText").GetComponent<Text>();
			foodText = GameObject.Find("FoodText").GetComponent<Text>();
			enemy1 = GameObject.Find("Enemy1").GetComponent<Image>();
			enemy2 = GameObject.Find("Enemy2").GetComponent<Image>();
			enemy3 = GameObject.Find("Enemy3").GetComponent<Image>();
			slot1 = GameObject.Find("Slot1");
			slot2 = GameObject.Find("Slot2");
			slot3 = GameObject.Find("Slot3");
			startButton = GameObject.Find("StartButton");
			
			//Set the text of levelText to the string "Day" and append the current level number.
			levelText.text = "Game Start!";
			orderText.enabled = false;
			enemy1.enabled = false;
			enemy2.enabled = false;
			enemy3.enabled = false;
			
			slot1.SetActive(false);
			slot2.SetActive(false);
			slot3.SetActive(false);
			startButton.SetActive (false);


			//Set levelImage to active blocking player's view of the game board during setup.
			levelImage.SetActive(true);

			//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
			Invoke("HideLevelImage", levelStartDelay);

			//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
			Invoke("ShowOrderImage", levelStartDelay + 0.5f);

			//Call the SetupScene function of the BoardManager script, pass it current level number.

		}
		
	

		//Hides black image used between levels
		void HideLevelImage()
		{
			//Disable the levelImage gameObject.
			levelText.enabled = false;

		}

		//Hides black image used between levels
		void ShowOrderImage()
		{
			orderText.enabled = true;
			enemy1.enabled = true;
			enemy2.enabled = true;
			enemy3.enabled = true;
			slot1.SetActive(true);
			slot2.SetActive(true);
			slot3.SetActive(true);
			startButton.SetActive (true);
		}

		//Update is called every frame.
		void Update()
		{
			if (level > 2 && GameObject.FindWithTag ("Enemy") == null) {
				GameOver ();
			} else {

				if (GameObject.FindWithTag ("Enemy") == null && !doingSetup) {
					if(level > 0) {
						ShowInterlude();
						Invoke("HideInterlude", levelStartDelay);
					}
					rand_check = Random.Range (1, 3);
					if (rand_check != 1)
						rand_check = 3;
					Instantiate (enemies_list [level], new Vector3 (boardScript.columns - 1, boardScript.rows - rand_check, 0f), Quaternion.identity);
					level++;
					foodText.text = "Points: " + playerPoints;
				}
				
				if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

				//Check that playersTurn or enemiesMoving or doingSetup are not currently true.
				if (playersTurn || doingSetup)
					//If any of these are true, return and do not start MoveEnemies.
					return;
				if (!waiting_update) {
					StartCoroutine (PlayerTurnReset ());
					waiting_update = true;
				}
			}
		}

		IEnumerator PlayerTurnReset () 
		{
			yield return new WaitForSeconds(turnDelay);
			playersTurn = true;
			waiting_update = false;
		}

		//GameOver is called when the player reaches 0 food points
		public void GameOver()
		{
			//Set levelText to display number of levels passed and game over message
			levelText.text = "You scored " + playerPoints + " points.";
			levelText.enabled = true;
			
			orderText.enabled = false;
			enemy1.enabled = false;
			enemy2.enabled = false;
			enemy3.enabled = false;
			
			slot1.SetActive(false);
			slot2.SetActive(false);
			slot3.SetActive(false);
			startButton.SetActive (false);
			//Enable black background image gameObject.
			levelImage.SetActive(true);

			StartCoroutine(ResetGame ());
			
			//Disable this GameManager.
			enabled = false;
			level = 0;
		}

		void ShowInterlude() {
			//Set levelText to display number of levels passed and game over message
			levelText.text = "You killed Enemy " + level;
			levelText.enabled = true;
			
			orderText.enabled = false;
			enemy1.enabled = false;
			enemy2.enabled = false;
			enemy3.enabled = false;
			
			slot1.SetActive(false);
			slot2.SetActive(false);
			slot3.SetActive(false);
			startButton.SetActive (false);
			levelImage.SetActive(true);

			doingSetup = true;
		}

		void HideInterlude() {
			levelImage.SetActive(false);
			
			doingSetup = false;
		}
		GameObject[] orderEnemies() {
			GameObject[] aux_enemies_list = new GameObject[3];

			switch (slot1.transform.GetChild (0).transform.name) {
				case ("Enemy1"):
					aux_enemies_list[0] = enemies_list[0];
				break;

				case ("Enemy2"):
					aux_enemies_list[0] = enemies_list[1];
				break;

				case ("Enemy3"):
					aux_enemies_list[0] = enemies_list[2];
				break;
			}

			switch (slot2.transform.GetChild (0).transform.name) {
				case ("Enemy1"):
					aux_enemies_list[1] = enemies_list[0];
					break;
					
				case ("Enemy2"):
					aux_enemies_list[1] = enemies_list[1];
					break;
					
				case ("Enemy3"):
					aux_enemies_list[1] = enemies_list[2];
					break;
			}

			switch (slot3.transform.GetChild (0).transform.name) {
				case ("Enemy1"):
					aux_enemies_list[2] = enemies_list[0];
					break;
					
				case ("Enemy2"):
					aux_enemies_list[2] = enemies_list[1];
					break;
					
				case ("Enemy3"):
					aux_enemies_list[2] = enemies_list[2];
					break;
			}
			return aux_enemies_list;
		}

		public void finishSetup()
		{	
			enemies_list = orderEnemies ();
			levelImage.SetActive(false);
			doingSetup = false;
			boardScript.SetupScene(level + 1);
		}

		IEnumerator ResetGame () {
			yield return new WaitForSeconds (3);
			Application.LoadLevel (Application.loadedLevel);
			enabled = true;
		}

	}
}

