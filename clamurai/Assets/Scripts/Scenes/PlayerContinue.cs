using UnityEngine;
using UnityEngine.SceneManagement;

// This script is super sketchy, just slapped it in here to have a basic strategy for letting the player try again.
public class PlayerContinue : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(Player.MostRecentlyStartedScene);
        }   
    }
}
