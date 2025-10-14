using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCompletedImage : MonoBehaviour
{
    public Image completedImage; // 表示先のImage
    public Sprite[] completedImages; // 難易度に対応した画像

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
                    Debug.Log("シーン名から画像を設定: 難易度 " + parsedDifficulty);
                }
            }
        }
    }
}
