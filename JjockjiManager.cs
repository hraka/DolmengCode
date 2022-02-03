
using System.Collections.Generic;
using UnityEngine;

public class JjockjiManager : MonoBehaviour
{

    Dictionary<int, string[]> contentData;

    void Awake() {
        contentData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData() {
        contentData.Add(3, new string[] {"쪽지를 발견했다. \n그래 바로 이거. \n이것은 방금 발견한 쪽지다.", "쪽지... 어라 글이 어디갔지?! 내가 지웠다."});
        contentData.Add(4, new string[] {"c를 누르면 돌을 던질 수 있습니다."});
    }

    public string GetContent(int id, int contentIndex) {
        if(contentIndex >= contentData[id].Length) {
            contentIndex = 0;
        }
        string jjockjiData = contentData[id][contentIndex];
        return jjockjiData;
    }

}
