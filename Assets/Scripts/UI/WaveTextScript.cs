using System.Collections;
using System.Collections.Generic;
using Enemies;
using TMPro;
using UI.Pause;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Variables = Game.Variables;

public class WaveTextScript : MonoBehaviour
{
    public GameObject EnemyIconPrefab;
    public GameObject IconSpot;

    private GameObject _panel;
    private TextMeshProUGUI _text;
    private Vector3 _textPosition;
    
    public static WaveTextScript waveTextScript;
    
    void Start()
    {
        waveTextScript = this;
        _panel = GetComponentInChildren<CanvasGroup>().gameObject;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _textPosition = _text.transform.position;
        _text.transform.position = new Vector3(_textPosition.x, _textPosition.y + 200, _textPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TextMove(params Variables.EnemyType[] enemies)
    {
        yield return new WaitForSeconds(3f);
        Level.currentLevel.WaveCount++;
        _text.transform.position = _textPosition;
        float t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
                yield return null;
            _text.color = Color.Lerp(Color.clear, Color.white, t);
            t += Time.fixedDeltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
                yield return null;
            _text.color = Color.Lerp(Color.white, Color.clear, t);
            t += Time.fixedDeltaTime;
            yield return null;
        }
        _text.transform.position = new Vector3(_textPosition.x, _textPosition.y + 200, _textPosition.z);
        for(int i = 0; i< enemies.Length; i++)
        {
            GameObject gm = Instantiate(EnemyIconPrefab, IconSpot.transform);
            gm.GetComponent<Image>().sprite = Level.currentLevel.EnemyDict[enemies[i]].GetComponent<Enemy>().Icon;
            RectTransform rt = gm.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(-135 * (enemies.Length - 1) / 2 + i * 135,
                0);
        }

        _panel.GetComponentInChildren<TextMeshProUGUI>().text = "WAVE: " + Level.currentLevel.WaveCount;
        yield return new WaitForSeconds(3f);
        CanvasGroup group = _panel.GetComponent<CanvasGroup>();
        t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
                yield return null;
            group.alpha = Mathf.Lerp(0, 1, t);
            t += Time.fixedDeltaTime*2;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        t = 0;
        while (t<1)
        {
            while (PauseScript.IsPaused)
                yield return null;
            group.alpha = Mathf.Lerp(1, 0, t);
            t += Time.fixedDeltaTime*2;
            yield return null;
        }
        group.alpha = 0;
        foreach (Image gm in _panel.GetComponentsInChildren<Image>())
        {
            if(gm.name==_panel.name)
                continue;
            Destroy(gm.gameObject);
        }
        yield return new WaitForSeconds(3f);
    }
}
