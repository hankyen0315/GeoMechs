using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    private int tutorialStepCount = 0;

    private TutorialStep[] tutorialSteps;
    private bool timeUp = false;


    public static TutorialManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        tutorialSteps = new TutorialStep[]
        {
            new TutorialStep
            {
                stepID = 0,
                condition = () => (LevelManager.State == LevelState.Prepare && Time.timeSinceLevelLoad > 0.8f), // Replace with actual condition
                action = () => UIManager.Instance.ShowTutorial()
            },
            new TutorialStep
            {
                stepID = 1,
                condition = () => LevelManager.State == LevelState.Fight, // Replace with actual condition
                action = () => 
                {
                    ShowNextTutorial(); 
                    GameManager.Instance.PauseGame(); // Pause the game when the tutorial starts
                }

            },
            new TutorialStep
            {
                stepID = 2,
                condition = () => LevelManager.State == LevelState.Prepare && PlayerStatsManager.Instance.GetMaxOverdrive() > 0, // Replace with actual condition
                action = () =>
                {
                    StartCoroutine(ActionAfterWaitTime(1f, ShowNextTutorial));
                }
            },
            // Add more steps as needed
        };
    }



    private void Update()
    {
        if (tutorialStepCount >= tutorialSteps.Length)
            return;
        if (tutorialSteps[tutorialStepCount].condition() == true)
        {
            Debug.Log($"Tutorial Step {tutorialSteps[tutorialStepCount].stepID} completed.");
            tutorialSteps[tutorialStepCount].action();
            tutorialStepCount++;
        }
    }


    private static void ShowNextTutorial()
    {
        Debug.Log("Showing next tutorial page.");
        UIManager.Instance.AvailablePageAmount++;
        UIManager.Instance.ShowTutorial();
        UIManager.Instance.ShowTutorialPage(UIManager.Instance.AvailablePageAmount);
    }

    private IEnumerator ActionAfterWaitTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

}
