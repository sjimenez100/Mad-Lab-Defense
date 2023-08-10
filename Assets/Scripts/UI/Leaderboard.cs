using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;

    [SerializeField]
    private List<TextMeshProUGUI> scores;

    [SerializeField]
    private List<TextMeshProUGUI> dates;


    private static string publicKey = "33ca69f3d57a3f9dc79e5137a28040de34c48f99bbbd1afa7a82349adcb111bf";


    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                dates[i].text = UnixTimeStampToFormattedString(msg[i].Date);
            } 
        }));

    }

    static string UnixTimeStampToFormattedString(ulong unixTimeStamp)
    {
        // Unix timestamp is in seconds, so convert it to a TimeSpan object
        TimeSpan timeSpan = TimeSpan.FromSeconds(unixTimeStamp);

        // Unix timestamp starts from January 1, 1970 (Epoch time)
        DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Add the seconds to the epoch time to get the final DateTime value
        DateTime dateTime = epochTime.Add(timeSpan);

        // Format the DateTime value as "MM/dd/yy hh:mm tt"
        string formattedString = dateTime.ToString("MM/dd/yy hh:mm tt");

        return formattedString;
    }
    public void SetLeaderboardEntry(string username, int score)
    {
        // non-trivial function call below (lets single ID enter several names)
        //LeaderboardCreator.ResetPlayer();
        LeaderboardCreator.UploadNewEntry(publicKey, username, score, (_) => { GetLeaderboard(); }, (msg) => 
        {
            // parses for message
            msg = msg.Split(':')[1].Trim();
            UIManager.main.ShowErrorMessage(msg); 
        
        });
    }

}
