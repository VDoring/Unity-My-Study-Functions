using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour // 대화 데이터를 관리(Manage)하는것이 목적인 파일.
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr; // 초상화 스프라이트를 저장할 배열

    void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // 데이터 넣을 공간 만듬. 이때 string[] 배열인 이유는 여러 문장이 들어갈 것이기 때문이다.
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // 대화 데이터
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳은 처음이지?:1", "앞으로 친하게 지내자!:2" }); // 여자아이
        talkData.Add(2000, new string[] { "어서와.:0", "이 곳은 아주 평화로운 곳이지.:0", "사람도 없고, 건물은 여기 있는 집 뿐이야.:0", "너가 여기서 뭘 하든 상관없지만, 옆에 보이는 애 만큼은 건들지 마.:1", "왜냐고?:0", "내 주인님이거든!:2" }); // 남자아이
        talkData.Add(100, new string[] { "주변의 나무로 만들어진 평범한 나무상자다." }); // 나무상자
        talkData.Add(200, new string[] { "고도의 술식이 그려진 종이가 올려져 있는 책상이다." }); // 책상

        // 퀘스트 대화
        talkData.Add(10 + 2000, new string[] { "처음 보는군. 넌 누구지?:0", "우리 주인님에게 입주 허가를 받지 않는 이상..:1", "넌 여기에 올 수 없어.:0",});
        talkData.Add(11 + 1000, new string[] { "안녕? 만나서 반가워.:0", "다른 곳에서 온 사람 같은데..:1", "일단 먼저 나와 계약을 해줘야겠어.:2", "이 마을에서 지내고 싶다면 말이야.:3","우후훗..:2","(저 사람의 마음을 돌릴 수단을 찾아야겠어.):2", "(주변을 둘러보면서 값 나갈 것 같은 물건을 구해보자.):2", });

        talkData.Add(20 + 2000, new string[] { "가치 있는 물건이라..:1", "혹시 모르지. 상자나 가구 주변에 값 나가는 물건이 있을지.:0", });
        talkData.Add(20 + 1000, new string[] { "뭐해?:3", "할 거 없으면 내 신발이나 핥지 그래?:3", });
        talkData.Add(20 + 5000, new string[] { "비싸보이는 보석을 찾았다.", });
        talkData.Add(21 + 5000, new string[] { "오? 이건.. 상당히 귀한 보석 중 하나인 \"비기너즈펄\"이군.:1", "훌륭하군. 더더욱 널 가지고 놀고 싶어졌어.:2", });

        portraitData.Add(1000 + 0, portraitArr[0]); // 여자아이 기본 모습
        portraitData.Add(1000 + 1, portraitArr[1]); // 여자아이 대화 모습
        portraitData.Add(1000 + 2, portraitArr[2]); // 여자아이 웃는 모습
        portraitData.Add(1000 + 3, portraitArr[3]); // 여자아이 화난 모습
        portraitData.Add(2000 + 0, portraitArr[4]); // 남자아이 기본 모습
        portraitData.Add(2000 + 1, portraitArr[5]); // 남자아이 대화 모습
        portraitData.Add(2000 + 2, portraitArr[6]); // 남자아이 웃는 모습
        portraitData.Add(2000 + 3, portraitArr[7]); // 남자아이 화난 모습
    }

    //지정된 대화 문장을 반환하는 함수.
    //매개변수: id=오브젝트ID, talkIndex=대화번호(몇번째 대화인가?)
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length) // 현재 대화 갯수가 전체 대화의 개수와 같다면(모든 대화를 했다면)
            return null; // 대화가 끝남을 표시.
        else
            return talkData[id][talkIndex]; // 대화 출력
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
