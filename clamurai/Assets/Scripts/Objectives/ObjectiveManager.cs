using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    List<bool> objectiveCompletionStatus = new List<bool>();

    private void Start()
    {
        var objectives = GameObject.FindGameObjectsWithTag("Objective");
        for (var i = 0 ; i < objectives.Length; i++)
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

        if (objectiveCompletionStatus.All(status => status)) // if every item in list is true:
        {
            // trigger game completion!
            print("Objectives Complete! Go have a snack.");
        }
    }
}