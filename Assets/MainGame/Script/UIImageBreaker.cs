using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class UIImageBreaker : MonoBehaviour

{

    [Header("対象のImage")]

    public Image targetImage;

    [Header("破片Prefab（UI Image）")]

    public GameObject fragmentPrefab;

    [Header("破壊設定")]

    public int fragments = 12;       // 何個の破片にするか

    public float scatterRadius = 50f; // 散らばる範囲

    public float fallDistance = 200f; // 下に落ちる距離

    public float delayBeforeBreak = 2f; // 破壊までの秒数

    public float scatterSpeed = 1f;   // 散らばるスピード

    private bool broken = false;

    void Start()

    {

        // 指定秒数後に破壊処理を開始

        Invoke(nameof(BreakImage), delayBeforeBreak);

    }

    void BreakImage()

    {

        if (broken) return;

        broken = true;

        // 元のImageを非表示

        targetImage.enabled = false;

        // RectTransformの中心位置

        RectTransform rt = targetImage.rectTransform;

        Vector2 basePos = rt.anchoredPosition;

        // 破片を生成

        for (int i = 0; i < fragments; i++)

        {

            GameObject frag = Instantiate(fragmentPrefab, rt.parent);

            RectTransform fragRT = frag.GetComponent<RectTransform>();

            // 生成位置は元のImageの中心付近

            fragRT.anchoredPosition = basePos;

            // 破片サイズを調整（小さくする）

            fragRT.sizeDelta = rt.sizeDelta / 4f;

            // 色をコピー

            Image fragImg = frag.GetComponent<Image>();

            fragImg.color = targetImage.color;

            // 散らばる先（横方向ランダム + 下方向）

            Vector2 scatter = basePos + new Vector2(

                Random.Range(-scatterRadius, scatterRadius),

                Random.Range(-scatterRadius, scatterRadius) - fallDistance

            );

            // コルーチンで移動開始

            StartCoroutine(ScatterAndFall(fragRT, scatter));

        }

    }

    IEnumerator ScatterAndFall(RectTransform frag, Vector2 targetPos)

    {

        Vector2 startPos = frag.anchoredPosition;

        float t = 0f;

        while (t < 1f)

        {

            t += Time.deltaTime * scatterSpeed;

            frag.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            yield return null;

        }

    }

}
