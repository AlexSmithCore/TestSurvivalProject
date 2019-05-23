using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycleSystem : MonoBehaviour
{
    void Start()
    {
        TimeManager();
        StartCoroutine(ChangeTimeValues());    
    }

    [Range(0f,1f)]
    public float time;
    [Space]
    public Light sun;
    public Light moon;

    public Light playerLight;

    public GameTime gameTime;

    public float dayCycle = 5f;

    public Text timerText;

    private const int dayLenght = 1440;
    public AnimationCurve intensity;
    public Gradient sunColors;

    IEnumerator ChangeTimeValues(){
        yield return new WaitForSeconds(dayCycle);
        sun.transform.eulerAngles = new Vector3(time * 180f,111.6f,0f);
        TimeManager();
        StartCoroutine(ChangeTimeValues());
    }

    private void TimeManager(){
        if(time >= 1){
            gameTime.day++;
            time  = 0;
        }
        time += 1 / (dayLenght  / 5f);

        float timeFactor = time * dayLenght;
        gameTime.hour = timeFactor / 60;
        gameTime.minute = timeFactor % 60;


        float intensityFactor = intensity.Evaluate(time);
        sun.intensity = intensityFactor;
        RenderSettings.ambientIntensity = intensityFactor;
        sun.color = sunColors.Evaluate(time);
        if(gameTime.hour > 18 || gameTime.hour < 4){
            playerLight.enabled = true;
        } else {
            playerLight.enabled = false;
        }

        string h;
        string m;
        if(gameTime.minute < 10){
            m = "0" + (int)gameTime.minute;
        } else {
            m = "" + (int)gameTime.minute;
        }

        if(gameTime.hour < 10){
            h = "0" + (int)gameTime.hour;
        } else {
            h = "" + (int)gameTime.hour;
        }

        timerText.text = h + ":" + m;
    }
}

[System.Serializable]
public class GameTime{
    public float hour;
    public float minute;
    public int day;
}