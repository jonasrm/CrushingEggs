using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour
{

    public GUIStyle style;

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;

    void OnGUI()
    {

        int baseLeft = 40;
        int baseTop = 20;
        int baseHeight = 60;

        if ( GUI.Button ( new Rect ( 10, 10, 100, 50 ), "Shake" ))
        {
            Shake();
        }

        if (GUI.Button( new Rect(baseLeft / 2, Screen.height - baseHeight - baseTop, Screen.width - baseLeft, baseHeight), "Exit"))
        {
            //audio.PlayOneShot(sound_click);
            Application.Quit();
        }
        
        baseTop += baseHeight + 20;
        if (GUI.Button(new Rect(baseLeft / 2, Screen.height - baseHeight - baseTop, Screen.width - baseLeft, baseHeight), "Start"))
        {
            //audio.PlayOneShot(sound_click);
            AutoFade.LoadLevel("Game", 1f, 1f, Color.black);
        }
        
        //if(GUI.Button(new Rect(70, 340, 245, 75), "", style2)){
        //audio.PlayOneShot(sound_click);
        //AutoFade.LoadLevel("Tutorial", 1.5f, 1.5f, Color.black);
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (shake_intensity > 0)
        {
            Camera.main.transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            Camera.main.transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
        }

    }

    void Shake()
    {
        originPosition = Camera.main.transform.position;
        originRotation = Camera.main.transform.rotation;
        shake_intensity = .3f;
        shake_decay = 0.002f;
    }

}
