using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class fade_in : MonoBehaviour {
	private Image image;  // SpriteRenderer 변수 생성
	public GameObject bg;
	public GameObject start_sound;
	public static float a;
	void Start () {
		a = 0;
		image =	gameObject.GetComponent<Image> ();
		GameManager.sound_flag = PlayerPrefs.GetInt("sound_flag"); 
		if (GameManager.sound_flag == 1) {
			start_sound.GetComponent<AudioSource> ().Play ();
		}
	}
	void Update () {
		if (a < 1f) {
			image.color = new Color (0, 0, 0, a);
			a += 0.01f;

		}
		if (a >= 1f) {
			SceneManager.LoadScene ("Play"); 

		}
	}
}
