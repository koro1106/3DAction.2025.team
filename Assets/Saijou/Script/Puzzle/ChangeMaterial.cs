using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public string stageID;
    public Renderer doorRenderer;
    public Material normalMat;
    public Material clearMat;
    void Start()
    {
        //　保存したクリア情報を確認
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            doorRenderer.material = clearMat;//マテリアル変更
        }
    }
}
