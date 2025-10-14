using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCameraMove : MonoBehaviour
{
    public Camera mainCamera;
    public float moveDistance = 1800f; //ˆÚ“®‹——£
    public float moveDuration = 1f; //ˆÚ“®‘¬“x(¬‚³‚¢‚Ù‚Ç‘¬‚¢)
    public Button rightButton;
    public Button leftButton;
    void Start()
    {
        rightButton.onClick.AddListener(() => StartCoroutine(MoveCamera(Vector3.right)));
        leftButton.onClick.AddListener(() => StartCoroutine(MoveCamera(Vector3.left)));
    }

    IEnumerator MoveCamera(Vector3 direction)
    {
        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = startPos + direction * moveDistance;
        float elapsed = 0f; //Œo‰ßŽžŠÔ

        while(elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
        mainCamera.transform.position = targetPos;
    }

}
