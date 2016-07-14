using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public int check_value;
	public static int keydown_errorcheck;
	int start_spawn=1;
	public int check_rotate;
	public static int score = 0;
	public static int highscore = 0;
	public static int clear2048 = 0;
	public static int sound_flag;
	public GameObject highScoreText;
	public GameObject clearui;
	public GameObject gameoverui;
	public GameObject exitui;
	public GameObject pause_ui;
	public GameObject panel;
	public GameObject pause_soundon;
	public GameObject pause_soundoff;
	public GameObject swipe_sound;
	public GameObject gameover_sound;
	public GameObject clear_sound;
	Vector2 firstposition;
	Vector2 secondposition;
	Vector2 currentswipe;
	float swipe_timer;


	int gameover_check = 0;
	int exit_ui_check = 0;
	int clear_check = 0;
	int pause_check = 0;

	public Canvas canvas;
	List<GameObject> Obj_List = new List<GameObject>();
	List<GameObject> Obj_List2 = new List<GameObject>();
	public static int[][] gridmap = new int[4][]{
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
	};

	public static int[][] checkmap = new int[4][]{
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
		new int[] {0,0,0,0},
	};

	public static int[] testmap = new int[4];
	public static int[] tempmap = new int[4];
	public static int resetkey = 0;
	// Use this for initialization
	public void Button_reset(){
		SceneManager.LoadScene ("Play",LoadSceneMode.Single); 
	}
	public void Button_continue(){
		clear_check = 0;
		panel.SetActive (false);
		clearui.SetActive (false);
	}
	public void Button_tweet(){
		Application.OpenURL ("https://www.twitter.com/intent/tweet?text=%E3%82%84%E3%81%A3%E3%81%B1Hinako2048%E3%81%A0%E3%81%AD%28%E2%97%8F+%C2%B4+%E2%96%BD+%60%E2%97%8F%29%EF%BE%89%E2%99%A1"+"%0AAndroid : https://goo.gl/NZmhkT%0AIOS : "+"https://goo.gl/ej197N"+"%0A" + "&hashtags=hinako2048");
	


	}

	public void Button_gameover_tweet(){
		

		Application.OpenURL ("https://www.twitter.com/intent/tweet?text=%E6%9C%80%E7%B5%82%E3%82%B9%E3%82%B3%E3%82%A2%E3%81%AB%E3%82%85%E3%83%BC%E3%82%93%28%E2%97%8F%E3%83%BB%CF%89%E3%83%BB%E2%97%8F%29%0A%E2%96%B6"+score+"%E7%82%B9%0AAndroid : https://goo.gl/NZmhkT%0AIOS : "+"https://goo.gl/ej197N"+"%0A" + "&hashtags=hinako2048");
	


	}

	public void button_exit(){
		Application.Quit ();
	}
	public void button_ui_exit(){
		if (exit_ui_check == 0) {
			exitui.SetActive (true);
			exitui.transform.SetAsLastSibling ();
			exit_ui_check = 1;
		} 
		else {
			exitui.SetActive (false);
			exit_ui_check = 0;
		}

	}
	public void button_cencel(){
		exitui.SetActive (false);
		exit_ui_check = 0;
	}

	public void button_pause(){
		if (gameover_check == 0 && clear_check ==0) {
			pause_check = 1;
			panel.SetActive (true);
			pause_ui.SetActive (true);
			panel.transform.SetAsLastSibling ();
			pause_ui.transform.SetAsLastSibling ();
			sound_flag = PlayerPrefs.GetInt ("sound_flag"); 
			if (sound_flag == 1) {
				pause_soundon.SetActive (true);
				pause_soundoff.SetActive (false);
			} else if (sound_flag == 0) {
				pause_soundon.SetActive (false);
				pause_soundoff.SetActive (true);
			}

		}
	}

	public void button_pause_exit(){
		if (gameover_check == 0&& clear_check ==0) {
			panel.SetActive (false);
			pause_ui.SetActive (false);
			pause_check = 0;
		}
	}

	public void button_home(){
		SceneManager.LoadScene ("Intro"); 
	}

	public void button_soundon(){
		GameObject aaa = GameObject.Find ("bgm");
		if (aaa != null) {
			aaa.GetComponent<AudioSource> ().Pause ();
		}
		swipe_sound.SetActive (false);
		gameover_sound.SetActive (false);
		clear_sound.SetActive (false);
		pause_soundoff.SetActive (true);
		pause_soundon.SetActive (false);
		PlayerPrefs.SetInt ("sound_flag", 0);
	}

	public void button_soundoff(){
		GameObject aaa = GameObject.Find ("bgm");
		if (aaa != null) {
			aaa.GetComponent<AudioSource> ().Play ();
		}
		PlayerPrefs.SetInt ("sound_flag", 1);
		swipe_sound.SetActive (true);
		gameover_sound.SetActive (true);
		clear_sound.SetActive (true);
		pause_soundon.SetActive (true);
		pause_soundoff.SetActive (false);

	}




	void Awake () {
		swipe_timer = 0;
		int i, j;
		for (i = 0; i < 4; i++) {
			for (j = 0; j < 4; j++) {
				gridmap [i] [j] = 0;
				checkmap [i] [j] = 0;
			}
		}

		start_spawn =1;
		clear2048 = 0;
		resetkey = 0;
		clearui.SetActive (false);
		gameoverui.SetActive (false);
		highscore = PlayerPrefs.GetInt("high_score"); 
		sound_flag = PlayerPrefs.GetInt("sound_flag"); 
		score = 0;
		keydown_errorcheck = 0;
		canvas = FindObjectOfType<Canvas> ();
		check_value = 0;
		exit_ui_check = 0;
		gameover_check = 0;
		Spawn();
		Spawn();
		Spawn();
		//		Spawn();Spawn();Spawn();
		//		Spawn();Spawn();Spawn();
		//		Spawn();Spawn();Spawn();
		//		Spawn();Spawn();Spawn();Spawn();
		start_spawn = 0;

	}

	// Update is called once per frame


	int check_change(int[][] grid, int[][] grid2){
		int count=0;
		for (int x = 0; x < 4; x++) {
			for (int y = 0; y < 4; y++) {
				if(grid[x][y] != grid2[x][y]){
					count++;
				}
			}
		}
		return count;
	}



	void set_target(){
		int i, j;
		for (i = 0; i < 4; i++) {
			for (j = 0; j < 4; j++) {
				GameObject temp = GameObject.Find (i.ToString() + "," + j.ToString ());

				if (temp) {
					temp.GetComponent<view_image> ().target_x = temp.GetComponent<view_image> ().x;
					temp.GetComponent<view_image> ().target_y = temp.GetComponent<view_image> ().y;
					//Obj_List.Add (temp);
					//	Debug.Log (i.ToString () + "," + j.ToString () + "의 x,y값 변경");
				}

			}
		}



		for (i = 0; i < 4; i++) {
			for (j = 0; j < 4; j++) {
				GameObject temp2 = GameObject.Find ("d" + i.ToString() + "," + j.ToString ());
				if (temp2) {
					temp2.GetComponent<view_image> ().target_x = temp2.GetComponent<view_image> ().x;
					temp2.GetComponent<view_image> ().target_y = temp2.GetComponent<view_image> ().y;
					//Obj_List.Add (temp);
					//	Debug.Log (i.ToString () + "," + j.ToString () + "의 x,y값 변경");
				}	
			}
		}

	}

	void rotate_image(){
		int i, j;

		for (i = 0; i < 4; i++) {
			for (j = 0; j < 4; j++) {
				GameObject temp = GameObject.Find (i.ToString() + "," + j.ToString ());

				if (temp) {
					temp.GetComponent<view_image> ().x = 3 - j;
					temp.GetComponent<view_image> ().y = i;
					temp.GetComponent<view_image> ().moved = 0;
					Obj_List.Add (temp);
					//	Debug.Log (i.ToString () + "," + j.ToString () + "의 x,y값 변경");
				} 

			}
		}

		for (i = 0; i < 4; i++) {
			for (j = 0; j < 4; j++) {
				GameObject temp2 = GameObject.Find ("d"+i.ToString() + "," + j.ToString ());
				if (temp2) {
					temp2.GetComponent<view_image> ().x = 3 - j;
					temp2.GetComponent<view_image> ().y = i;
					temp2.GetComponent<view_image> ().moved = 0;
					Obj_List2.Add (temp2);
					//	Debug.Log (i.ToString () + "," + j.ToString () + "의 x,y값 변경");
				} 
			}
		}

		//	Debug.Log ("obj_list의 크기는 : " + Obj_List.Count);

		for(i=0;i<Obj_List.Count;i++){
			//		temp_image.rectTransform.anchoredPosition = new Vector3 (Obj_List[i].GetComponent<view_image>().x*127-190, -Obj_List[i].GetComponent<view_image>().y*127-430, 0);
			Obj_List[i].name = Obj_List[i].GetComponent<view_image>().x.ToString() + "," + Obj_List[i].GetComponent<view_image>().y.ToString();

		}
		for(i=0;i<Obj_List2.Count;i++){
			//		temp_image.rectTransform.anchoredPosition = new Vector3 (Obj_List[i].GetComponent<view_image>().x*127-190, -Obj_List[i].GetComponent<view_image>().y*127-430, 0);
			Obj_List2[i].name = "d" + Obj_List2[i].GetComponent<view_image>().x.ToString() + "," + Obj_List2[i].GetComponent<view_image>().y.ToString();

		}

	}


	void save_data(){
		if(score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("high_score", highscore);
		} 
		PlayerPrefs.Save ();
	}


	void LateUpdate(){
		save_data ();

			if (Input.GetKeyDown (KeyCode.Backspace) && pause_check ==1 || Input.GetKeyDown (KeyCode.Escape) && pause_check == 1) {
				button_pause_exit ();
			} else if (Input.GetKeyDown (KeyCode.Backspace) && pause_check ==0 || Input.GetKeyDown (KeyCode.Escape) && pause_check == 0) {
				button_pause ();
			}


		if (Input.GetKeyDown (KeyCode.R)) {
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
		} else if (check_rotate == 4) {
			if (check_change (checkmap, gridmap) > 0) {
				sound_flag = PlayerPrefs.GetInt("sound_flag"); 
				if (sound_flag == 1) {
					swipe_sound.GetComponent<AudioSource> ().Play ();
				}
				Spawn ();
				set_target ();
			} 

			if (check_empty (gridmap) != 0 && check_merge (gridmap) != 0 && check_merge2 (gridmap) != 0) {
				check_value++;
			}
		}

		else if (check_rotate == 1) {
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			if (check_change(checkmap,gridmap) > 0) {
				sound_flag = PlayerPrefs.GetInt("sound_flag"); 
				if (sound_flag == 1) {
					swipe_sound.GetComponent<AudioSource> ().Play ();
				}
				Spawn ();
				set_target ();
			} 

			if (check_empty (gridmap) != 0 && check_merge (gridmap) != 0 && check_merge2(gridmap) != 0) {
				check_value++;
			}
		}
		else if (check_rotate == 2) {
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			if (check_change(checkmap,gridmap) > 0) {
				sound_flag = PlayerPrefs.GetInt("sound_flag"); 
				if (sound_flag == 1) {
					swipe_sound.GetComponent<AudioSource> ().Play ();
				}
				Spawn ();
				set_target ();
			} 

			if (check_empty (gridmap) != 0 && check_merge (gridmap) != 0 && check_merge2(gridmap) != 0) {
				check_value++;
			}

		}
		else if (check_rotate == 3) {
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			rotateBoard (gridmap);
			rotate_image ();
			Obj_List.Clear ();
			Obj_List2.Clear ();
			if (check_change(checkmap,gridmap) > 0) {
				sound_flag = PlayerPrefs.GetInt("sound_flag"); 
				if (sound_flag == 1) {
					swipe_sound.GetComponent<AudioSource> ().Play ();
				}
				Spawn ();
				set_target ();
			} 

			if (check_empty (gridmap) != 0 && check_merge (gridmap) != 0 && check_merge2(gridmap) != 0) {
				check_value++;
			}

		}

		if (clear2048 == 1) {
			panel.SetActive (true);
			clearui.SetActive (true);
			panel.transform.SetAsLastSibling ();
			clearui.transform.SetAsLastSibling ();
			sound_flag = PlayerPrefs.GetInt("sound_flag"); 
			if (sound_flag == 1) {
				clear_sound.GetComponent<AudioSource> ().Play ();
			}
			clear2048 = 2;
		}
	}
	void timeover(){
		if (swipe_timer == 1) {
			Debug.Log ("timeover");
			swipe_timer = 0;
		}

	}

	void Update () {

		check_rotate = 0;
		//	rotateBoard (gridmap);
		if (check_value == 0) {
			if (keydown_errorcheck == 0 && clear_check ==0 && pause_check ==0) {

				//swipe start

				if (Input.GetMouseButtonDown (0)) {
					firstposition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					swipe_timer = 1;
					//Invoke ("timeover", 4.0f);
				} else if (Input.GetMouseButtonUp (0)) {
					secondposition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					currentswipe = new Vector2 (secondposition.x - firstposition.x, secondposition.y - firstposition.y);
					if (Mathf.Abs (currentswipe.x) > 15 || Mathf.Abs (currentswipe.y) > 15) {
						currentswipe.Normalize ();
						if (swipe_timer == 1) {
							if (currentswipe.y > 0 && currentswipe.x > -0.5f && currentswipe.x < 0.5f) {
							//	Debug.Log ("up swipe");
								swipe_timer = 0;
								for (int x = 0; x < 4; x++) {
									for (int y = 0; y < 4; y++) {
										checkmap [x] [y] = gridmap [x] [y];
									}
								}
								moveup ();
								check_rotate = 4;
							}
							//swipe down
							if (currentswipe.y < 0 && currentswipe.x > -0.5f && currentswipe.x < 0.5f) {
							//	Debug.Log ("down swipe");
								swipe_timer = 0;
								for (int x = 0; x < 4; x++) {
									for (int y = 0; y < 4; y++) {
										checkmap [x] [y] = gridmap [x] [y];
									}
								}
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();

								moveup ();
								check_rotate = 2;
							}
							//swipe left
							if (currentswipe.x < 0 && currentswipe.y > -0.5f && currentswipe.y < 0.5f) {
							//	Debug.Log ("left swipe");
								swipe_timer = 0;
								for (int x = 0; x < 4; x++) {
									for (int y = 0; y < 4; y++) {
										checkmap [x] [y] = gridmap [x] [y];
									}
								}
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();
								moveup ();
								check_rotate = 3;
							}
							//swipe right
							if (currentswipe.x > 0 && currentswipe.y > -0.5f && currentswipe.y < 0.5f) {
							//	Debug.Log ("right swipe");
								swipe_timer = 0;
								for (int x = 0; x < 4; x++) {
									for (int y = 0; y < 4; y++) {
										checkmap [x] [y] = gridmap [x] [y];
									}
								}
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();
								rotateBoard (gridmap);
								rotate_image ();
								Obj_List.Clear ();
								Obj_List2.Clear ();
								moveup ();
								check_rotate = 1;
							}
						} 
					}
				}


				//swipe end


				if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.KeypadEnter)) {

					for (int x = 0; x < 4; x++) {
						for (int y = 0; y < 4; y++) {
							checkmap [x] [y] = gridmap [x] [y];
						}
					}
					moveup ();
					check_rotate = 4;


				} else if (Input.GetKeyDown (KeyCode.R)) {
					for (int x = 0; x < 4; x++) {
						for (int y = 0; y < 4; y++) {
							checkmap [x] [y] = gridmap [x] [y];
						}
					}

					//Application.OpenURL("http://twitter.com/intent/tweet?text=hello+world&url=http://www.google.com");


					if (check_change (checkmap, gridmap) > 0) {

					} 

					if (check_empty (gridmap) != 0 && check_merge (gridmap) != 0 && check_merge2 (gridmap) != 0) {
						check_value++;
					}

				} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
					for (int x = 0; x < 4; x++) {
						for (int y = 0; y < 4; y++) {
							checkmap [x] [y] = gridmap [x] [y];
						}
					}
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();

					moveup ();
					check_rotate = 2;




				} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					for (int x = 0; x < 4; x++) {
						for (int y = 0; y < 4; y++) {
							checkmap [x] [y] = gridmap [x] [y];
						}
					}
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();
					moveup ();
					check_rotate = 3;

				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
					for (int x = 0; x < 4; x++) {
						for (int y = 0; y < 4; y++) {
							checkmap [x] [y] = gridmap [x] [y];
						}
					}
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();
					rotateBoard (gridmap);
					rotate_image ();
					Obj_List.Clear ();
					Obj_List2.Clear ();
					moveup ();
					check_rotate = 1;


				}

			}
		} 



		else {
			if (resetkey == 0) {
				save_data ();
				Debug.Log ("game over!!");
				sound_flag = PlayerPrefs.GetInt("sound_flag"); 
				if (sound_flag == 1) {
					gameover_sound.GetComponent<AudioSource> ().Play ();
				}
				panel.SetActive (true);
				gameoverui.SetActive (true);
				panel.transform.SetAsLastSibling ();
				gameoverui.transform.SetAsLastSibling ();
				resetkey = 1;
				gameover_check = 1;
			}

		}
	}


	void rotateBoard(int[][] gridmap) { 
		int i,j,n=4; 
		int tmp;
		for (i=0; i<n/2; i++) { 
			for (j=i; j<n-i-1; j++) { 

				tmp = gridmap [i] [j];				
				gridmap[i][j] = gridmap[j][n-i-1]; 
				gridmap[j][n-i-1] = gridmap[n-i-1][n-j-1]; 
				gridmap[n-i-1][n-j-1] = gridmap[n-j-1][i]; 
				gridmap[n-j-1][i] = tmp; 

			} 
		} 

	} 




	int check_empty(int[][] gridmap){
		int x, y;
		int count = 0;
		for (x = 0; x < 4; x++) {
			for (y = 0; y < 4; y++) {
				if (gridmap [x] [y] == 0) {
					count++;
				}
			}
		}
		if (count > 0) {
			return 0;
		} else{
			return 1;
		}
	}


	int check_merge(int[][] gridmap){
		int x, y;
		int count = 0;
		for (x = 0; x < 4; x++) {
			for (y = 0; y < 3; y++) {
				if (gridmap [x] [y] == gridmap[x][y+1]) {
					count++;
				}
			}
		}
		if (count > 0) {
			return 0;
		} else{
			return 1;
		}
	}

	int check_merge2(int[][] gridmap){
		rotateBoard (gridmap);
		int x, y;
		int count = 0;
		for (x = 0; x < 4; x++) {
			for (y = 0; y < 3; y++) {
				if (gridmap [x] [y] == gridmap[x][y+1]) {
					count++;
				}
			}
		}
		rotateBoard (gridmap);
		rotateBoard (gridmap);
		rotateBoard (gridmap);
		if (count > 0) {
			return 0;
		} else{
			return 1;
		}
	}







	void moveup() { 
		int x; 

		for (x = 0; x < 4; x++) { 
			slide (x);
		}
	} 



	void slide(int foranime) { 

		int x,t,stop=0;
		for (x=0;x<4;x++) { 

			GameObject aaa = GameObject.Find (foranime.ToString() + "," + x.ToString ());

			if (GameManager.gridmap[foranime][x]!=0) { 
				t = findTarget(foranime,x,stop); 
				// if target is not original position, then move or merge 
				if (t!=x) { 
					// if target is zero, this is a move 
					if (GameManager.gridmap[foranime][t]==0) { 
						GameManager.gridmap[foranime][t]=GameManager.gridmap[foranime][x];
						if (aaa) {

							//		aaaimage.rectTransform.anchoredPosition = new Vector3 (foranime * 127-190, -t * 127-430, 0);
							aaa.name = foranime.ToString () + "," + t.ToString ();
							aaa.GetComponent<view_image> ().x = foranime;
							aaa.GetComponent<view_image> ().y = t;

						}

					} else if (aaa && GameManager.gridmap[foranime][t]==GameManager.gridmap[foranime][x]) { 

						GameManager.gridmap[foranime][t] = GameManager.gridmap[foranime][t]*2; 
						if (GameManager.gridmap [foranime] [t] == 2048 && clear2048 == 0) {
							clear_check = 1;
							clear2048 = 1;
						}
						aaa.name = foranime.ToString () + "," + t.ToString ();
						aaa.GetComponent<view_image> ().x = foranime;
						aaa.GetComponent<view_image> ().y = t;
						//	aaaimage.rectTransform.anchoredPosition = new Vector3 (foranime*127-190, -t*127-430, 0);

						GameObject ttt = GameObject.Find (foranime.ToString() + "," + t.ToString ());
						ttt.name = "d" + foranime.ToString () + "," + t.ToString ();
						ttt.GetComponent<view_image> ().x = foranime;
						ttt.GetComponent<view_image> ().y = t;
						ttt.GetComponent<view_image> ().combination = 1;
						//	Destroy (ttt);
						//	aaa.GetComponent<view_image> ().x = t;
						//	aaa.GetComponent<view_image> ().y = Mathf.Abs(3-foranime);

						// increase score 
						score+=gridmap[foranime][t]; 
						// set stop to avoid double merge 
						stop = t+1; 
					} 
					GameManager.gridmap[foranime][x]=0; 

				} 

			} 
		} 

	} 



	void Spawn(){
		int rand_x = 0;
		int rand_y = 0;
		int oc = 0;
		while (oc == 0 && check_empty(gridmap) == 0) {
			rand_x = Random.Range (0, 4);
			rand_y = Random.Range (0, 4);
			if (gridmap [rand_x] [rand_y] == 0) {
				gridmap [rand_x] [rand_y] = 2;

				oc = 1;

				GameObject a = Instantiate (GameObject.Find ("box")) as GameObject;
				a.transform.SetParent (canvas.transform);
				//a.name = "123";
				var aimage = a.GetComponent<Image> ();
				aimage.rectTransform.sizeDelta = new Vector2 (140, 140);
				if (start_spawn == 1) {
					aimage.rectTransform.localScale = new Vector3 (1, 1, 1);
				}
				else if(start_spawn == 0){
					aimage.rectTransform.localScale = new Vector3 (0.85f, 0.85f, 1);
				}

				aimage.rectTransform.anchoredPosition = new Vector3 (rand_x*127.5f-191, -rand_y*127.5f-428, 0);
				a.name = rand_x.ToString() + "," + rand_y.ToString();
				a.GetComponent<view_image> ().x = rand_x;
				a.GetComponent<view_image> ().y = rand_y;
				a.GetComponent<view_image> ().original_x = rand_x;
				a.GetComponent<view_image> ().original_y = rand_y;
				a.GetComponent<view_image> ().target_x = rand_x;
				a.GetComponent<view_image> ().target_y = rand_y;

			} 
		}

	}



	int findTarget(int foranime, int x, int stop) { 
		int t; 

		// if the position is already on the first, don't evaluate 
		if (x==0) { 
			return x; 
		} 
		for(t=x-1;t>=0;t--) { 
			if (GameManager.gridmap[foranime][t]!=0) { 
				if (GameManager.gridmap[foranime][t]!=GameManager.gridmap[foranime][x]) { 
					// merge is not possible, take next position 
					return t+1; 
				} 
				return t; 
			} else { 
				// we should not slide further, return this one 
				if (t==stop) { 
					return t; 
				} 
			} 
		} 
		// we did not find a 
		return x; 
	} 
}

