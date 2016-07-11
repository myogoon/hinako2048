using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class tweet : MonoBehaviour {

	// Use this for initialization

	// Update is called once per frame

	public void clear_tweet(){
		//Application.OpenURL ("www.twitter.com/intent/tweet?text=hello+world&url=https://www.youtube.com/watch?v=ZKgvZYiE5ZE");
	}


	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			if (this.gameObject.name == "Button_tweet") {
				//Application.OpenURL ("www.twitter.com/intent/tweet?text=hello+world&url=https://www.youtube.com/watch?v=ZKgvZYiE5ZE");
			} 
			else if (this.gameObject.name == "Button_pause") {
				SceneManager.LoadScene ("Play",LoadSceneMode.Single); 
			}
			else if (this.gameObject.name == "clear_tweet") {
				//Application.OpenURL ("www.twitter.com/intent/tweet?text=hello+world&url=https://www.youtube.com/watch?v=ZKgvZYiE5ZE");
			}
			else if (this.gameObject.name == "clear_reset") {
				SceneManager.LoadScene ("Play",LoadSceneMode.Single); 
			}
			else if (this.gameObject.name == "clear_continue") {
				this.gameObject.SetActive (false);
			}
			else if (this.gameObject.name == "Button_pause") {
				SceneManager.LoadScene ("Play",LoadSceneMode.Single); 
			}
			else if (this.gameObject.name == "Button_pause") {
				SceneManager.LoadScene ("Play",LoadSceneMode.Single); 
			}
		} 

	
	}
}
