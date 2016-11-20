using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudController : MonoBehaviour {
    public PlayerControllerScript player;
    public Text ScoreText;
		
	// Update is called once per frame
	void Update () {
        this.ScoreText.text = "Score: " + player.score;
	}
}
