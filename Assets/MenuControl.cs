using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuControl : MonoBehaviour {
	public Canvas startMenu;
	public Canvas gameoverMenu;

	// Use this for initialization
	void Start () {
		startMenu = startMenu.GetComponent<Canvas> ();
		gameoverMenu = gameoverMenu.GetComponent<Canvas> ();
		startMenu.enabled = true;
		gameoverMenu.enabled = false;
	}

	public void StartPressed () {
		startMenu.enabled = false;
	}

	public void RestartPressed() {
		gameoverMenu.enabled = false;
	}

	public void ShowGameOverMenu() {
		gameoverMenu.enabled = true;
	}
}
