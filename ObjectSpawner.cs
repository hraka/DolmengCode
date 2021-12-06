using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prefabArray;
    private Vector3 offset;
    // Start is called before the first frame update

    void Start() {
        for (int i = 0; i < 15; ++i) {
            int index = Random.Range(0, prefabArray.Length);
            float range = Random.Range(3f, 7f);
            Vector3 position = new Vector3(-40.5f + (range * i), 0, 0);
            

            Instantiate(prefabArray[index], position, Quaternion.identity);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("`")) {
            Vector3 camPos = Camera.main.transform.position;
            Debug.Log("카메라 위치는 " + camPos.x);
            offset = new Vector3(camPos.x, 0, 0);
            MakeObject();
        }
        
    }

    private void MakeObject() {
        for (int i = 0; i < 10; ++i) {
            int index = Random.Range(0, prefabArray.Length);
            Vector3 position = new Vector3(-4.5f + (2 * i), 0, 0) + offset;
            

            Instantiate(prefabArray[index], position, Quaternion.identity);
        }
    }

}
