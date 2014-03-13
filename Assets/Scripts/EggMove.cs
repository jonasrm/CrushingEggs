using UnityEngine;
using System.Collections;

public class EggMove : MonoBehaviour {

	public Transform spawPoint;
	public Transform deadPoint;

	private int rotatioSpeed = 0;
	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		this.transform.position = spawPoint.transform.position;
		this.transform.position += new Vector3(Random.Range (-15f, 15f), 0);
		rotatioSpeed = Random.Range(-10, 10) * 10;

	}
	
	// Update is called once per frame
	void Update () {

		this.gameObject.transform.Rotate (new Vector3 (0, 0, rotatioSpeed) * Time.deltaTime);

        //if (this.transform.position.y < deadPoint.transform.position.y)
        //{
        //    Engine.Lives -= 1;
        //    Destroy(this.gameObject);
        //}

		if (Input.GetMouseButtonDown(0))			
		{			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);			
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			
			if(hit.collider != null && hit.collider.transform == this.transform)
			{
				Engine.Score += 1;
				Destroy(this.gameObject);
			}
		}

	}

	/*
	void OnMouseDown()
	{
		Destroy(this.gameObject);
		Engine.Score += 1;
	}
	*/

}
