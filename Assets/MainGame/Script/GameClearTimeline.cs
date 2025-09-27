using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class G : MonoBehaviour
{
    public Image targetImage;

    [Header("��������")]
    public float breakY = 200f;
    public int partsCount = 20; // ���ɕ���邩

    [Header("�����p�[�c�̃v���n�u���")]
    public GameObject[] partPrefabs;

    [Header("�t�F�[�h�ݒ�")]
    public float fadeDuration = 0.5f; // �t�F�[�h���ԁi�b�j

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
        // ���������p�[�c���i�[
        var partsList = new System.Collections.Generic.List<Image>();

        RectTransform targetRect = targetImage.rectTransform;

        for (int i = 0; i < partsCount; i++)
        {
            GameObject prefab = partPrefabs[Random.Range(0, partPrefabs.Length)];
            GameObject part = Instantiate(prefab, targetRect.parent);

            RectTransform rt = part.GetComponent<RectTransform>();
            Image img = part.GetComponent<Image>();

            // ��������X�^�[�g
            Color c = img.color;
            c.a = 0f;
            img.color = c;

            // ���摜�̈ʒu���班�������_��
            rt.anchoredPosition = targetRect.anchoredPosition + new Vector2(
                Random.Range(-targetRect.sizeDelta.x / 4f, targetRect.sizeDelta.x / 4f),
                Random.Range(-targetRect.sizeDelta.y / 4f, targetRect.sizeDelta.y / 4f)
            );

            // ������
            Vector2 scatterPos = rt.anchoredPosition + new Vector2(
                Random.Range(-100f, 100f),
                Random.Range(-100f, -100f)
            );

            partsList.Add(img);
            StartCoroutine(ScatterPart(rt, scatterPos, img));
        }

        // �t�F�[�h�A�E�g�E�C��
        float t = 0f;
        Color originalColor = targetImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);

            // ���摜�͂��񂾂񓧖���
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - alpha);

            // �p�[�c�͂��񂾂�\��
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

        // �Ō�ɉ摜�����S�ɏ���
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