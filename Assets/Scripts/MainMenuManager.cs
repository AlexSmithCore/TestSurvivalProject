using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    string[] fileEntries;
    List<SaveData> listData;
    public GameObject PrefabLoadButtonPanel;
    public GameObject PrefabLoadButtonInfo;
    public GameObject PrefabStartGame;

    public InputField nameInput;
    private void Awake()
    {
        CloseAllUI();

        listData = new List<SaveData>();

        //Путь к сохранениям
        string path = Application.dataPath + "/Data/Saves";
        //Получение всех файлов сохранений
        fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            try
            {
                SaveData sd = (SaveData)bformatter.Deserialize(fs);
                listData.Add(sd);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message + " " + fileName);
            }
            finally
            {
                fs.Close();
            }
        }
        //Если сохранений нет, то нет кнопки продолжить и загрузить
        if (fileEntries.Length == 0)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = true;
            listData.Sort();
            listData.Reverse();
            for (int i = 0; i < listData.Count; ++i)
            {
                GameObject newLoadButtonInfo = Instantiate(PrefabLoadButtonInfo, transform.GetChild(1));
                newLoadButtonInfo.transform.GetChild(0).GetComponent<Text>().text = listData[i].name;
                newLoadButtonInfo.transform.GetChild(1).GetComponent<Text>().text = "Last Save: " + listData[i].dataTime.ToShortTimeString() + listData[i].dataTime.ToShortDateString();
                int a = i;
                newLoadButtonInfo.GetComponent<Button>().onClick.AddListener(delegate { LoadGame(a);});
            }
        }
    }

    private void Update(){
        if(nameInput.text.Length > 0){
            transform.GetChild(2).GetChild(2).GetComponent<Button>().interactable = true;
        } else {
            transform.GetChild(2).GetChild(2).GetComponent<Button>().interactable = false;
        }
    }

    public void СontinueGame()
    {
        UserData.instance.SaveDataToUserData(listData[0]);
        SceneManager.LoadScene(listData[0].currentSceneID);
    }

    public void NewGame()
    {
        CloseAllUI();
        PrefabStartGame.SetActive(true);
    }

    public void StartNewGame(){
        UserData.instance.namePlayer = nameInput.text;
        SceneManager.LoadScene(1);
    }   

    public void LoadGameButton()
    {
        CloseAllUI();
        PrefabLoadButtonPanel.SetActive(true);
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void LoadGame(int index)
    {
        Debug.Log(index);
        UserData.instance.SaveDataToUserData(listData[index]);
        SceneManager.LoadScene(listData[index].currentSceneID);
    }

    public void CloseAllUI(){
        PrefabLoadButtonPanel.SetActive(false);
        PrefabStartGame.SetActive(false);
    }
}
