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
            RenderSettings.fogMode = FogMode.Linear;
            StartCoroutine(TimeChange());
        }

        private void Update()
        {
            Second += Time.deltaTime*Multiplier;
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

                RenderSettings.fogStartDistance = -1250;
                RenderSettings.fogColor = NightColor;
                transform.eulerAngles = new Vector3(265, 40, 0);
                
                //Ночь

                while (Hour >= 0 && Hour < 3)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second)  / (3 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(265, 40, 0), new Vector3(175, 40, 0), t);
                    yield return null;
                }
                

                //Ночь-рассвет
                
                while (Hour >= 3 && Hour < 5)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 3*3600)  / (2 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(175, 40, 0), new Vector3(160, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(NightColor, DawnColor, t);
                    RenderSettings.fogStartDistance = Mathf.Lerp(-1250, -500, t);
                    yield return null;
                }

                //Рассвет-день
                
                
                while (Hour >= 5 && Hour < 7)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 5*3600)  / (2 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(160, 40, 0), new Vector3(140, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(DawnColor, DayColor, t); 
                    RenderSettings.fogStartDistance = Mathf.Lerp(-500, 0, t);
                    yield return null;
                }

                //День
                
                RenderSettings.fogStartDistance = 0;
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
                    RenderSettings.fogStartDistance = Mathf.Lerp(0, -350, t);
                    yield return null;
                }

                //Закат
                
                
                while (Hour >= 17 && Hour < 21)
                {
                    float t = (Hour * 3600 + Minute * 60 + Second - 17*3600)  / (4 * 3600);
                    transform.eulerAngles = Vector3.Lerp( new Vector3(20, 40, 0), new Vector3(5, 40, 0), t);
                    RenderSettings.fogColor = Color.Lerp(DuskColor, NightColor, t);
                    RenderSettings.fogStartDistance = Mathf.Lerp(-350, -1000, t);
                    yield return null;
                }

                //Ночь

                RenderSettings.fogStartDistance = -1000;
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