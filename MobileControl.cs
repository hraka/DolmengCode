
using UnityEngine;
using TMPro;

public class MobileControl : MonoBehaviour
{
    [Header("Debug Test")]
    [SerializeField]
    private TextMeshProUGUI textTouch;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("끗");
            Application.Quit();
        }

        OnSingleTouch();
        
    }

    private void OnSingleTouch() {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                textTouch.text = "Touch Begin";
            }

            if (touch.phase == TouchPhase.Ended) {
                textTouch.text = "Touch End";
            }

            
        }
    }
}
