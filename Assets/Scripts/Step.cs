/*
#1. 메인 메뉴 만들기 1

    1. UI 해상도 대응
        a. Canvas Scaler는 주로 다양한 디바이스 화면 및 해상도에 대응하기 위해 UI 크기 및 배치를 조정
        b. Constant Pixel Size( 고정 픽셀 크기 ) : UI 의 크기를 고정된 픽셀 크기로 유지하며, 
            => 화면 크기에 관계없이 항상 동일한 크기로 나타낸다.
        d. Scale With Screen Size( 화면 크기에 따라 크기 조절 ) : 
            => UI 의 크기를 화면 크기에 따라 조절하며, 
            => 다양한 화면 크기와 해상도에 대응하기 위해 사용된다. 
            => UI 의 크기는 디바이스의 스크린 크기 및 해상도에 따라 상대적으로 조절된다.
    
    2. 설정 UI 기능 만들기
        a. 플레이어의 현재 설정을 저장하는 클래스( PlayerSetting )를 만든다.
            => 사용자의 컨트롤 방식을 정의할 열거형( EControlType )을 만든다.
                {
                    Mouse,
                    KeyboardMouse
                }
            => 열거형을 정적 변수를 만들어 둔다.
                public static EControlType controlType;
        b. Setting UI 를 관리할 클래스( SettingUI )를 만든다.
            => 속성으로 마우스, 키보드 컨트롤 설정 버튼과 애니메이터를 만든다.
            => OnEnable() 함수를 만들고 설정 창이 활성화 될 때
                {
                    1. 플레이어의 설정 정보를 받아와서 그에 따라 버튼 색을 지정한다.
                    switch(PlayerSetting.controlType)
                    2. 저장된 설정 정보가 마우스일 경우, 키보드일 경우
                }
            => 실제 조작 설정 버튼과 연결된 함수( SetControlMode )를 만든다.
                {
                    1. 매개변수를 설정을 저장하는 정적 변수에 배정한다.
                    PlayerSetting.controlType = (EControlType)index;
                    2. OnEnable() 함수와 마찬가지로 버튼 색을 지정
                }
            => 설정 창을 닫는 함수를 만든다.
            => 설정 창을 닫는 코루틴 함수를 만든다.
                {
                    1. 애니메이션을 출력한다.
                    2. 잠시 대기
                    3. 설정 창 비활성화
                    4. 애니메이션 리셋
                }
        c. Setting UI 오브젝트의 자식으로 Background 이미지를 만들고 Button 컴포넌트를 부착한다.
            => 이미지의 알파값을 0으로 조정한다.
            => 설정 창이 떳을 때 설정 창 밖을 클릭하여 설정 창을 빠져나올 수 있도록 닫는 함수를 연결한다.
        d. MainMenu UI 에서 설정 버튼을 눌렀을 때 Setting UI 오브젝트를 띄운다.

    3. 온라인 버튼과 종료 버튼 기능 만들기
        a. 메인 메뉴 UI를 관리할 클래스( MainMenuUI )를 만든다.
            => 온라인 버튼과 연동될 함수를 만든다. public void OnClickOnlineButton()
                => 간단하게 문자를 출력한다.
            => 게임 종료 버튼과 연동될 함수를 만든다. public void OnClickQuitButton()
                {
                    1. 전처리기로 유니티 에디터일 때와 빌드 상태일 때를 구분한다.
                    2. 유니티 에디터일 때
                    UnityEditor.EditorApplication.isPlayer = false;
                    3. 빌드일 때
                    Application.Quit();
                }

SerializeField : Unity 엔진에서 사용되는 C# 속성 또는 필드 앞에 붙여 사용되는 어트리뷰트다. 
이는 해당 속성이나 필드가 인스펙터뷰에서 직렬화되어 노출될 수 있도록 지정하는 역할을 한다.
*/