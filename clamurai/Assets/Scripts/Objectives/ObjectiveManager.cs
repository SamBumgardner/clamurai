using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public string NextScene = string.Empty;
    List<bool> objectiveCompletionStatus = new List<bool>();

    private void Start()
    {
        var objectives = GameObject.FindGameObjectsWithTag("Objective");
        for (var i = 0; i < objectives.Length; i++)
        {
            var objectiveComponent = objectives[i].GetComponent<IObjective>();
            objectiveComponent.ObjectiveStatusChange += OnObjectiveStatusChange;
            objectiveComponent.ID = i;
            objectiveCompletionStatus.Add(false);
        }
        print($"Setup complete for {objectiveCompletionStatus.Count} objectives.");
    }

    public void OnObjectiveStatusChange(object sender, ObjectiveStatusChangeEventArgs args)
    {
        objectiveCompletionStatus[args.ObjectiveID] = args.IsComplete;
        // can use args.Progress here to update information too for partial progress.
        // update HUD information (or send relevant events, whatever's fine).

        if (IsLevelComplete())
        {
            LevelFinished();
        }
    }

    private bool IsLevelComplete()
    {
        // Returns true if all completionStatus entries are true.
        return objectiveCompletionStatus.All(status => status);
    }

    public int GetCompletedObjectiveCount()
    {
        return objectiveCompletionStatus.Count(status => status);
    }

    public int GetTotalObjectiveCount()
    {
        return objectiveCompletionStatus.Count();
    }

    private void LevelFinished()
    {
        // Better to trigger some kind of fanfare here, probably.
        // This works fine from a functionality perspective though.
        if (!string.IsNullOrEmpty(NextScene))
        {
            SceneSmoothTransition.instance.TransitionScene(NextScene);
        }
    }
}