using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using TMPro;
using System.Linq;

public class FirebaseLeaderboard : MonoBehaviour
{
    private string apiKey = "AIzaSyAORgiYBmdy7UWKq9u_vMNGFUuoaMy3wsI";
    private string databaseURL = "https://aeliana-s-journey-default-rtdb.europe-west1.firebasedatabase.app";
    private string userID; // Replace with the actual user ID
    string databaseLevelLeaderboardName = "LevelLeaderboard";

    string[] levelLeaderboard = {"LevelOne", "LevelTwo", "LevelThree" };

    Dictionary<string, string> sceneToLevelRecord = new Dictionary<string, string>()
    {
        {"LevelOne", "levelOneRecord"},
        {"LevelTwo", "levelTwoRecord"},
        {"LevelThree", "levelThreeRecord"}
    };

    public TMP_Text level1 = null;
    public TMP_Text level2 = null;
    public TMP_Text level3 = null;

    [Serializable]
    public class LeaderboardEntry
    {
        public string userID;
        public float time;
    }

    public void Start()
    {
        userID = PlayerPrefs.GetString("userId");
        StartCoroutine(GetUser());
    }

    public void GetAllLeaderboardEntries()
    {
        GetLeaderboard("LevelOne", level1);
        GetLeaderboard("LevelTwo", level2);
        GetLeaderboard("LevelThree", level3);
    }

    public void UploadTime(string level)
    {
        StartCoroutine(UploadDataToFirebase(level, PlayerPrefs.GetFloat(sceneToLevelRecord[level])));
    }

    public void GetLeaderboard(string level, TMP_Text levelText)
    {
        StartCoroutine(GetLeaderboardData(level, levelText));
    }
    IEnumerator GetLeaderboardData(string level, TMP_Text levelText)
    {
        string apiUrl = $"{databaseURL}/{databaseLevelLeaderboardName}/{level}.json?auth={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log(jsonResponse);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonResponse);
                var valuesList = values.ToList();
                valuesList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                string leaderBoardText = "";
                foreach (var entry in valuesList)
                {
                    TimeSpan score = TimeSpan.FromMilliseconds(entry.Value);
                    leaderBoardText += entry.Key + " " + score.ToString(@"mm\:ss\:fff") + "\n";
                }
                levelText.text = leaderBoardText;
            }
            else
            {
                Debug.LogWarning("Leaderboard data is empty.");
            }
        }
        else
        {
            Debug.LogError($"Error getting leaderboard data. Status Code: {request.responseCode}\nError: {request.error}");
        }
    }

    IEnumerator UploadDataToFirebase(string level, float levelTime)
    {
        string apiUrl = $"{databaseURL}/{databaseLevelLeaderboardName}/{level}/{userID}.json?auth={apiKey}";
        int time = (int)(levelTime * 1000);

        UnityWebRequest existingRequest = UnityWebRequest.Get(apiUrl);
        yield return existingRequest.SendWebRequest();

        if (existingRequest.result == UnityWebRequest.Result.Success)
        {
            if (existingRequest.downloadHandler != null && existingRequest.downloadHandler.text != null && existingRequest.downloadHandler.text != "null")
            {
                if (float.TryParse(existingRequest.downloadHandler.text, out float existingTime))
                {
                    if (time < existingTime)
                    {
                        StartCoroutine(UpdateExistingTime(apiUrl, time));
                    }
                }
                else
                {
                    Debug.LogError($"Error parsing existing time. Value: {existingRequest.downloadHandler.text}");
                }
                if (time < existingTime)
                {
                    StartCoroutine(UpdateExistingTime(apiUrl, time));
                }
            }
            else
            {
                StartCoroutine(CreateNewEntry(apiUrl, time));
            }
        }
        else
        {
            Debug.LogError($"Error checking existing data. Status Code: {existingRequest.responseCode}\nError: {existingRequest.error}");
        }
    }

    IEnumerator UpdateExistingTime(string apiUrl, int time)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(time.ToString());

        UnityWebRequest updateRequest = UnityWebRequest.Put(apiUrl, data);
        updateRequest.method = UnityWebRequest.kHttpVerbPUT;
        updateRequest.SetRequestHeader("Content-Type", "application/json");

        yield return updateRequest.SendWebRequest();

        if (updateRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Time updated successfully.");
        }
        else
        {
            Debug.LogError($"Error updating time. Status Code: {updateRequest.responseCode}\nError: {updateRequest.error}");
        }
    }

    IEnumerator CreateNewEntry(string apiUrl, float time)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(time.ToString());

        UnityWebRequest createRequest = UnityWebRequest.Put(apiUrl, data);
        createRequest.method = UnityWebRequest.kHttpVerbPUT;
        createRequest.SetRequestHeader("Content-Type", "application/json");

        yield return createRequest.SendWebRequest();

        if (createRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("New entry created successfully.");
        }
        else
        {
            Debug.LogError($"Error creating new entry. Status Code: {createRequest.responseCode}\nError: {createRequest.error}");
        }
    }

    IEnumerator GetUser()
    {
        string users = "";
        foreach(var level in levelLeaderboard)
        {
            string apiUrl = $"{databaseURL}/{databaseLevelLeaderboardName}/{level}.json?auth={apiKey}";

            UnityWebRequest request = UnityWebRequest.Get(apiUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log(jsonResponse);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                    foreach (var entry in values)
                    {
                        users += entry.Key + ", ";
                    }
                }
                else
                {
                    Debug.LogWarning("Leaderboard data is empty.");
                }
            }
            else
            {
                Debug.LogError($"Error getting leaderboard data. Status Code: {request.responseCode}\nError: {request.error}");
            }
        }
        PlayerPrefs.SetString("users", users);
    }
}