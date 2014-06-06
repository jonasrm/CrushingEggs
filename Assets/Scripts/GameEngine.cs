using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	#region Variables

	public GameObject prefabGoldEgg, prefabNormalEgg;
	public static GameObject goldEgg, normalEgg;
	public AudioClip sound_button_click;

	public enum gameMode {
		Menu,
		inGame,
		Pause,
		GameOver
	}

	public static gameMode status;

	private static int lives, score, record, nextLevelCount;
	private static float indexGenerationEggs, currentTime;
	private Transform button_start, button_exit, button_pause, button_back, button_menu;


	public static int Lives {
		get {
			return lives;
		}
		set {
			lives = value;
		}
	}

	public static int Score {
		get {
			return score;
		}
		set {
			score = value;
		}
	}

	#endregion

	#region Main

	// Use this for initialization
	void Start () {
		record = PlayerPrefs.GetInt("record");
		goldEgg = prefabGoldEgg;
		normalEgg = prefabNormalEgg;
		button_start = GameObject.Find("button_start").transform;
		button_exit = GameObject.Find("button_exit").transform;
		button_pause = GameObject.Find("button_pause").transform;
		button_back = GameObject.Find("button_back").transform;
		button_menu = GameObject.Find("button_menu").transform;
		setMenu();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
			MouseDown();
		
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (status == gameMode.inGame)
		{
			Run();
		}

	}

	#endregion

	#region GUI

	void OnGUI()
	{
		if (status == gameMode.inGame)
		{
			GUI.Box(new Rect (-5, 10, 120, 80), "");
			GUI.Label(new Rect(20, 20, 200, 100), "Record: " + record);
			GUI.Label(new Rect(20, 40, 200, 100), "Score: " + score);
			GUI.Label(new Rect(20, 60, 200, 100), "Lives: " + lives);
		}
	}

	#endregion

	#region Mouse

	private void MouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

		if (hit.collider != null)
		{
			AudioSource.PlayClipAtPoint(sound_button_click, transform.position);
			if (hit.collider.transform == button_start)
				setInGame(true);
			else if (hit.collider.transform == button_exit)
				Application.Quit();
			else if (hit.collider.transform == button_pause)
				setPause();
			else if (hit.collider.transform == button_back)
				setInGame();
			else if (hit.collider.transform == button_menu)
				setMenu();
		}

	}

	#endregion

	#region GameMode

	public static void setMenu()
	{
		Time.timeScale = 1;
		EffectRainEggs.run = true; //start raining
		status = gameMode.Menu;
		MoveCamera(1);
	}

	public static void setInGame(bool reset = false)
	{
		if (reset)
			resetVar();

		Time.timeScale = 1;
		EffectRainEggs.run = false; //stop raining
		status = gameMode.inGame;
		MoveCamera(2);
	}

	public static void setPause()
	{
		Time.timeScale = 0;
		status = gameMode.Pause;
		MoveCamera(3);
	}

	public static void setGameOver()
	{
		status = gameMode.GameOver;
		GameObject.Find("button_back").renderer.enabled = false;
		MoveCamera(3);
	}

	private static void resetVar()
	{
		lives = 3;
		score = 0;
		currentTime = 0f;
		indexGenerationEggs = 2f;
		nextLevelCount = 0;
		GameObject.Find("button_back").renderer.enabled = true;

		GameObject[] eggs = GameObject.FindGameObjectsWithTag("egg");
		foreach( GameObject i in eggs)
		{
			Destroy(i);
		}

	}

	#endregion

	#region Camera

	private static void MoveCamera(int scene)
	{
		switch (scene)
		{
		case 1:
			Camera.main.transform.position = GameObject.Find("scene_1").transform.position;
			break;
		case 2:
			Camera.main.transform.position = GameObject.Find("scene_2").transform.position;
			break;
		case 3:
			Camera.main.transform.position = GameObject.Find("scene_3").transform.position;
			break;
		}

	}

	#endregion

	#region Game

	private void Run()
	{
		if (lives <= 0)
		{
			setGameOver();
			return;
		}

		if (score > record)
		{
			record = score;
			PlayerPrefs.SetInt("record", score);
		}

		if (score > 0 && score % 5 == 0)
		{
			if (nextLevelCount == 0)
			{
				nextLevelCount = 1;
				indexGenerationEggs -= (indexGenerationEggs * .2f);

				if (Random.Range(0,1) == 1)
				{
					Instantiate(prefabGoldEgg);
				}

			}
		}
		else
		{
			nextLevelCount = 0;
		}

		currentTime += Time.deltaTime;
		
		if (currentTime >= indexGenerationEggs)
		{
			currentTime = 0f;
			Instantiate(normalEgg);
		}
	}

	#endregion

}
