using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public GameObject player;
    public GameObject computer;
    private Text playerText=null;
    private Text computerText=null;

	// Use this for initialization
	void Start () {
        playerText = GameObject.FindGameObjectWithTag("HackerScore").GetComponent<Text>();
        computerText = GameObject.FindGameObjectWithTag("ComputerScore").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
        if (playerText != null && computerText != null)
        {
            playerText.text = string.Format("Hacker Pwnt\n{0}", player.GetComponent<Respawnable>().Deaths);
            computerText.text = string.Format("Computer Hacked\n{0}", computer.GetComponent<Respawnable>().Deaths);
        }
	}
}
