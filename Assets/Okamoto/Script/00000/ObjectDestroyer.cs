using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // 破片プレハブ
    public GameObject fragmentPrefab;
    // 破壊後の破片数
    public int fragmentCount = 100;
    // 破壊の衝撃力
    public float force = 500f;

    // 破壊のトリガーとなるメソッド
    public void DestroyObject()
    {
        // オブジェクトを非表示にする（物理演算に影響しないようにする）
        gameObject.SetActive(false);

        // 破片を生成して飛び散らせる
        for (int i = 0; i < fragmentCount; i++)
        {
            // 破片の位置をランダムに決める
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 0.5f;
            // 破片の生成
            GameObject fragment = Instantiate(fragmentPrefab, randomPosition, Random.rotation);

            // 破片に物理力を加える
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Random.insideUnitSphere * force);
            }
        }

        // オブジェクトを破壊（完全に削除）
        Destroy(gameObject);
    }

    // Updateメソッドでスペースキーを押したときに破壊を実行
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // スペースキーで破壊
        {
            DestroyObject();
        }
    }
}
