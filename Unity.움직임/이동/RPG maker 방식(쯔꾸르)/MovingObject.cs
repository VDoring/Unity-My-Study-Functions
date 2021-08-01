using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float character_speed; // 기본 속도

    private Vector3 vector; // 3개의 값을 동시에 가지고 있는 변수

    public float runSpeed; // 달리기 상태의 속도
    public float applyRunSpeed; // 달리기 키를 누를 경우에만 적용되게 하는 변수.
    private bool applyRunFlag = false;

    // 쯔꾸르 게임에선 픽셀을 기준으로 움직인다. 따라서 픽셀 단위로 움직이게 하기위한 변수를 추가한다.
    public int walkCount; //
    private int currentWalkCount;

    // 코루틴이 컴퓨터의 빠른 연산으로 인해 여러개 실행되지않게 방지하기위한 변수
    private bool canMove = true; // (움직일 수 있는가에 대한 여부 설정으로 예상)


    // Start is called before the first frame update
    // 프레임 시작 전에 실행할 코드를 모아두는 함수
    void Start()
    {
    }

    IEnumerator MoveCoroutine() //이것을 코루틴이라고 한다. 함수랑 코루틴이 동시에 처리되도록 하는 것이 가능하다. 일종의 다중처리 시스템인 셈.(하지만 완전한 다중처리 시스템은 유니티에서 지원하지 않는다. 다중처리로 보이게 하는 것 뿐.)
    {
        if (Input.GetKey(KeyCode.LeftShift)) //LEftShift 키를 눌렀을경우
        {
            applyRunSpeed = runSpeed; // 달리기 속도 적용
            applyRunFlag = true;
        }
        else
        {
            applyRunSpeed = 0; // 달리기 속도 미적용
            applyRunFlag = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); // x값, y값, z값을 vector에 저장한다. transform.position.z을 넣은 이유는 z축 값은 이 프로젝트에선 바뀌지 않을것이기 때문이다.


        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0) //vector에 들어있는 x축의 값이 0이 아니라면
                transform.Translate(vector.x * (character_speed + applyRunSpeed), 0, 0); // vector.x * character_speed에서 vector.x의 값은 좌 방향키(-1) 또는 우 방향키(1)만 리턴되므로 -1 * character_speed 또는 1 * character_speed가 된다.
            else if (vector.y != 0)
                transform.Translate(0, vector.y * (character_speed + applyRunSpeed), 0); // 이번엔 y좌표를 바꾸는 것이므로 (0, y, 0)의 형식을 사용한다.

            if (applyRunFlag) // L-Shift를 누른 상태일시
                currentWalkCount++; //밑에꺼까지 합해서 2씩 증가(2 이동)
            currentWalkCount++; //L-Shift를 누르지 않은 상태일시 1씩 증가(1 이동)

            yield return new WaitForSeconds(0.01f); // 0.01초동안 대기한다.
        }
        currentWalkCount = 0;
        canMove = true;
    }

    // Update is called once per frame.
    // 한 프레임마다 실행할 코드를 모아두는 함수
    void Update()
    {
        if (canMove) // 코루틴이 컴퓨터의 빠른 연산으로 인해 여러개 실행되지않게 방지
        {
            //Input.GetAxisRaw("Horizontal")
            //'오른쪽' 방향키가 눌리면 '1'을 리턴하고, '왼쪽' 방향키가 눌리면 '-1'을 리턴한다.
            //Input.GetAxisRaw("Vertical")
            //'위쪽' 방향키가 눌리면 '1'을 리턴하고, '아래쪽' 방향키가 눌리면 '-1'을 리턴한다.
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // 방향키가 하나라도 눌러졌을경우
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}