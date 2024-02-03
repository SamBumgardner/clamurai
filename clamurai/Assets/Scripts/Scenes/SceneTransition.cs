using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string NextScene;
    public string OptionalSecondaryScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneSmoothTransition.instance.TransitionScene(NextScene);
            Destroy(this);
        }

        if (!string.IsNullOrEmpty(OptionalSecondaryScene) && Input.GetKeyDown(KeyCode.Z))
        {

            SceneSmoothTransition.instance.TransitionScene(OptionalSecondaryScene);
            Destroy(this);
        }
    }
}
