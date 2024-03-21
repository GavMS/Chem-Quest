using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Animator crossFade;

    int nextLevelBuildIndex;

    public GameObject StartText;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        crossFade = GameObject.Find("Transition").GetComponent<Animator>();

        StartCoroutine(WaitForTitle());

    }

    IEnumerator WaitForTitle()
    {
        yield return new WaitForSeconds(5);
        if (StartText != null)
        {
            StartText.SetActive(true);
        }
        while (true)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Space))
                {
                    GetComponent<AudioScript>().PlayAudio(0);
                    GameObject.Find("Player").SendMessage("Portal");
                    LoadScene(1);
                    break;
                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RestartScene()
    {       
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {       
        LoadScene(0);
    }

    public void NextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    

    public void LoadScene(int indexBuild){
        nextLevelBuildIndex = indexBuild;
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevelBuildIndex);
    }
}
