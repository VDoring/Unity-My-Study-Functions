using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; //퀘스트 ID 
    public int questActionIndex; // 퀘스트 대화순서 변수 생성
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList; //퀘스트 데이터를 저장할 Dictionary

    void Awake()
    {
        questList = new Dictionary<int, QuestData>(); // 초기화
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("새로운 마을. 갑작스런 상하관계 요구."
                                        , new int[] { 2000, 1000 })); // 퀘스트 만들기. int[]에는 해당 퀘스트와 연관된 NPC id를 입력한다.

        questList.Add(10, new QuestData("뇌물 찾기."
                                        , new int[] { 5000, 2000 })); // 퀘스트 만들기 2.
    }

    //매개변수: id=NPC ID
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id) // 퀘스트 대화순서를 올리는 함수. 대화 진행을 위함.
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    void NextQuest() // 다음 퀘스트
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject() // 퀘스트 오브젝트를 관리할 함수
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2) // 퀘스트번호, 퀘스트 대화순서에 따라 오브젝트를 조절한다.
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
