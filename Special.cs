using UnityEngine;

public class Special : MonoBehaviour
{
    void Start()
    {
        Invoke("Change", 2);
    }

    private void Change() {
        gameObject.layer = 9;
    }
}
