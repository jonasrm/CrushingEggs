using UnityEngine;
using System.Collections;

public class EggMoveEffect : MonoBehaviour {
	
	private int variationSpeed = 0;
	private int rotatioSpeed = 0;
	private Vector3 screenPoint;
	private Vector3 offset;
	private float randomPosition = .5f;
	private int randomRotation = 20;
	
	// Use this for initialization
	void Start ()
	{
		this.transform.position = GameObject.Find("effectPoint").transform.position;
		this.transform.position += new Vector3(Random.Range (-randomPosition, randomPosition), 0);
		rotatioSpeed = Random.Range(-randomRotation, randomRotation) * 10;
		variationSpeed = Random.Range(0, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.Translate (new Vector3(0, variationSpeed, 0) * Time.deltaTime);
		this.gameObject.transform.Rotate (new Vector3 (0, 0, rotatioSpeed) * Time.deltaTime);

		if (this.transform.position.y < GameObject.Find("deadPoint").transform.position.y)
		{
			Destroy(this.gameObject);
		}

	}

}
