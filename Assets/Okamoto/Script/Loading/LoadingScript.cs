using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Text nowLoading;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("displayNowLoading"); // �R���[�`���Ăяo��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator displayNowLoading()
    { // ������NowLoading�\���ƃA�j���[�V����
        while (true)
        {
            nowLoading.text = "NowLoading";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading.";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading..";
            yield return new WaitForSeconds(0.5f);
            nowLoading.text = "NowLoading...";
            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
}
