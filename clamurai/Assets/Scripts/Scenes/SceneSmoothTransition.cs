using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSmoothTransition : MonoBehaviour
{
    public static SceneSmoothTransition instance;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Awake");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Debug.Log("Start");
    }

    public void TransitionScene(string scene)
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DelayScene(scene));
    }

    private IEnumerator DelayScene(string scene)
    {
        animator.SetBool("Open", false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
        animator.SetBool("Open", true);
    }
    // Update is called once per frame

}
