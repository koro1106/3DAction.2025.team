using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    public string stageID;
    void Start()
    {
        gameObject.SetActive(true);
        //　保存したクリア情報を確認
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
