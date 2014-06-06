using UnityEngine;
using System.Collections;

public class EggMove : MonoBehaviour {
	
	public bool isGold;
	public AudioClip sound_crash;
	public AudioClip sound_lose;
	private int variationSpeed = 0;
	private int rotatioSpeed = 0;
	private Vector3 screenPoint;
	private Vector3 offset;
	private float randomPosition = 2;
	private int randomRotation = 20;

	// Use this for initialization
	void Start ()
	{
		this.transform.position = GameObject.Find("spawPoint").transform.position;
		this.transform.position += new Vector3(Random.Range (-randomPosition, randomPosition), 0);
		rotatioSpeed = Random.Range(-randomRotation, randomRotation) * 10;
		variationSpeed = Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.Rotate (new Vector3 (0, 0, rotatioSpeed) * Time.deltaTime);
		this.gameObject.transform.Translate (new Vector3(0, variationSpeed, 0) * Time.deltaTime);
		
		if (this.transform.position.y < GameObject.Find("deadPoint").transform.position.y)
		{
			AudioSource.PlayClipAtPoint(sound_lose, transform.position);
			if (!isGold)
				GameEngine.Lives -= 1;

			Destroy(this.gameObject);
		}
		
		if (Input.GetMouseButtonDown(0))			
		{			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);			
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			
			if(hit.collider != null && hit.collider.transform == this.transform)
			{
				AudioSource.PlayClipAtPoint(sound_crash, transform.position);
				GameEngine.Score += 1;

				if (isGold)
					GameEngine.Score += 1;

				Destroy(this.gameObject);
			}
		}
	}
	
}
