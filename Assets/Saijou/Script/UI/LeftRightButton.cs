using UnityEngine;

public class LeftRightButton : MonoBehaviour
{
    public GameObject stage123;
    public GameObject stage456;
    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject rightLine;
    public GameObject leftLine;
    [SerializeField] private SEManager seManager;//SE

    public void OnRightButoon()
    {
        seManager.ClickUISE();//SE

        rightButton.SetActive(false);
        stage123.SetActive(false);

        rightLine.SetActive(false);
        leftLine.SetActive(true);

        leftButton.SetActive(true);
        stage456.SetActive(true);
    }
    public void OnLeftButoon()
    {
        seManager.ClickUISE();//SE

        leftButton.SetActive(false);
        stage456.SetActive(false);

        leftLine.SetActive(false);
        rightLine.SetActive(true);

        rightButton.SetActive(true);
        stage123.SetActive(true);
    }
}
