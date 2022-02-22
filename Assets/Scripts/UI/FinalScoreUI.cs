using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Demo.Core;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameField = null;
    [SerializeField] TextMeshProUGUI scoreField = null;
    private GameObject _player;
    private HighScores _highScores;

    private void OnEnable() 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _highScores = _player.GetComponent<HighScores>();
        HighScore latestScore = _highScores.GetLatestScore();
        nameField.text = latestScore.Name;
        scoreField.text = latestScore.Score.ToString();
        Debug.Log("name and Score: " + nameField.text + ": " + scoreField.text);
    }
    public void SetFields(string name, string score)
    {
        Debug.Log("Final Score Set Fields Called");
        nameField.text = name;
        scoreField.text = score;
    }
}
