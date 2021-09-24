using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameState : MonoBehaviour
{
    private static GameState instance;
    private int score;
    private string playerName;
    private int highestScore;
    private string highestScorePlayerName;
    private const string PathToSave = "/savefile.json";
    private bool isNewRecord = false;

    public static GameState Instance { get => instance; }
    public int Score { get => score; set => score = value; }
    public int HighestScore { get => highestScore; set => highestScore = value; }
    public string HighestScorePlayerName { get => highestScorePlayerName; set => highestScorePlayerName = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public bool IsNewRecord { get => isNewRecord; set => isNewRecord = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMenuStats()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            if (this.highestScore > 0)
            {
                string highestText = "Best Score: " + this.highestScore + "\nName: " + this.highestScorePlayerName;
                GameObject.Find("RecordText").GetComponent<TextMeshProUGUI>().text = highestText;
            }
            else
            {
                GameObject.Find("RecordText").SetActive(false);
            }
        }
    }

    public void SaveData()
    {
        SavedData data = new SavedData();
        data.score = this.score;
        data.highestScore = this.highestScore;
        data.playerName = this.playerName;
        data.highestScorePlayerName = this.highestScorePlayerName;

        File.WriteAllText(Application.persistentDataPath + PathToSave, JsonUtility.ToJson(data));
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + PathToSave;

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SavedData data = JsonUtility.FromJson<SavedData>(json);
        }
    }

    [System.Serializable]
    public class SavedData
    {
        public int score;
        public int highestScore;
        public string playerName;
        public string highestScorePlayerName;
    }

    public void UpdateScore(int m_Points)
    {
        this.score = m_Points;
        if (m_Points > this.highestScore)
        {
            this.highestScore = m_Points;
            this.highestScorePlayerName = this.playerName;
            this.isNewRecord = true;
        }
    }
}
