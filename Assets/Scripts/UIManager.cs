using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    void Awake(){
        instance = this;
    }

    public bool pause;

    public Transform player;

    public GameObject pauseMenu;
    public GameObject mainUI;

    public GameObject PrefabExitButton;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            pause = !pause;

            Cursor.visible = pause;
            pauseMenu.SetActive(pause);
            mainUI.SetActive(!pause);

            if(pause){
                Time.timeScale = 0f;
            } else {
                Time.timeScale = 1f;
            }
        }
    }

    public void ExitButton(){
        PrefabExitButton.SetActive(true);
    }

    public void ExitYesButton(){
        if(UserData.instance == null){
            Debug.Log("User Data not loaded!");
            return;
        }
        UserData.instance.SaveDataToUserData(new SaveData(player.position, UserData.instance.namePlayer));
        Application.Quit();
    }

    public void ExitNoButton(){
        PrefabExitButton.SetActive(false);
    }

    public void MainMenuButton(){
        if(UserData.instance == null){
            Debug.Log("User Data not loaded!");
            return;
        }
        UserData.instance.SaveDataToUserData(new SaveData(player.position, UserData.instance.namePlayer));
        SceneManager.LoadScene(0);
    }
}
