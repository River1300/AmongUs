using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 단순히 설정 정보를 저장하고 관리하기 위한 데이터 클래스로 사용되며, 
//      => MonoBehaviour 와는 독립적으로 사용된다. 
//      => 이런 데이터 클래스들은 주로 설정 값, 상태 정보, 게임 데이터, 
//      => 플레이어 정보 등을 저장하고 전달하기 위해 사용된다.

public enum EControlType
{
    Mouse,
    KeyboardMouse
}

public class PlayerSetting
{
    // 다른 스크립트에서 게임의 설정을 변경할 필요가 있다. 
    //  => 정적 변수를 통해 한 곳에서 설정을 변경하면 다른 스크립트에서 업데이트된 설정을 읽을 수 있다.
    public static EControlType controlType;
    // 방을 만들때 입력 받은 텍스트를 저장해 두었다가 만들어진 방에 전달한다.
    public static string nickname;
}
