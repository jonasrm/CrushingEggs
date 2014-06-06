using UnityEngine;
using System.Collections;

public class EffectRainEggs : MonoBehaviour {

	public GameObject prefabEgg;
	public float currentTime = 0f, indexGenerationEggs = .2f;
	public static bool run = true;

	// Update is called once per frame
	void Update () {

		if(!run)
			return;

		currentTime += Time.deltaTime;
		
		if (currentTime >= indexGenerationEggs)
		{
			currentTime = 0f;
			GameObject e = (GameObject)Instantiate(prefabEgg);
			e.transform.localScale = Vector3.one * Random.Range(-5, 1) * 0.1f;
		}
	}
}
