using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public string stageID;
    public Renderer doorRenderer;
    public Material normalMat;
    public Material clearMat;
    void Start()
    {
        //�@�ۑ������N���A�����m�F
        if (PlayerPrefs.GetInt(stageID + "_Cleared", 0) == 1)
        {
            doorRenderer.material = clearMat;//�}�e���A���ύX
        }
    }
}
