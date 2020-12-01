using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    // Start is called before the first frame update
    private bool loadScene;
    void Start()
    {
        loadScene = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(loadScene){
            StartCoroutine(LoadScene());
        }
        
    }

    public void LoadNextScene() {
        loadScene = true;
    }

    IEnumerator LoadScene() {
        Debug.Log("Loading scene......");
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene Loaded.....");
        loadScene = false;
    }
}
