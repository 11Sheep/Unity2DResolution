using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Wallpapers from: http://wallpapercave.com/cartoon-background

// Landscape
//
// 1024x768 - 4:3
// 1920x1080 - 16:9
// 960x640 - 3:2
// 1024x600 - 17:10
// 1280x800 - 16:10
//
// Portrait
// 
// 800x1280 - 10:16

public class MainScript : MonoBehaviour {

    private Dropdown mGameBKDropdown;
    private Dropdown mDeviceDropdown;
    private GameObject mDemoGame;
    private GameObject mTabletFrame;
    private GameObject mJumpingBall;

    private List<BackgroundPicture> mLandscapeBackgroundsList = new List<BackgroundPicture>
    {
        new BackgroundPicture() { isLandscape = true, dislayName = "960x640 (3:2)", resourceName = "landsscape_960x640_bk" },
        new BackgroundPicture() { isLandscape = true, dislayName = "1024x600 (17:10)", resourceName = "landsscape_1024x600_bk" },
        new BackgroundPicture() { isLandscape = true, dislayName = "1024x768 (4:3)", resourceName = "landsscape_1024x768_bk" },
        new BackgroundPicture() { isLandscape = true, dislayName = "1280x800 (16:10)", resourceName = "landsscape_1280x800_bk" },
        new BackgroundPicture() { isLandscape = true, dislayName = "1920x1080 (16:9)", resourceName = "landsscape_1920x1080_bk" }
    };

    private List<BackgroundPicture> mPortraitBackgroundsList = new List<BackgroundPicture>
    {
        new BackgroundPicture() { isLandscape = false, dislayName = "960x640 (2:3)", resourceName = "portrait_640x960_bk" },
        new BackgroundPicture() { isLandscape = false, dislayName = "600x1024 (10:17)", resourceName = "portrait_600x1024_bk" },
        new BackgroundPicture() { isLandscape = false, dislayName = "768x1024 (3:4)", resourceName = "portrait_768x1024_bk" },
        new BackgroundPicture() { isLandscape = false, dislayName = "1800x1280 (10:16)", resourceName = "portrait_800x1280_bk" },
        new BackgroundPicture() { isLandscape = false, dislayName = "1080x1920 (9:16)", resourceName = "portrait_1080x1920_bk" }
    };

    // x:113, y:35   w:762,h:584

    void Start () {
        mDemoGame = GameObject.Find("DemoGame");
        mTabletFrame = GameObject.Find("tablet_frame");
        mJumpingBall = GameObject.Find("JumpingBall");

        float demoGameWidth = mDemoGame.GetComponent<SpriteRenderer>().bounds.size.x;
        float demoGameHeight = mDemoGame.GetComponent<SpriteRenderer>().bounds.size.y;

        float frameX = mTabletFrame.GetComponent<SpriteRenderer>().bounds.size.x;
        float frameY = mTabletFrame.GetComponent<SpriteRenderer>().bounds.size.y;

        float xScale = (762f / 100f) / demoGameWidth;
        float yScale = (584f / 100f) / demoGameHeight;

        mDemoGame.transform.localScale = new Vector2(xScale, yScale);

        float newDemoWidth = mDemoGame.GetComponent<SpriteRenderer>().bounds.size.x;
        float newDemoHeight = mDemoGame.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log("framex: " + frameX + ", frameY: " + frameY);
        Debug.Log("demoGameWidth: " + demoGameWidth + ", demoGameHeight: " + demoGameHeight);
        Debug.Log("xScale: " + xScale + ", yScale: " + yScale);
        Debug.Log("newDemoWidth: " + newDemoWidth + ", newDemoHeight: " + newDemoHeight);

        float mJumpingBallWidth = mJumpingBall.GetComponent<SpriteRenderer>().bounds.size.x;
        float mJumpingBallHeight = mJumpingBall.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log("mJumpingBallWidth: " + mJumpingBallWidth + ", mJumpingBallHeight: " + mJumpingBallHeight);

        float mJumpingBallScaleX = (newDemoWidth / 10f) / mJumpingBallWidth;
        //float mJumpingBallScaleY = (newDemoHeight / 10f) / mJumpingBallHeight;

        mJumpingBall.transform.localScale = new Vector2(mJumpingBallScaleX, mJumpingBallScaleX);

        float mNewJumpingBallWidth = mJumpingBall.GetComponent<SpriteRenderer>().bounds.size.x;
        float mNewJumpingBallHeight = mJumpingBall.GetComponent<SpriteRenderer>().bounds.size.y;

        Debug.Log("mNewJumpingBallWidth: " + mNewJumpingBallWidth + ", mNewJumpingBallHeight: " + mNewJumpingBallHeight);

        mGameBKDropdown = GameObject.Find("GameBKDropdown").GetComponent<Dropdown>();

        mDeviceDropdown = GameObject.Find("DeviceDropdown").GetComponent<Dropdown>();
        mDeviceDropdown.ClearOptions();

        List<string> devicesList = new List<string>();
        devicesList.Add("Apple iPhone 6 750X1334");
        devicesList.Add("Apple iPhone 5 540X1136");

        mDeviceDropdown.AddOptions(devicesList);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnOrientationValueChange(int newValue)
    {
        if (newValue == 0)
        {
            Debug.Log("Orientation changed to landscape");


        }
        
    }

    private void LoadPortraitPicturesBK()
    {
        mGameBKDropdown.ClearOptions();

        //mLandscapeBackgroundsList = new List<string>();
        //mLandscapeBackgroundsList.Add("Apple iPhone 6 750X1334");
        //mLandscapeBackgroundsList.Add("Apple iPhone 5 540X1136");

 //       mDeviceDropdown.AddOptions(devicesList);
    }

    class BackgroundPicture
    {
        public bool isLandscape;
        public string dislayName;
        public string resourceName;
    }
}
