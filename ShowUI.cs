using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uis;


    public void SwapUIView() {
        for(int i = 0; i < uis.Length; i++) {
            uis[i].SetActive(!uis[i].activeSelf);
        }
    }

}
