using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LoadingScreenManager Instance;
    public GameObject m_LoadingScreenObject;
    public Slider ProgressBar;

    private void Awake() 
    {
       if (Instance != null && Instance != this)
       {
         Destroy(this.gameObject);
       } 
       else
       {
         Instance = this;
         DontDestroyOnLoad(this.gameObject);
       }
    }

    // Update is called once per frame
    public void SwitchToScene(int id)
    {
        m_LoadingScreenObject.SetActive(true);
         ProgressBar.value = 0;
         StartCoroutine(SwitchToSceneAsyc(id));
    }

    IEnumerator SwitchToSceneAsyc(int id)
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(id);
        while (!asyncload.isDone)
        {
            ProgressBar.value = asyncload.progress; 
            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        m_LoadingScreenObject.SetActive(false);
    }
}
