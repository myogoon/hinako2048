using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class view_sc : MonoBehaviour {
	// Use this for initialization

	Text viewscore=null;
	// Use this for initialization
	void Awake () {	
		viewscore = 	gameObject.GetComponent<Text> ();

	}

	// Update is called once per frame
	void LateUpdate () {
		if(this.gameObject.name=="Text_high"){
			viewscore.text = GameManager.highscore.ToString();
			}
		else if(this.gameObject.name=="Text_score"){
			viewscore.text = GameManager.score.ToString();
		}
		else
		{
		}


	}
}
