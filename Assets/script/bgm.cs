using UnityEngine;
using System.Collections;

public class bgm : MonoBehaviour {
	private static bool played = false;
	// Use this for initialization
	void Start () {
		if (played == false) {
			DontDestroyOnLoad (this.gameObject);
			//this.gameObject
			played = true;
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
