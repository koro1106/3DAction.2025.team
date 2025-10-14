using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCompletedImage : MonoBehaviour
{
    public Image completedImage; // �\�����Image
    public Sprite[] completedImages; // ��Փx�ɑΉ������摜

    void Start()
    {
        SetImage();
    }
    void SetImage()
    {
        string sceneName = StageLoader.LastPlayedStageName;

        if (sceneName.StartsWith("MainStage"))
        {
            string numberPart = sceneName.Replace("MainStage", "");

            if (int.TryParse(numberPart, out int parsedDifficulty))
            {
                if (parsedDifficulty >= 0 && parsedDifficulty < completedImages.Length)
                {
                    completedImage.sprite = completedImages[parsedDifficulty];
                    Debug.Log("�V�[��������摜��ݒ�: ��Փx " + parsedDifficulty);
                }
            }
        }
    }
}
