
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] areaPrefabs;
    [SerializeField] private int spawnAreaCountStart = 3;

    [SerializeField] private float xDistance = 20; //구역 사이의 거리

    [SerializeField] private float ySet = 0; //구역 사이의 거리

    
    [SerializeField] private float size = 0.5f;

    [SerializeField]
    private Transform playerTransform;
    private int areaIndex = 0;


    private void Awake() {
        for(int i = 0; i < spawnAreaCountStart; ++i) {
            if(i == 0) {
                SpawnArea();
            }
            else {
                SpawnArea();
            }
        }
    }

    public void SpawnArea() {
        GameObject clone = null;

        clone = Instantiate(areaPrefabs[0]);

        clone.transform.position = new Vector3(areaIndex * xDistance * size, ySet, 0);
        clone.transform.localScale = new Vector3(size, size, 1f);

        clone.GetComponent<Area>().Setup(this, playerTransform);

        areaIndex ++;

        //실시간 생성이 아니라 미리 생성해두고 그걸 하나씩 꺼내와도 좋겠다.
    }
}
