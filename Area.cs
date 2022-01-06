using UnityEngine;

public class Area : MonoBehaviour
{

    [SerializeField] private float destroyDistance;
    private AreaSpawner areaSpawner;
    private Transform playerTransform;

    [SerializeField]
    private int areaIndex;

    public void Setup(AreaSpawner areaSpawner, Transform playerTransform, int areaIndex) {
        this.areaSpawner = areaSpawner;
        this.playerTransform = playerTransform;
        this.areaIndex = areaIndex;

        Debug.Log("캐릭터 : " + playerTransform.position.x + " 맵 : " + this.transform.position.x);
    }

    private void Update() {
        //캐릭터가 x 증가 방향으로 이동하는 경우 뒤의 구역을 제거 (뒤의 구역 기준)
            if(playerTransform.localScale.x >= 0 && playerTransform.position.x - transform.position.x >= destroyDistance) { //당연히 >0
                areaSpawner.SpawnAreaRight(areaIndex);

                Destroy(gameObject);
            }
         else 
            if(playerTransform.localScale.x <= 0 && playerTransform.position.x - transform.position.x <= -1f * destroyDistance) { //당연히 < 0
                areaSpawner.SpawnAreaLeft(areaIndex);

                Destroy(gameObject);
            }
        
    }

}

