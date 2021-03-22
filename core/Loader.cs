using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private float _progress = 0f;
    [SerializeField] Canvas ca;
    [SerializeField] Slider slider;
    [SerializeField] Image loader;
   [SerializeField] GameObject enviroment;
    [SerializeField] GameObject instant;
    private void Awake()
    {
        DontDestroyOnLoad(ca.gameObject);
        DontDestroyOnLoad(this.gameObject);
      //  instant.SetActive(false);
    }
    public void OnLoadLevelClick(int sceneIndex)
    {
      loader.gameObject.SetActive(true);

        StartCoroutine(LoadAsync(sceneIndex));
    }



    IEnumerator LoadAsync(int sceneIndex)
    {
        enviroment.SetActive(false);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;

        while (_progress < 1f)
        {
            _progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = _progress;
          
            yield return null;
        }
        operation.allowSceneActivation = true;
        //loader.gameObject.SetActive(false);
        instant.gameObject.SetActive(false);
      
        
    }
}
