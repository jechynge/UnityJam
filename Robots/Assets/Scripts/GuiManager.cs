using UnityEngine;
using System.Collections;
using System.Collections.Generic; // This allows me to use lists. 
using UnityEngine.UI;  // This allows you to use any GUI specific UI elements

public class GuiManager : MonoBehaviour {
	
	public enum GameStateType {
		Gameplay,
		Dead,
		DeathScreen,
		Paused
	}
	

	public GameObject ScrPaused;
	public GameObject ScrGameplay;
	public GameObject ScrTitle;
	public GameObject ScrCredits;
	public GameObject ScrDeath;

	public AudioClip SfxButton;

	public Camera myCamera;
	
	//public List <Flammable> ArObjective;  //This list holds all the burnable objectives.
	
	public bool Paused;	// If this is true, the game is paused.

	
	public GameStateType GameState;
	private static GuiManager instance = null;
	public static GuiManager Instance {get {return instance; }}
	
	void Awake () {
		// Required for singleton
		instance = this;
	}
	
	// Use this for initialization
	void Start () {
		
		GameState = GameStateType.Paused;
		ScrTitle.SetActive (true);
		
	}
	
	// Update is called once per frame
	void Update () {

		

		
		if (Input.GetKeyDown (KeyCode.P) == true) {
			print ("p pressed");
			OnBtnPaused ();
		}
		
		
		
		
		// If the title screen is currently up
		if (ScrTitle.activeSelf == true) { 
			
			if (Input.GetKey(KeyCode.Return) || Input.GetButton("Fire1")) {
				OnBtnStart();
			}
		}
	}
	
		
	
	
	
	public void OnBtnPaused() {
		
		//If you are already paused, return to gameplay. 
		if (GameState == GameStateType.Paused) {
			GameState = GameStateType.Gameplay;
			ScrGameplay.SetActive (true);
			ScrPaused.SetActive (false);
		} else {
			if (GameState == GameStateType.Gameplay)
				GameState = GameStateType.Paused;
			ScrGameplay.SetActive (false);
			ScrPaused.SetActive (true);
		}
		
	}
	
	public void OnBtnStart() {
		
		GameState = GameStateType.Gameplay;
		
		
		ScrTitle.SetActive (false);
		ScrGameplay.SetActive (true);
		
	}
	
	public void OnBtnCredits() {
		
		ScrTitle.SetActive (false);
		ScrCredits.SetActive (true);
		
	}
	
	
	public void OnReturntoMainMenu() {
		
		Application.LoadLevel (0);
		
		
	}
	
	public void OnBtnSound () {
		GetComponent<AudioSource> ().PlayOneShot (SfxButton);
		
	}
	
}
