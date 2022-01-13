using UnityEngine;

public class ObjectSpawnerInArea : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prefabArray;

    void Update()
    {
        if(Input.GetKeyDown("`")) {
            MakeObject();
        }
        
    }

    public void MakeObject() {
        int dolCount = Random.Range(2,5);
        for (int i = 0; i < dolCount; ++i) {
            int index = Random.Range(0, prefabArray.Length);
            float rangeX = Random.Range(-5f, 5f);
            float rangeY = Random.Range(1.7f, 3f); //돌이 겹치지 않으려면 어떻게 하지? 일단 위아래 범위 차이를 두었다
            Vector3 position = new Vector3(rangeX, rangeY, 0);
            
            GameObject dol = Instantiate(prefabArray[index], position, Quaternion.identity);
            dol.transform.SetParent(this.transform, false);
        }
    }

}
