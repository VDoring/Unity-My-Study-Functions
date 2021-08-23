// https://www.youtube.com/watch?v=nhmc1z9yh0c
// 캐릭터에 맞춰 카메라 움직이기

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target; //플레이어나 기타 움직이는 요소를 타겟으로 정할 수 있다.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LAteUpdate는 Update함수가 실행되고 난 다음에 실행하는 함수이다.
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
