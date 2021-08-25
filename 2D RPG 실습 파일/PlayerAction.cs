using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;

    public GameManager manager;//플레이어에서 매니저 함수를 호출할 수 있게 하는 변수

    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove; //쯔꾸르 방식으로 움직이기 위해서는 x축 또는 y축 하나로만 이동해야한다.
                        // 따라서 x축을 움직일지, y축을 움직일지 결정하는 bool 변수를 선언한다.
    Vector3 dirVec; //현재 캐릭터가 바라보고 있는 방향 값 저장
    GameObject scanObject; //Object가 스캔되었는가 여부
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 움직임을 위한 변수
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // 버튼 눌러짐 여부를 체크한다.
        // (수평(Horizontal), 수직(Vertical)이동 버튼이벤트를 변수로 저장한다.)
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal"); // 왼쪽,오른쪽 방향키를 누를 때 True가 된다.
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");   // 위쪽,아래쪽 방향키를 누를 때 True가 된다.
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");     // 왼쪽,오른쪽 방향키를 땔 때 True가 된다.
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");       // 위쪽,아래쪽 방향키를 땔 때 True가 된다.

        // 수평(Horizontal) 움직임 여부를 체크한다.
        if (hDown) // 왼쪽, 오른쪽 방향키를 눌렀을경우
            isHorizonMove = true;
        else if (vDown) // 위쪽, 아래쪽 방향키를 눌렀을경우
            isHorizonMove = false;
        else if (hUp || vUp) // 아무 방향키나 손을 떼면 현재 x방향값으로 isHorizonMove를 판단하는 코드.
            isHorizonMove = h != 0;

        // 애니메이션
        if(anim.GetInteger("hAxisRaw") != h) // 현재상태의 값과 애니메이션 작동 값이 다를경우. 즉 움직여야할경우. (유니티 애니메이터의 State가 계속 연속으로 (빠르게)실행되면 지정한 애니메이션이 작동할 틈을 주지 못하게 되므로 애니메이션이 재생되지 않는 문제가 발생한다. 따라서 한번 State가 작동했다면 플레이어가 특정 행동을 취하기 전까지는 같은 State를 작동하지 못하게 함으로써 애니메이션을 실행할 수 있게 한다.)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v) // 위와 같음. h와 v의 차이.
        {                                         // 위로 이동중에 아래로 이동하면 애니메이션 VAxis와 y가 달라지니 그 때 애니메이터를 실행하라는 의미이다.
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else // 이전상태와 현재상태가 변하지 않았을경우
            anim.SetBool("isChange", false);

        // Direction
        //(여기서는 상하좌우 순서로 진행됨. 특별한 이유는 없음)
        if (vDown && v == 1) // 위일때
            dirVec = Vector3.up;
        else if (vDown && v == -1) // 아래일때
            dirVec = Vector3.down;
        else if (hDown && h == -1) // 왼쪽일때
            dirVec = Vector3.left;
        else if (hDown && h == 1) // 오른쪽일때
            dirVec = Vector3.right;

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null) // Jump는 스페이스바와 동일
        {
            //Debug.Log("This is :" + scanObject.name); 디버그용
            manager.Action(scanObject);
        }

    }

    void FixedUpdate()
    {
        // 움직이기.
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        //Debug.Log(rayHit);

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}
