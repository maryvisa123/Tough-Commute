using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using System.Collections;
using UnityEngine.SceneManagement;

public class PointsController : MonoBehaviour
{
    public Slider pointsSlider;
    private int maxPoints = 100;
    private int currentPoints = 50; // Start at a certain number between min and max
    private int minPoints = 0; // Define min points if needed

    private Coroutine pointsSubtractCoroutine;

    private void Start()
    {
        // Initialize points and UI
        pointsSlider.maxValue = maxPoints;
        pointsSlider.value = currentPoints;
    }


    public void AddPoints(int points)
    {
        currentPoints += points;
        currentPoints = Mathf.Clamp(currentPoints, minPoints, maxPoints);
        pointsSlider.value = currentPoints;
    }

    public void RemovePoints(int points)
    {
        currentPoints -= points;
        currentPoints = Mathf.Clamp(currentPoints, minPoints, maxPoints);
        pointsSlider.value = currentPoints;
    }

}