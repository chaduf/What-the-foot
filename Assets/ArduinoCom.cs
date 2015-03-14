using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoCom : MonoBehaviour {
	public string portName = "";
	public int baud = 9600;
	public float timeStep = 1.0f;
	SerialPort port;
	float time;

	private ArrayList cmdStack;

	public void SendCmd(string cmd){
		if (port.IsOpen)
			port.Write(cmd);
	}

	public void AddCmd(string cmd){
		cmdStack.Insert (0, cmd);
	}


	public void InitPort (){
		port = new SerialPort(portName, baud);
		port.Close();
		port.Open();
	}

	// Use this for initialization
	void Start () {
		InitPort ();
		time = 0.0f;
		cmdStack = new ArrayList ();
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (cmdStack.Count > 0) {
			if (time > timeStep)  {
				SendCmd((string)cmdStack[0]);
				cmdStack.RemoveAt(0);
				time = 0.0f;
			}
		}
	}

//	void OnGUI(){
//		if (GUI.Button(new Rect(10, 10, 100, 50), "direct")){
//			AddCmd(directRotationCmd);
//		}
//		if (GUI.Button(new Rect (10, 110, 100, 50), "clock")){
//			AddCmd(clockRotationCmd);
//		}
//
//		if (GUI.Button(new Rect (10, 210, 100, 50), "init port")){
//			InitPort();
//		}
//	}

	void OnApplcationQuit(){
		port.Close ();
	}
}
