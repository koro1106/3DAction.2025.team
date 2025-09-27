using System.Collections;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float jumpForce = 10;
    public float squashScale = 0.5f;
    public float squashDuration = 0.1f;
    public float stretchDuration = 0.1f;

    private Vector3 originalScale;
    private Coroutine squashCoroutine;

    private void Start()
    {
        originalScale = transform.parent.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (squashCoroutine != null) StopCoroutine(squashCoroutine);
            squashCoroutine = StartCoroutine(SquashSpring());
        }
    }
    private IEnumerator SquashSpring()
    {
        Vector3 targetScale = new Vector3(originalScale.x, squashScale, originalScale.z);

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / squashDuration;
            transform.parent.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / stretchDuration;
            transform.parent.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }
        transform.parent.localScale = originalScale; // ”O‚Ì‚½‚ßƒŠƒZƒbƒg
    }
}