/*

				if (aaa != null) {
					//Debug.Log (aaa.name);
					var aimage = aaa.GetComponent<Image> ();
					aaa.name = (n - j - 1).ToString () + "," + i.ToString ();
					aimage.rectTransform.anchoredPosition = new Vector3 ((n-j-1)*100-150, -i*100+200, 0);
					aaa.GetComponent<view_image> ().x = n - j - 1;
					aaa.GetComponent<view_image> ().y = i;
					//Debug.Log ("aaa roatate!");
					GameObject aaac = GameObject.Find (i.ToString() + "," + j.ToString ());
					if (aaac != null && aaa.GetComponent<view_image>().combination == 1) {
						var acimage = aaac.GetComponent<Image> ();
						aaac.name = (n - j - 1).ToString () + "," + i.ToString ();
						acimage.rectTransform.anchoredPosition = new Vector3 ((n - j - 1) * 100 - 150, -i * 100 + 200, 0);
						aaac.GetComponent<view_image> ().x = n - j - 1;
						aaac.GetComponent<view_image> ().y = i;
						aaac.GetComponent<view_image> ().combination = 0;
						Debug.Log ("aaa_c roatate!");
					}
				}
				else
				{
				//	Debug.Log ("aaa.name error");
				}
				if (aab != null) {
					//Debug.Log (aab.name);
					var bimage = aab.GetComponent<Image> ();
					aab.name = (n - i - 1).ToString () + "," + (n - j - 1).ToString ();
					bimage.rectTransform.anchoredPosition = new Vector3 ((n-i-1)*100-150, -(n-j-1)*100+200, 0);
					aab.GetComponent<view_image> ().x = n - i - 1;
					aab.GetComponent<view_image> ().y = n - j - 1;
					//Debug.Log ("aab roatate!");
					GameObject aabc = GameObject.Find ((n - j - 1).ToString () + "," + i.ToString ());
					if (aabc != null&& aab.GetComponent<view_image>().combination == 1) {
						var bcimage = aabc.GetComponent<Image> ();
						aabc.name = (n - i - 1).ToString () + "," + (n - j - 1).ToString ();
						bcimage.rectTransform.anchoredPosition = new Vector3 ((n-i-1)*100-150, -(n-j-1)*100+200, 0);
						aabc.GetComponent<view_image> ().x = n - i - 1;
						aabc.GetComponent<view_image> ().y = n - j - 1;
						aabc.GetComponent<view_image> ().combination = 0;
						Debug.Log ("aab_c roatate!");
					}
				}
				else
				{
				//	Debug.Log ("aab.name error");
				}
				if (aac != null) {
					//Debug.Log (aac.name);
					var cimage = aac.GetComponent<Image> ();
					aac.name = j.ToString () + "," + (n - i - 1).ToString ();
					cimage.rectTransform.anchoredPosition = new Vector3 (j*100-150, -(n-i-1)*100+200, 0);
					aac.GetComponent<view_image> ().x = j;
					aac.GetComponent<view_image> ().y = n - i - 1;
					//Debug.Log ("aac roatate!");
					GameObject aacc = GameObject.Find ((n - i - 1).ToString () + "," + (n - j - 1).ToString ());
					if (aacc != null&& aac.GetComponent<view_image>().combination == 1) {
						var ccimage = aacc.GetComponent<Image> ();
						aacc.name = j.ToString () + "," + (n - i - 1).ToString ();
						ccimage.rectTransform.anchoredPosition = new Vector3 (j*100-150, -(n-i-1)*100+200, 0);
						aacc.GetComponent<view_image> ().x = j;
						aacc.GetComponent<view_image> ().y = n - i - 1;
						aacc.GetComponent<view_image> ().combination = 0;
						Debug.Log ("aac_c roatate!");
					}
				}
				else
				{
				//	Debug.Log ("aac.name error");
				}
				if (aad != null) {
					//Debug.Log (aad.name);
					var dimage = aad.GetComponent<Image> ();
					aad.name = i.ToString () + "," + j.ToString ();
					dimage.rectTransform.anchoredPosition = new Vector3 (i*100-150, -j*100+200, 0);
					aad.GetComponent<view_image> ().x = i;
					aad.GetComponent<view_image> ().y = j;
					//Debug.Log ("aad roatate!");
					GameObject aadc = GameObject.Find (j.ToString () + "," + (n - i - 1).ToString ());
					if (aadc != null&& aad.GetComponent<view_image>().combination == 1) {
						var dcimage = aadc.GetComponent<Image> ();
						aadc.name = i.ToString () + "," + j.ToString ();
						dcimage.rectTransform.anchoredPosition = new Vector3 (i*100-150, -j*100+200, 0);
						aadc.GetComponent<view_image> ().x = i;
						aadc.GetComponent<view_image> ().y = j;
						aadc.GetComponent<view_image> ().combination = 0;
						Debug.Log ("aad_c roatate!");
					}
				}
				else
				{
				//	Debug.Log ("aad.name error");
				}

*/


