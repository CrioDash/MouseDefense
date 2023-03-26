using System.Collections;
using System.Collections.Generic;
using Enemies;
using TMPro;
using UI;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Variables = GameData.Variables;

public class WaveTextScript : MonoBehaviour
{
    public GameObject EnemyIconPrefab;
    public GameObject IconSpot;

    private GameObject _panel;
    private Vector3 _textPosition;
    
    public static WaveTextScript Instance;
    
    void Start()
    {
        Instance = this;
        _panel = GetComponentInChildren<CanvasGroup>().gameObject;
    }

    public IEnumerator TextMove(params Variables.EnemyType[] enemies)
    {
        Level.Instance.WaveCount++;
        float t;
        for(int i = 0; i< enemies.Length; i++)
        {
            GameObject gm = Instantiate(EnemyIconPrefab, IconSpot.transform);
            gm.GetComponent<Image>().sprite = Level.Instance.EnemyDict[enemies[i]].GetComponent<Enemy>().Icon;
            RectTransform rt = gm.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(-135 * (enemies.Length - 1) / 2 + i * 135,
                0);
        }

        _panel.GetComponentInChildren<TextMeshProUGUI>().text = "WAVE: " + Level.Instance.WaveCount;
        yield return new WaitForSeconds(3f);
        CanvasGroup group = _panel.GetComponent<CanvasGroup>();
        t = 0;
        while (t<1)
        {
            group.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime*2;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        t = 0;
        while (t<1)
        {
            group.alpha = Mathf.Lerp(1, 0, t);
            t += Time.deltaTime*2;
            yield return null;
        }
        group.alpha = 0;
        foreach (Image gm in _panel.GetComponentsInChildren<Image>())
        {
            if(gm.name==_panel.name)
                continue;
            Destroy(gm.gameObject);
        }

        WaveTimerScript.Instance.TimerStart(5);
        while (!WaveTimerScript.Instance.waited)
        {
            yield return null;
        }
    }
}
