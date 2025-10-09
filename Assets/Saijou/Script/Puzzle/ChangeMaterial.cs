using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public string stageID;
    public Renderer doorRenderer;
    public Material normalMat;
    public Material clearMat;
    [SerializeField] private SEManager seManager; // SE

    void Start()
    {
        //　保存したクリア情報を確認
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            seManager.CancelSE();
            doorRenderer.material = clearMat;//マテリアル変更
        }
    }
}
