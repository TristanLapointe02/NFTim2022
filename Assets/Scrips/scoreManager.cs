using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;

    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
    }

    public void pointsVache()
    {
        score += 3;
        scoreText.text = score.ToString() + " POINTS";
    }

    public void pointsMouton()
    {
        score += 5;
        scoreText.text = score.ToString() + " POINTS";
    }

    public void pointsChien()
    {
        score += 10;
        scoreText.text = score.ToString() + " POINTS";
    }

    public void pointsCheval()
    {
        score += 15;
        scoreText.text = score.ToString() + " POINTS";
    }

    public void pointsLama()
    {
        score += 20;
        scoreText.text = score.ToString() + " POINTS";
    }
}
