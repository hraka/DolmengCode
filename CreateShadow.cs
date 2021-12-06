
using UnityEngine;

public class CreateShadow : MonoBehaviour
{

    [SerializeField]
    GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        // SpriteRenderer ar = a.GetComponent<SpriteRenderer>();
        // GameObject shadow = new GameObject();
        // shadow.name = "그림자";
        // shadow.transform.localPosition = a.transform.localPosition;
        // SpriteRenderer shadowRenderer = shadow.AddComponent<SpriteRenderer>();
        // shadowRenderer.color = new Color(0, 0, 1, 0);
        // shadowRenderer.sprite = ar.sprite;


        GameObject shadow = Instantiate(a, a.transform.position, a.transform.rotation);
        shadow.name = "그림자";
        // shadow.Destory(shadow.GetComponent<)
        // SpriteRenderer shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        // shadowRenderer.color = new Color(0, 0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
