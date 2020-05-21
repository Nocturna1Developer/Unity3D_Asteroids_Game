using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    [SerializeField] public static int scoreAmount = 0;

    [SerializeField] public Text scoreText;

    public void Start()
    {
        scoreText = GetComponent<Text>();

        //if (Score.scoreAmount >= 1500)
        //{
        //    NextLevel();
        //}
    }

    public void Update()
    {
        scoreText.text = scoreAmount.ToString();
    }

    //public void NextLevel()
    //{
    //    SceneManager.LoadScene(2);
    //}
}
