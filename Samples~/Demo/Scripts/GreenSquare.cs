using Racer.EzSaverLite.Core;
using Racer.EzSaverLite.Wrappers;
using UnityEngine;
using UnityEngine.UI;

namespace Racer.EzSaverLite.Samples
{
    /// <summary>
    /// Demonstrates how to use <see cref="SaverManager"/> of the EzSaverLite package to save and load data.
    /// </summary>
    internal class GreenSquare : MonoBehaviour
    {
        // Cached variable to be initialized once and used across
        private ISaver _saver;

        private int _score;
        private int _highscore;

        private Button _button;
        private Text _scoreText;
        private Text _highscoreText;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _scoreText = transform.GetChild(0).GetComponent<Text>();
            _highscoreText = transform.GetChild(1).GetComponent<Text>();
        }

        private void Start()
        {
            _button.onClick.AddListener(AddScore);

            // One time initialization
            _saver = SaverManager.Saver;

            // Reused here
            _highscore = _saver.GetInt("Highscore");

            SetHighscoreText();
        }

        private void AddScore()
        {
            _score++;

            SetScoreText();

            if (_highscore >= _score) return;

            _highscore = _score;

            SetHighscoreText();

            // Reused here
            _saver.SaveInt("Highscore", _highscore);
        }

        private void SetScoreText()
        {
            _scoreText.text = "Score: " + _score;
        }

        private void SetHighscoreText()
        {
            _highscoreText.text = "Highscore: " + _highscore;
        }

        public void ClearAll()
        {
            // Alternatively, using the static instance (unsimplified)
            SaverManager.Saver.ClearAll();

            _score = _highscore = 0;

            SetScoreText();
            SetHighscoreText();
        }
    }
}