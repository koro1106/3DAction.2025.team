using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spring : MonoBehaviour
{
   public float jumpForce = 10;//íµÇÀÇÈã≠Ç≥
   

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);//àÍíUYë¨ìxÇÉ[ÉçÇ…
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
