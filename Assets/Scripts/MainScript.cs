using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    // UI
    private Dropdown mGameBKDropdown;
    private Dropdown mDeviceDropdown;
    private Dropdown mOrientationDropdown;
    private Toggle mFitToFrame;

    // Game objects
    private GameObject mDemoGame;
    private SpriteRenderer mDemoGameSpriteRenderer;
    private GameObject mDeviceFrame;
    private GameObject mJumpingBall;
    private GameObject mButtomBar;

    // Local variables 
    private PhoneDetails mCurrentDevice;

    private List<BackgroundPicture> mLandscapeBackgroundsList = new List<BackgroundPicture>
    {
        new BackgroundPicture() { isLandscape = true, dislayName = "1920x1080 (16:9)", resourceName = "bk/landsscape_1920x1080_bk", ratio = (16f/9f) },
        new BackgroundPicture() { isLandscape = true, dislayName = "1024x600 (17:10)", resourceName = "bk/landsscape_1024x600_bk", ratio = (17f/10f) },
        new BackgroundPicture() { isLandscape = true, dislayName = "1280x800 (16:10)", resourceName = "bk/landsscape_1280x800_bk", ratio = (16f/10f) },
        new BackgroundPicture() { isLandscape = true, dislayName = "960x640 (3:2)", resourceName = "bk/landsscape_960x640_bk", ratio = (3f/2f) },
        new BackgroundPicture() { isLandscape = true, dislayName = "1024x768 (4:3)", resourceName = "bk/landsscape_1024x768_bk" , ratio = (4f/3f) }
    };

    private List<BackgroundPicture> mPortraitBackgroundsList = new List<BackgroundPicture>
    {
        new BackgroundPicture() { isLandscape = false, dislayName = "1080x1920 (9:16)", resourceName = "bk/portrait_1080x1920_bk", ratio = (9f/16f) },
        new BackgroundPicture() { isLandscape = false, dislayName = "600x1024 (10:17)", resourceName = "bk/portrait_600x1024_bk", ratio = (10f/17f) },
        new BackgroundPicture() { isLandscape = false, dislayName = "1800x1280 (10:16)", resourceName = "bk/portrait_800x1280_bk", ratio = (10f/16f) },
        new BackgroundPicture() { isLandscape = false, dislayName = "640x960 (2:3)", resourceName = "bk/portrait_640x960_bk", ratio = (2f/3f) },
        new BackgroundPicture() { isLandscape = false, dislayName = "768x1024 (3:4)", resourceName = "bk/portrait_768x1024_bk", ratio = (3f/4f) }
    };

    private List<PhoneDetails> mPhoneDetailsList = new List<PhoneDetails>
    {
        new PhoneDetails() { phoneName = "Galaxy Note 5 (2560x1440, 16:9)", resourceName = "phone_frame_galaxy_note_5", xOffset = 39, yOffset = 8, width = 349, height = 197 },
        new PhoneDetails() { phoneName = "Nexus 5X (1920x1080, 16:10)", resourceName = "phone_frame_nexus5x", xOffset = 46, yOffset = 12, width = 378, height = 213 },
        new PhoneDetails() { phoneName = "Nexus 4 (1280x768, 16:10)", resourceName = "phone_frame_nexus4", xOffset = 72, yOffset = 17, width = 473, height = 284 },
        new PhoneDetails() { phoneName = "iPad Mini 5 (1024x768, 4:3)", resourceName = "phone_frame_ipad_mini", xOffset = 49, yOffset = 17, width = 383, height = 288 },
        new PhoneDetails() { phoneName = "iPhone 5S (1136x640, 16:9)", resourceName = "phone_frame_iphone_5s", xOffset = 82, yOffset = 22, width = 425, height = 242 },
        new PhoneDetails() { phoneName = "iPhone 6S Plus (1920x1080, 16:9)", resourceName = "phone_frame_iphone_6s", xOffset = 59, yOffset = 15, width = 353, height = 200 },
        new PhoneDetails() { phoneName = "iPad AIR 2 (2048x1536, 4:3)", resourceName = "phone_frame_ipad_air2", xOffset = 50, yOffset = 25, width = 473, height = 355 }
    };

    void Start()
    {
        //
        // Get references to all objects
        //
        mDemoGame = GameObject.Find("DemoGame");
        mDemoGameSpriteRenderer = mDemoGame.GetComponent<SpriteRenderer>();
        mDeviceFrame = GameObject.Find("DeviceFrame");
        mJumpingBall = GameObject.Find("JumpingBall");
        mFitToFrame = GameObject.Find("FitToFrame").GetComponent<Toggle>();
        mButtomBar = GameObject.Find("ButtomBar");
        mGameBKDropdown = GameObject.Find("GameBKDropdown").GetComponent<Dropdown>();
        mOrientationDropdown = GameObject.Find("OrientationDropdown").GetComponent<Dropdown>();
        mDeviceDropdown = GameObject.Find("DeviceDropdown").GetComponent<Dropdown>();

        //
        // Set the devices information
        //
        mDeviceDropdown.ClearOptions();

        List<string> devicesList = new List<string>();

        for (int index = 0; index < mPhoneDetailsList.Count; index++)
        {
            devicesList.Add(mPhoneDetailsList[index].phoneName);
        }

        mDeviceDropdown.AddOptions(devicesList);

        //
        // Initialize values
        //

        // Fake set device
        OnDeviceValueChnaged(0);

        // Fake set orientation change
        OnOrientationValueChange(0);

        // Fake set game background
        OnBackgroundImageValueChange(0);
    }

    public void OnFitToGameValueChange(bool isOn)
    {
        FitGameToFrame();
    }

    public void OnBackgroundImageValueChange(int newValue)
    {
        bool isInLandscape = (mOrientationDropdown.value == 0);
        string resourceName;

        if (isInLandscape)
        {
            resourceName = mLandscapeBackgroundsList[mGameBKDropdown.value].resourceName;
        }
        else
        {
            resourceName = mPortraitBackgroundsList[mGameBKDropdown.value].resourceName;
        }

        // Set the resource
        mDemoGameSpriteRenderer.sprite = Resources.Load<Sprite>(resourceName);

        FitGameToFrame();
    }

    public void OnDeviceValueChnaged(int newVaule)
    {
        // Get device info
        mCurrentDevice = mPhoneDetailsList[mDeviceDropdown.value];

        // Set the new picture
        mDeviceFrame.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("phones/" + mCurrentDevice.resourceName);

        FitGameToFrame();
    }

    public void OnOrientationValueChange(int newValue)
    {
        if (newValue == 0)
        {
            Debug.Log("Orientation changed to landscape");

            // Put frame on landscape
            mDeviceFrame.transform.localEulerAngles = new Vector3(0, 0, 0);

            LoadLandscapePicturesBK();
        }
        else
        {
            Debug.Log("Orientation changed to portrait");

            // Put frame on portrait
            mDeviceFrame.transform.localEulerAngles = new Vector3(0, 0, -90);

            LoadPortraitPicturesBK();
        }

        // Reset the picture;
        OnBackgroundImageValueChange(mGameBKDropdown.value);
    }

    private void LoadLandscapePicturesBK()
    {
        mGameBKDropdown.ClearOptions();

        List<string> optionList = new List<string>();

        for (int index = 0; index < mLandscapeBackgroundsList.Count; index++)
        {
            optionList.Add(mLandscapeBackgroundsList[index].dislayName);
        }

        mGameBKDropdown.AddOptions(optionList);
    }

    private void LoadPortraitPicturesBK()
    {
        mGameBKDropdown.ClearOptions();

        List<string> optionList = new List<string>();

        for (int index = 0; index < mPortraitBackgroundsList.Count; index++)
        {
            optionList.Add(mPortraitBackgroundsList[index].dislayName);
        }

        mGameBKDropdown.AddOptions(optionList);
    }

    private void FitGameToFrame()
    {
        bool isInLandscape = (mOrientationDropdown.value == 0);

        mDemoGame.transform.localScale = Vector2.one;

        float demoGameWidth = mDemoGameSpriteRenderer.bounds.size.x;
        float demoGameHeight = mDemoGameSpriteRenderer.bounds.size.y;

        float xScale;
        float yScale;

        if (mFitToFrame.isOn)
        {
            xScale = ((isInLandscape ? mCurrentDevice.width : mCurrentDevice.height) / 100f) / demoGameWidth;
            yScale = ((isInLandscape ? mCurrentDevice.height : mCurrentDevice.width) / 100f) / demoGameHeight;
        }
        else
        {
            if (isInLandscape)
            {
                xScale = (mCurrentDevice.width / 100f) / demoGameWidth;
                float widthSize = mDemoGameSpriteRenderer.bounds.size.x * xScale;
                float newHeightSize = widthSize / mLandscapeBackgroundsList[mGameBKDropdown.value].ratio;
                float currentHeightSize = mDemoGameSpriteRenderer.bounds.size.y;
                yScale = newHeightSize / currentHeightSize;
            }
            else
            {
                yScale = (mCurrentDevice.width / 100f) / demoGameHeight;
                float heightSize = mDemoGameSpriteRenderer.bounds.size.y * yScale;
                float newWidthSize = heightSize * mPortraitBackgroundsList[mGameBKDropdown.value].ratio;
                float currentWidthSize = mDemoGameSpriteRenderer.bounds.size.x;
                xScale = newWidthSize / currentWidthSize;
            }
        }

        // Place the bottom bar in place
        mButtomBar.transform.localPosition = new Vector2(0, -mDemoGameSpriteRenderer.bounds.size.y / 2);

        mDemoGame.transform.localScale = new Vector2(xScale, yScale);
   
        //
        // Fit the ball to the screen size
        //

        // Stop the ball physics
        mJumpingBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Put the ball in the center
        mJumpingBall.transform.localPosition = Vector2.zero;

        // Set the ball size relative to the screen width
        mJumpingBall.transform.localScale = Vector2.one;
        float jumpingBallWidth = mJumpingBall.GetComponent<SpriteRenderer>().bounds.size.x;
        float newDemoWidth = mDemoGameSpriteRenderer.bounds.size.x;
        float mJumpingBallScaleX = (newDemoWidth / 10f) / jumpingBallWidth;
        mJumpingBall.transform.localScale = new Vector2(mJumpingBallScaleX, mJumpingBallScaleX);
    }

    class BackgroundPicture
    {
        public bool isLandscape;
        public string dislayName;
        public string resourceName;
        public float ratio;
    }

    class PhoneDetails
    {
        public string phoneName;
        public string resourceName;
        public float xOffset;
        public float yOffset;
        public float width;
        public float height;
    }
}
