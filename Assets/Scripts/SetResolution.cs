using UnityEngine;
using System.Collections;

public class SetResolution : MonoBehaviour {

    public enum GameMode { Landscape, Portrait };

    private static float GAME_WIDTH = 1920;
    private static float GAME_HEIGHT = 1080;

    public GameMode gameMode = GameMode.Landscape;
    public bool useStreatch = false;

    void Start () {
        if (gameMode == GameMode.Landscape)
        {
            if (useStreatch)
            {
                // Scale the game object to fit the screen
                float newHeightScale = (float)(GAME_WIDTH * Screen.height) / (float)(GAME_HEIGHT * Screen.width);
                gameObject.transform.localScale = new Vector3(1, newHeightScale, 0);
            }

            // Adjust the camera to the screen size
            Camera.main.orthographicSize = (GAME_WIDTH / 200f) / Camera.main.aspect;
        }
        else
        {
            if (useStreatch)
            {
                // Scale the game object to fit the screen
                float newHeightScale = (float)(GAME_WIDTH * Screen.width) / (float)(GAME_HEIGHT * Screen.height);
                gameObject.transform.localScale = new Vector3(newHeightScale, 1, 0);
            }

            // Adjust the camera to the screen size
            Camera.main.orthographicSize = (GAME_WIDTH / 200f);
        }
    }
}
