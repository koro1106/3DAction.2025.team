using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    public string stageID;
    void Start()
    {
        gameObject.SetActive(true);
        //�@�ۑ������N���A�����m�F
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
