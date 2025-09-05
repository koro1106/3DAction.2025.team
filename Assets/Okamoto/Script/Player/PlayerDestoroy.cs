using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDestoroy : MonoBehaviour
{
   public  IEnumerator LoadSelectionScene()
   {
        Debug.Log("コルーチン呼ばれた");
        yield return new WaitForSeconds(1f);//1秒待つ

        SceneManager.LoadScene("GameOverScene");//選択画面に遷移
   }
}
