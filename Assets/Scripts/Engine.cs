using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	public GameObject prefabEggGold;
	public GameObject prefabEggWhite;

	private static int lives, score;
	
	private static float indexGenerationEggs, indexGravity, currentTime;
	
	void Awake()
	{
		lives = 5;
		score = 0;
		currentTime = 0f;
		indexGenerationEggs = 2;
		indexGravity = 2;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(20, 10, 200, 100), "Lives: " + lives);
		GUI.Label(new Rect(20, 30, 200, 100), "Score: " + score);
	}

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

			if ((score % 2) == 0)
			{
				indexGenerationEggs -= .1f;
				indexGravity += .5f;
			}
		}
	}

	// Use this for initialization
	void Start () {

		Instantiate(prefabEggWhite);

	}
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;

		if (currentTime >= indexGenerationEggs)
		{
			currentTime = 0f;
			GameObject egg = (GameObject)Instantiate(prefabEggWhite);
			egg.rigidbody2D.gravityScale = indexGravity;
		}

		if (lives <= 0)
		{
			Time.timeScale = 0;
			lives = 0;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
