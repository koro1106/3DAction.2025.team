using UnityEngine;

using UnityEngine.UI;

using System.Collections;

public class UIImageBreaker : MonoBehaviour

{

    [Header("�Ώۂ�Image")]

    public Image targetImage;

    [Header("�j��Prefab�iUI Image�j")]

    public GameObject fragmentPrefab;

    [Header("�j��ݒ�")]

    public int fragments = 12;       // ���̔j�Ђɂ��邩

    public float scatterRadius = 50f; // �U��΂�͈�

    public float fallDistance = 200f; // ���ɗ����鋗��

    public float delayBeforeBreak = 2f; // �j��܂ł̕b��

    public float scatterSpeed = 1f;   // �U��΂�X�s�[�h

    private bool broken = false;

    void Start()

    {

        // �w��b����ɔj�󏈗����J�n

        Invoke(nameof(BreakImage), delayBeforeBreak);

    }

    void BreakImage()

    {

        if (broken) return;

        broken = true;

        // ����Image���\��

        targetImage.enabled = false;

        // RectTransform�̒��S�ʒu

        RectTransform rt = targetImage.rectTransform;

        Vector2 basePos = rt.anchoredPosition;

        // �j�Ђ𐶐�

        for (int i = 0; i < fragments; i++)

        {

            GameObject frag = Instantiate(fragmentPrefab, rt.parent);

            RectTransform fragRT = frag.GetComponent<RectTransform>();

            // �����ʒu�͌���Image�̒��S�t��

            fragRT.anchoredPosition = basePos;

            // �j�ЃT�C�Y�𒲐��i����������j

            fragRT.sizeDelta = rt.sizeDelta / 4f;

            // �F���R�s�[

            Image fragImg = frag.GetComponent<Image>();

            fragImg.color = targetImage.color;

            // �U��΂��i�����������_�� + �������j

            Vector2 scatter = basePos + new Vector2(

                Random.Range(-scatterRadius, scatterRadius),

                Random.Range(-scatterRadius, scatterRadius) - fallDistance

            );

            // �R���[�`���ňړ��J�n

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
