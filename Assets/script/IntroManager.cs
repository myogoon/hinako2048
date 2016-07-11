using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	public string SceneName;
	public GameObject bg;
	public GameObject fade;
	int sound_check;
	bool i;
	// Use this for initialization
	void Start () {

		GameObject aaa = GameObject.Find ("bgm");
		sound_check = PlayerPrefs.GetInt ("sound_flag");
		if (sound_check ==0){
			aaa.GetComponent<AudioSource> ().Stop ();
		}
			
		//Screen.SetResolution (354, 630, false); //윈도우 빌드시에만?
		i = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		

		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetMouseButton (0)) {

			fade.SetActive (true);


		} else if (Input.GetKeyDown (KeyCode.Backspace)|| Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit ();
		}

		if (i==true){
			bg.transform.position = new Vector3 (bg.transform.position.x + 0.01f, bg.transform.position.y, bg.transform.position.z);
		}
		else if (i==false){
			bg.transform.position = new Vector3 (bg.transform.position.x - 0.01f, bg.transform.position.y,bg.transform.position.z);
		}
		if(bg.transform.localPosition.x >=230){
			i=false;
		}
		else if(bg.transform.localPosition.x <=-50){
			i=true;
		}
	
	}
}

