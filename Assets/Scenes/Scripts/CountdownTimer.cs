using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Slider timerSlider;
    public float totalTime = 60f; // Total time in seconds
    private float timeLeft;

    void Start()
    {
        timeLeft = totalTime;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft;
        }
        else
        {
            // Time's up, go to Game Over scene
            SceneManager.LoadScene("GameOverScene"); // Replace with your actual Game Over scene name
        }
    }
}
