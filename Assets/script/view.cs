using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class view : MonoBehaviour {
	public int x;
	public int y;
	Text viewscore=null;
	// Use this for initialization
	void Awake () {	
		viewscore = 	gameObject.GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void LateUpdate () {

		viewscore.text = GameManager.gridmap [x] [y].ToString();

	}
}
