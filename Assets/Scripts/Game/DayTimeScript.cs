using System;
using System.Collections;
using System.Timers;
using UI.Pause;
using UnityEngine;

namespace Game
{
    public class DayTimeScript:MonoBehaviour
    {
        [Header("Стартовое время")] 
        public int Hour;
        public int Minute;
        public float Second;

        [Header("Другие параметры")] 
        public float Multiplier;

        public Color NightColor;
        public Color DawnColor;
        public Color DayColor;
        public Color DuskColor;

        private void Start()
        {
            RenderSettings.fog = true;
            StartCoroutine(TimeChange());
        }

        private void FixedUpdate()
        {
            if(PauseScript.IsPaused)
                return;
            Second += Time.fixedDeltaTime*Multiplier;
            if (Second >= 60)
            {
                Second = 0;
                Minute++;
            }

            if (Minute == 60)
            {
                Minute = 0;
                Hour++;
            }

            if (Hour == 24)
            {
                Hour = 0;
            }
        }

        public IEnumerator TimeChange()
        {
            while (true)
            {
                yield return new WaitUntil(() => Hour >= 0);

                RenderSettings.fogDensity = 0.015f;
                RenderSettings.fogColor = NightColor;
                transform.eulerAngles = new Vector3(265, 40, 0);
                
                //Ночь

                while (Hour >= 0 && Hour < 3)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second)  / (3 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(265, 40, 0), new Vector3(175, 40, 0), t);
                    yield return null;
                }

                yield return new WaitUntil(() => Hour >= 3);

                //Ночь-рассвет
                
                while (Hour >= 3 && Hour < 5)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 3*3600)  / (2 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(175, 40, 0), new Vector3(160, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(NightColor, DawnColor, t);
                    RenderSettings.fogDensity = Mathf.Lerp(0.015f, 0.005f, t);
                    yield return null;
                }
                
                yield return new WaitUntil(() => Hour >= 5);
                
                //Рассвет-день
                
                
                while (Hour >= 5 && Hour < 7)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 5*3600)  / (2 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(160, 40, 0), new Vector3(140, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(DawnColor, DayColor, t);
                    RenderSettings.fogDensity = Mathf.Lerp(0.005f, 0.01f, t);
                    yield return null;
                }
                
                yield return new WaitUntil(() => Hour >= 7);
                
                //День
                
                RenderSettings.fogDensity = 0.01f;
                RenderSettings.fogColor = DayColor;
                transform.eulerAngles = new Vector3(140, 40, 0);

                while (Hour >= 7 && Hour < 15)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 7*3600)  / (8 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(140, 40, 0), new Vector3(40, 40, 0), t);
                    yield return null;
                }
                
                while (Hour >= 15 && Hour < 17)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 15*3600)  / (2 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(40, 40, 0), new Vector3(20, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(DayColor, DuskColor, t);
                    RenderSettings.fogDensity = Mathf.Lerp(0.01f, 0.01f, t);
                    yield return null;
                }
                
                yield return new WaitUntil(() => Hour >= 17);
                
                //Закат
                
                
                while (Hour >= 17 && Hour < 21)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 17*3600)  / (4 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(20, 40, 0), new Vector3(5, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(DuskColor, NightColor, t);
                    RenderSettings.fogDensity = Mathf.Lerp(0.01f, 0.015f, t);
                    yield return null;
                }
                
                yield return new WaitUntil(() => Hour >= 21);
                
                //Ночь
                
                RenderSettings.fogDensity = 0.015f;
                RenderSettings.fogColor = NightColor;
                transform.eulerAngles = new Vector3(5, 40, 0);

                while (Hour >= 21 && Hour < 24)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 21*3600)  / (3 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(5, 40, 0), new Vector3(-90, 40, 0), t);
                    yield return null;
                }
                
            }
        }
    }
}