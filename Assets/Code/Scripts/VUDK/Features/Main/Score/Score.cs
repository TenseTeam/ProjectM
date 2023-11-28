namespace VUDK.Features.Main.Score
{
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Generic.Managers.Main;

    public class Score : MonoBehaviour
    {
        [SerializeField]
        private string _scorePref;

        public int ScoreValue { get; private set; }
        public int HighScore => PlayerPrefs.GetInt(_scorePref);

        private void Start()
        {
            EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnScoreChange, ScoreValue);
            EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnHighScoreChange, HighScore);
        }

        public void ChangeScore(int scoreToAdd)
        {
            ScoreValue += scoreToAdd;

            if(ScoreValue < 0)
                ScoreValue = 0;

            SaveHighScore();
        }

        private void SaveHighScore()
        {
            if (ScoreValue > HighScore)
            {
                PlayerPrefs.SetInt(_scorePref, ScoreValue);
                EventManager.Ins.TriggerEvent(EventKeys.ScoreEvents.OnHighScoreChange, HighScore);
            }
        }
    }
}

