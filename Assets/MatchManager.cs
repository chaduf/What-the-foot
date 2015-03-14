using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchManager : MonoBehaviour {
	public enum RULES{
		SCORE,
		TIME,
		INFINITE
	}

	public RULES rules;

	public int maxScore;
	public float maxChrono;

	public int scoreTeam1;
	public int scoreTeam2;
	public float chrono;

	public GameObject GameManager;
	public GameObject[] scoreTeam1Texts;
	public GameObject[] scoreTeam2Texts;
	public GameObject[] chronoTexts;

	public void InitMatch(){
		scoreTeam1 = 0;
		scoreTeam2 = 0;
		rules = RULES.INFINITE;
		foreach(GameObject chronoText in chronoTexts){
			chronoText.GetComponent<Text>().enabled = false;
		}
	}

	public void InitMatch(int score){
		InitMatch ();

		maxScore = score;
		rules = RULES.SCORE;
	}

	public void InitMatch(float time){
		InitMatch ();

		chrono = 0; 
		maxChrono = time;
		rules = RULES.TIME;

		foreach(GameObject chronoText in chronoTexts){
			chronoText.GetComponent<Text>().enabled = true;
		}
	}

	public void endMatch(int winner){
		GameManager.GetComponent<GameManager> ().EnterEnd (winner);
	}

	public void ScorePoint (int team){
		switch (team) {
		case 1:
			scoreTeam1++;
			break;
		case 2:
			scoreTeam2++;
			break;
		}

		if (rules == RULES.SCORE){
			if (scoreTeam1 >= maxScore){
				endMatch(1);
			}
			else if (scoreTeam2 >= maxScore){
				endMatch(1);
			}
		}
	}

	public void CheckChrono(){
		chrono = Mathf.Min (chrono + Time.deltaTime, maxChrono);

		if (chrono >= maxChrono) {
			if (scoreTeam1 > scoreTeam2){
				endMatch(1);
			}
			else if (scoreTeam2 > scoreTeam1){
				endMatch(2);
			}
			else{
				InitMatch(1);
			}
		}
	}

	void DisplayScore(){
		foreach (GameObject scoreTeam1Text in scoreTeam1Texts) {
			scoreTeam1Text.GetComponent<Text>().text = scoreTeam1.ToString();
		}
		foreach (GameObject scoreTeam2Text in scoreTeam2Texts) {
			scoreTeam2Text.GetComponent<Text>().text = scoreTeam2.ToString();
		}
	}

	void DisplayTime(){
		int minutes = ((int)chrono) / 60;
		int seconds = ((int)chrono) % 60;

		foreach (GameObject chronoText in chronoTexts) {
			chronoText.GetComponent<Text>().text = minutes.ToString() + " : " + seconds.ToString();
		}
	}

	// Update functions;
	void TimeUpdate(){
		CheckChrono ();
		DisplayTime ();
		DisplayScore();
	}

	void ScoreUpdate(){
		DisplayScore();
	}

	void InfiniteUpdate(){
		DisplayScore();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (rules) {
		case RULES.TIME:
			TimeUpdate ();
			break;
		case RULES.SCORE:
			ScoreUpdate ();
			break;
		case RULES.INFINITE:
			InfiniteUpdate ();
			break;
		}
	}
}
