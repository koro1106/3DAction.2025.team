using UnityEngine;

public class PuzzleWall : MonoBehaviour
{
    public string stageID;
    void Start()
    {
        //�@�ۑ������N���A�����m�F
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
