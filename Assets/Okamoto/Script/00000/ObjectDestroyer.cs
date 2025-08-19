using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // �j�Ѓv���n�u
    public GameObject fragmentPrefab;
    // �j���̔j�А�
    public int fragmentCount = 100;
    // �j��̏Ռ���
    public float force = 500f;

    // �j��̃g���K�[�ƂȂ郁�\�b�h
    public void DestroyObject()
    {
        // �I�u�W�F�N�g���\���ɂ���i�������Z�ɉe�����Ȃ��悤�ɂ���j
        gameObject.SetActive(false);

        // �j�Ђ𐶐����Ĕ�юU�点��
        for (int i = 0; i < fragmentCount; i++)
        {
            // �j�Ђ̈ʒu�������_���Ɍ��߂�
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 0.5f;
            // �j�Ђ̐���
            GameObject fragment = Instantiate(fragmentPrefab, randomPosition, Random.rotation);

            // �j�Ђɕ����͂�������
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Random.insideUnitSphere * force);
            }
        }

        // �I�u�W�F�N�g��j��i���S�ɍ폜�j
        Destroy(gameObject);
    }

    // Update���\�b�h�ŃX�y�[�X�L�[���������Ƃ��ɔj������s
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �X�y�[�X�L�[�Ŕj��
        {
            DestroyObject();
        }
    }
}
