using UnityEngine;

#if UNITY_ANDROID

using Library.GooglePlay;

public class AndroidLeaderboardExample : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool isInitialized;

    void Start()
    {
        GooglePlayLeaderboard.InitializeLeaderboard((success) =>
        {
            isInitialized = success;

            if (success)
            {
                // Everything is good to go
            }
            else
            {
                // Something went wrong
            }
        });
    }

    public void ReportScore()
    {
        GooglePlayLeaderboard.ReportScore(25);
    }

    public void ShowLeaderboard()
    {
        GooglePlayLeaderboard.ShowLeaderboard();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ReportScore();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ShowLeaderboard();
        }
    }
}

#endif