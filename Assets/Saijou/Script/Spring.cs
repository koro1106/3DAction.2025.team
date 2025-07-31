using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isSpring = false;

    public float springRenge = 2f;
    public Transform player;
    void Start()
    {
        originalScale = transform.localScale;
        originalScale = new Vector3(originalScale.x, 0.3f,originalScale.z);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isSpring)
        {
            float yDistance = Mathf.Abs(transform.position.x - player.position.x);

            if (yDistance <= springRenge)
            {
                //StartCoroutine(Spring());
            }
        }
    }

    //IEnumerator Spring()
    //{
    //    isSpring = true;

    //    yield return new WaitForSeconds(1);
    //}
}
