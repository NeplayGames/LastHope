using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MissionHelper : MonoBehaviour
{

    public int mission;
    [SerializeField] Text missionNumber;
  
    [SerializeField] int totalMission = 1;
    [SerializeField] GameObject missionInfo;
    
    #region start
    private void Start()
    {
        string saveFile = "missionInfo";
        string path = GetPathFromSaveFile(saveFile);
        if (File.Exists(path))
            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                BinaryFormatter formatter = new BinaryFormatter();
                Serializables data = (Serializables)formatter.Deserialize(stream);
                mission = data.Mission();

            }
        else

            mission = 0;


        LoadMissionInfo();
       
     
    }
    #endregion

    #region update

    #endregion
   
  
   

    private void LoadMissionInfo()
    {
      

        missionNumber.text = "Mission " + mission.ToString();
        if (mission != totalMission)
        {
            SceneManager.LoadSceneAsync("mission" + mission.ToString(), LoadSceneMode.Additive);
        }

        StartCoroutine(DeactivateTextMission());
    }

    public void AddMissionInfo(int mis)
    {
        string saveFile = "missionInfo";
        string path = GetPathFromSaveFile(saveFile);
        using (FileStream stream = File.Open(path, FileMode.Create))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, new Serializables(mis));
          
        }
    }

    public void MissionComplete()
    {

        mission++;
      
          
        
        // AddMissionInfo(mission);

            StartCoroutine(DeactivateText());


    }

    #region firstMission
    IEnumerator DeactivateText()
    {

        yield return new WaitForSeconds(5f);
        LoadMissionInfo();
        SceneManager.UnloadSceneAsync("mission" + (mission-1).ToString());

        missionInfo.SetActive(false);
        missionNumber.gameObject.SetActive(false);
      
    }
    IEnumerator DeactivateTextMission()
    {

        yield return new WaitForSeconds(5f);
      

        missionInfo.SetActive(false);
        missionNumber.gameObject.SetActive(false);
    }
    private string GetPathFromSaveFile(string saveFile)
    {
        return Application.persistentDataPath + "/" + saveFile + ".sav";

    }
    #endregion
}
