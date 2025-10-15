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
        //�@�ۑ������N���A�����m�F
        if (PlayerPrefs.GetInt(stageID + "_Cleared", -1) == 1)
        {
            seManager.CancelSE();
            doorRenderer.material = clearMat;//�}�e���A���ύX
        }
    }
}
