using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] public TMP_Text volumeTestValue = null;
    [SerializeField] public UnityEngine.UI.Slider volumeSlider = null;

    [Header("Levels To Load")]
    public string newGameLevel;
    private string levelToLoad;

    [Header("Resolutions Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Level Records")]
    public TMP_Text LevelOneRecordValue = null;
    public TMP_Text LevelTwoRecordValue = null;
    public TMP_Text LevelThreeRecordValue = null;

    [Header("User Id")]
    public TMP_InputField UserIdInputField = null;
    public TMP_Text UserAlreadyUsedWarning = null;
    public TMP_Text UserSetAlert = null;
    public UnityEngine.UI.Button UsernameNotSetButton = null;

    private void Start()
    {
        UserIdInputField.text = PlayerPrefs.GetString("userId");

        TimeSpan levelOneRecord = TimeSpan.FromMilliseconds(PlayerPrefs.GetFloat("levelOneRecord") *1000);
        TimeSpan levelTwoRecord = TimeSpan.FromMilliseconds(PlayerPrefs.GetFloat("levelTwoRecord") *1000);
        TimeSpan levelThreeRecord = TimeSpan.FromMilliseconds(PlayerPrefs.GetFloat("levelThreeRecord") * 1000);

        LevelOneRecordValue.text = levelOneRecord.ToString(@"mm\:ss\:fff");
        LevelTwoRecordValue.text = levelTwoRecord.ToString(@"mm\:ss\:fff");
        LevelThreeRecordValue.text = levelThreeRecord.ToString(@"mm\:ss\:fff");

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if(PlayerPrefs.GetString("userId") != "" && PlayerPrefs.GetString("userId") != "0") {
            UsernameNotSetButton.gameObject.SetActive(false);
        }
        else
        {
            UsernameNotSetButton.gameObject.SetActive(true);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void OnLevelWasLoaded()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVOlume(float volume)
    {
        AudioListener.volume = volume;
        volumeTestValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void UserIdApply()
    {
        string users = PlayerPrefs.GetString("users");
        string myUserNames = PlayerPrefs.GetString("myUserIds");
        if (myUserNames.Contains(UserIdInputField.text)){
            PlayerPrefs.SetString("userId", UserIdInputField.text);
            UserAlreadyUsedWarning.SetText("");
            UserSetAlert.SetText("Username has been set!");
            PlayerPrefs.SetInt("levelOneRecord", 0);
            PlayerPrefs.SetInt("levelTwoRecord", 0);
            PlayerPrefs.SetInt("levelThreeRecord", 0);
        }
        else
        {
            if (!users.Contains(UserIdInputField.text))
            {
                PlayerPrefs.SetString("userId", UserIdInputField.text);
                UserAlreadyUsedWarning.SetText("");
                UserSetAlert.SetText("Username has been set!");
                string myUserIds = PlayerPrefs.GetString("myUserIds");
                PlayerPrefs.SetString("myUserIds", UserIdInputField.text + ", " + myUserIds);
                PlayerPrefs.SetInt("levelOneRecord", 0);
                PlayerPrefs.SetInt("levelTwoRecord", 0);
                PlayerPrefs.SetInt("levelThreeRecord", 0);
            }
            else
            {
                UserSetAlert.SetText("");
                UserAlreadyUsedWarning.SetText("Username already used!");
            }
        }
    }
}
