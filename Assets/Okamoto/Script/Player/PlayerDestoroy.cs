using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDestoroy : MonoBehaviour
{
   public  IEnumerator LoadSelectionScene()
   {
        Debug.Log("�R���[�`���Ă΂ꂽ");
        yield return new WaitForSeconds(1f);//1�b�҂�

        SceneManager.LoadScene("GameOverScene");//�I����ʂɑJ��
   }
}
