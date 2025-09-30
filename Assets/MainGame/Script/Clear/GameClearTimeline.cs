using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class G : MonoBehaviour
{
    public Image targetImage;

    [Header("崩れる条件")]
    public float breakY = 200f;
    public int partsCount = 20; // 何個に崩れるか

    [Header("崩れるパーツのプレハブ候補")]
    public GameObject[] partPrefabs;

    [Header("フェード設定")]
    public float fadeDuration = 0.5f; // フェード時間（秒）

    private bool broken = false;

    void Update()
    {
        if (!broken && targetImage.rectTransform.anchoredPosition.y <= breakY)
        {
            StartCoroutine(BreakIntoPartsSmooth());
            broken = true;
        }
    }

    IEnumerator BreakIntoPartsSmooth()
    {
        // 生成したパーツを格納
        var partsList = new System.Collections.Generic.List<Image>();

        RectTransform targetRect = targetImage.rectTransform;

        for (int i = 0; i < partsCount; i++)
        {
            GameObject prefab = partPrefabs[Random.Range(0, partPrefabs.Length)];
            GameObject part = Instantiate(prefab, targetRect.parent);

            RectTransform rt = part.GetComponent<RectTransform>();
            Image img = part.GetComponent<Image>();

            // 透明からスタート
            Color c = img.color;
            c.a = 0f;
            img.color = c;

            // 元画像の位置から少しランダム
            rt.anchoredPosition = targetRect.anchoredPosition + new Vector2(
                Random.Range(-targetRect.sizeDelta.x / 4f, targetRect.sizeDelta.x / 4f),
                Random.Range(-targetRect.sizeDelta.y / 4f, targetRect.sizeDelta.y / 4f)
            );

            // 崩れる先
            Vector2 scatterPos = rt.anchoredPosition + new Vector2(
                Random.Range(-100f, 100f),
                Random.Range(-100f, -100f)
            );

            partsList.Add(img);
            StartCoroutine(ScatterPart(rt, scatterPos, img));
        }

        // フェードアウト・イン
        float t = 0f;
        Color originalColor = targetImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);

            // 元画像はだんだん透明に
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - alpha);

            // パーツはだんだん表示
            foreach (var img in partsList)
            {
                if (img != null)
                {
                    Color c = img.color;
                    c.a = alpha;
                    img.color = c;
                }
            }

            yield return null;
        }

        // 最後に画像を完全に消す
        targetImage.enabled = false;
    }

    IEnumerator ScatterPart(RectTransform rt, Vector2 targetPos, Image img)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));

        Vector2 startPos = rt.anchoredPosition;
        Quaternion startRot = rt.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, Random.Range(-60f, 60f));

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            rt.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            rt.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }
    }
}