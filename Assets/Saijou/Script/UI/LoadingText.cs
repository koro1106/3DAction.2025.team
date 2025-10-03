using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string fullText = "NowLoading...";
    public float delay = 0.1f;// 遅延時間

    private Coroutine typingCoroutine;
    void Start()
    {
        text.text = "チェック中...";
    }
    public void StartTyping()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {
        text.text = "";
        foreach(char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
