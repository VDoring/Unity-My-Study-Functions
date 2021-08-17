using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel; // 대화창 표시 유무
    public Image portraitImg; // 초상화
    public Text talkText; // 대화창 텍스트
    public GameObject scanObject; //플레이어가 스캔한 오브젝트
    public bool isAction; // 상태 저장용 변수
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex); // 퀘스트번호 + NPC ID -> 퀘스트 대화 데이터 ID

        if(talkData == null) // 대화가 끝난게 확인된 경우
        {
            isAction = false;
            talkIndex = 0; // 대화가 끝났으므로 대화 카운트도 초기화한다.
            questManager.CheckQuest(id);
            return;
        }

        // NPC와 NPC가 아닌 경우를 구분.
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0]; // 메세지 출력에 구분자(:)를 포함시키지 않기 하기 위해 :을 기준으로 문자열을 나눈다. 이때 나눠진 두 문자열은 string 배열이 되어 저장된다. 저기서 바로 출력하게 하기 위해서는 Split() 뒤에 바로 배열 인덱스 번호를 지정하는 것이다.

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // Parse()는 문자열을 해당 타입으로 변환해주는 함수이다. 여기서는 int이다.
            portraitImg.color = new Color(1, 1, 1, 1); // NPC일 때 Image가 보인다.
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);  // NPC가 아닐 때 Image가 안 보인다.
        }

        isAction = true;
        talkIndex++; // 다음 문장을 출력하기 위해 +1;
    }
}
