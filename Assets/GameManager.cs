using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public enum STATE{
		PAUSE,
		MENU,
		MATCH,
		END
	};

	public Canvas pauseCanvas;
	public Canvas menuCanvas;
	public Canvas matchCanvas;
	public Canvas endCanvas;

	public GameObject matchManager;

	public STATE state;

	public int maxScore = 5;
	public float maxChrono = 90.0f;

	private int winner;

	// State Change functions
	public void EnterMenu(){
		switch (state){
		case STATE.PAUSE:
			OutPause();
			break;
		case STATE.END:
			OutEnd();
			break;
		default:
			return;
		}
		state = STATE.MENU;
		menuCanvas.enabled = true;
	}

	public void EnterPause(){
		switch (state) {
		case STATE.MATCH:
			Time.timeScale = 0.0f;
			OutMatch();
			break;
		default:
			return;
		}
		state = STATE.PAUSE;
		pauseCanvas.enabled = true;
	}

	public void EnterMatch(){
		switch (state) {
		case STATE.PAUSE:
			OutPause();
			break;
		default:
			return;
		}
		state = STATE.MATCH;
		matchCanvas.enabled = true;
	}

	public void EnterMatch(MatchManager.RULES rules){
		switch (state) {
		case STATE.MENU:
			OutMenu();
			break;
		default:
			return;
		}
		state = STATE.MATCH;

		switch (rules) {
		case MatchManager.RULES.SCORE:
			matchManager.GetComponent<MatchManager> ().InitMatch (maxScore);
			break;
		case MatchManager.RULES.TIME:
			matchManager.GetComponent<MatchManager> ().InitMatch (maxChrono);
			break;
		case MatchManager.RULES.INFINITE:
			matchManager.GetComponent<MatchManager> ().InitMatch ();
			break;
		}

		matchCanvas.enabled = true;
	}

	public void EnterEnd(int winnerTeam){
		switch (state) {
		case STATE.MATCH:
			OutMatch ();
			winner = winnerTeam;
			break;
		default:
			return;
		}
		state = STATE.END;
		endCanvas.enabled = true;
	}

	void OutMenu (){
		menuCanvas.enabled = false;
	}

	void OutPause(){
		Time.timeScale = 1.0f;
		pauseCanvas.enabled = false;
	}

	void OutMatch(){
		matchCanvas.enabled = false;
	}

	void OutEnd(){
		endCanvas.enabled = false;
	}

	//Buttons functions	
	public void OnScoreModeClick(){
		EnterMatch (MatchManager.RULES.SCORE);
	}

	public void OnTimeModeClick(){
		EnterMatch (MatchManager.RULES.TIME);
	}

	public void OnInfiniteModeClick (){
		EnterMatch (MatchManager.RULES.INFINITE);
	}

	public void OnResumeClick(){
		EnterMatch ();
	}

	public void OnEchapClick(){
		EnterMenu ();
	}

	//Update functions

	void MatchUpdate(){

	}

	// à chager pour un tap sur l'écran tactile
	void EndUpdate(){
		if (Input.GetMouseButtonDown(0)) {
			EnterMenu();
		}
	}

	// Use this for initialization
	void Start () {
		state = STATE.MENU;
		pauseCanvas.enabled = false;
		matchCanvas.enabled = false;
		endCanvas.enabled = false;
		menuCanvas.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state){
		case STATE.END:
			EndUpdate ();
			break;
		}
	}
}
