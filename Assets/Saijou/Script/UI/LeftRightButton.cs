using UnityEngine;

public class LeftRightButton : MonoBehaviour
{
    
    [SerializeField] private SEManager seManager;//SE

    public void OnRightButoon()
    {
        seManager.ClickUISE();//SE

        
    }
    public void OnLeftButoon()
    {
        seManager.ClickUISE();//SE

        
    }
}
