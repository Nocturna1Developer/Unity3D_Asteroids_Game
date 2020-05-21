using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class Lives : MonoBehaviour
{
    [SerializeField] public static int livesAmount = 3;

    [SerializeField] public Text livesText;

    public void Start()
    {
        livesText = GetComponent<Text>();

        if (Lives.livesAmount <= 0)
        {
            GameLost();
        }
    }

    public void Update()
    {
        livesText.text = livesAmount.ToString();
    }

    public void GameLost()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
