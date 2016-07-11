using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class view_image : MonoBehaviour {
	public int x;
	public int y;
	public int original_x;
	public int original_y;
	public int target_x;
	public int target_y;
	public int combination;
	public int moved;
	public bool grow;
	Image a;
	RectTransform r;
	public Texture2D t_2;
	public Texture2D t_4;
	public Texture2D t_8;
	public Texture2D t_16;
	public Texture2D t_32;
	public Texture2D t_64;
	public Texture2D t_128;
	public Texture2D t_256;
	public Texture2D t_512;
	public Texture2D t_1024;
	public Texture2D t_2048;
	public Texture2D t_4096;
	// Use this for initialization

	void Awake () {	
		a =	gameObject.GetComponent<Image> ();
		r = gameObject.GetComponent<RectTransform> ();
		t_2 = Resources.Load ("2") as Texture2D;
		t_4 = Resources.Load ("4") as Texture2D;
		t_8 = Resources.Load ("8") as Texture2D;
		t_16 = Resources.Load ("16") as Texture2D;
		t_32 = Resources.Load ("32") as Texture2D;
		t_64 = Resources.Load ("64") as Texture2D;
		t_128 = Resources.Load ("128") as Texture2D;
		t_256 = Resources.Load ("256") as Texture2D;
		t_512 = Resources.Load ("512") as Texture2D;
		t_1024 = Resources.Load ("1024") as Texture2D;
		t_2048 = Resources.Load ("2048") as Texture2D;
		t_4096 = Resources.Load ("66338") as Texture2D;

	}

	// Update is called once per frame
	void Update () {
		if (GameManager.gridmap [x] [y] == 2) {
			a.sprite = Sprite.Create (t_2, new Rect (0, 0, t_2.width, t_2.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 4) {
			a.sprite = Sprite.Create (t_4, new Rect (0, 0, t_4.width, t_4.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 8) {
			a.sprite = Sprite.Create (t_8, new Rect (0, 0, t_8.width, t_8.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 16) {
			a.sprite = Sprite.Create (t_16, new Rect (0, 0, t_16.width, t_16.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 32) {
			a.sprite = Sprite.Create (t_32, new Rect (0, 0, t_32.width, t_32.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 64) {
			a.sprite = Sprite.Create (t_64, new Rect (0, 0, t_64.width, t_64.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 128) {
			a.sprite = Sprite.Create (t_128, new Rect (0, 0, t_128.width, t_128.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 256) {
			a.sprite = Sprite.Create (t_256, new Rect (0, 0, t_256.width, t_256.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 512) {
			a.sprite = Sprite.Create (t_512, new Rect (0, 0, t_512.width, t_512.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 1024) {
			a.sprite = Sprite.Create (t_1024, new Rect (0, 0, t_1024.width, t_1024.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 2048) {
			a.sprite = Sprite.Create (t_2048, new Rect (0, 0, t_2048.width, t_2048.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}
		else if(GameManager.gridmap [x] [y] == 4096) {
			a.sprite = Sprite.Create (t_4096, new Rect (0, 0, t_4096.width, t_4096.height), Vector2.zero);
			//a.gameObject.SetActive (false);
		}

		if (transform.localScale.x < 1f) { 
			transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (1f, 1f, 1f), Time.deltaTime);
		}

		if (r.localPosition.x == target_x * 127 - 190 && r.localPosition.y == -target_y * 127 + 50 && combination == 0) {
			original_x = x;
			original_y = y;
		}
		else if (r.localPosition.x ==  target_x * 127 - 190 && r.localPosition.y == -target_y * 127 + 50 && combination == 1) {
			original_x = x;
			original_y = y;
			GameManager.keydown_errorcheck = 1;
			Invoke ("death", 0.2f);
			combination = 0;

			//Destroy(this.gameObject); 
		} 
		else {
			r.transform.localPosition = Vector2.MoveTowards(r.localPosition, new Vector2(target_x * 127 - 190, -target_y * 127 + 50), 5500*Time.deltaTime);
			//r.transform.position = Vector2.MoveTowards (r.position, new Vector2 (x * 127 - 190, -y * 127 - 430), 300*Time.deltaTime);
			//transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (x * 127 - 190, -y * 127 - 430, 1f), Time.deltaTime /100 );
		} 
		//	Manager.done = true;
		
		//viewscore.text = GameManager.gridmap [x] [y].ToString();

	}
	void death(){
		Destroy (this.gameObject);
		GameManager.keydown_errorcheck = 0;
	}

}
