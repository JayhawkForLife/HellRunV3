using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Texture backgroundTexture;

    public GUISkin startSkin;

    void OnGUI()
    {
        // Display the main menu background
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        GUI.skin = startSkin;
        if (GUI.Button(new Rect(Screen.width/2-150, Screen.height * .4f, Screen.width * .25f, Screen.height * .2f), "Play Again?"))
        {
            Application.LoadLevel("HellMaze");
        }
        if (GUI.Button(new Rect(Screen.width/2-150, Screen.height * .7f, Screen.width * .20f, Screen.height * .2f), "QUIT"))
        {
            Application.Quit();
        }
    }
}
