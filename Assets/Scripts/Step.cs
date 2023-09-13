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

/*
#2. 메인 메뉴 만들기 2

    1. 별 파티클 만들기
        a. 파티클 오브젝트를 만들고 화면 왼쪽에서 오른쪽으로 별이 이동하는 이펙트 생성
        b. 파티클 컴포넌트의 Prewarm 프로퍼티를 체크하여 파티클 생성 위치를 자연 스럽게 배치

    2. 플레이어 색상 셰이더 만들기
        a. RGB 색상으로만 구성된 스프라이트가 쥐어졌을 때
            => 이 스프라이트에서 R 채널, G 채널, B 채널을 따로 분리하여 스프라이트를 3 부분으로 분리한다.
            => R 은 메인 색상, B 는 메인 색상의 그림자 색상, G 는 유리 색상으로 분리한다.
        b. 분리 작업을 위해 셰이더를 작성한다.
            => 셰이더 그래프( Sprite Lit Graph )를 만든다.
        c. 셰이더 그래프에서, 먼저 필요한 외부 프로퍼티를 두 개 선언한다.
            => Texture2D 프로퍼티를 추가하고 _MainTex라 명명
            => _MainTex라 명명할 경우 Renderer 계열의 컴포넌트에서 사용하는 텍스쳐를 자동으로 가져온다.
                => Unity 는 Shader Graph 를 사용하여 셰이더를 작성할 때 
                => _MainTex와 같은 네이밍 규칙을 사용하면 머티리얼에서 해당 
                => 프로퍼티에 자동으로 연결되는 기능을 제공한다.
                => 이것은 사용자가 수동으로 머티리얼에 텍스쳐를 할당할 필요 없이 
                => 자동으로 텍스쳐를 적용할 수 있게 해준다.
        d. 그 다음, Color 프로퍼티를 추가하고 _PlayerColor라고 명명
            => Color 프로퍼티의 Default Color 를 잘 보이는 색으로 지정
        e. _MainTex 의 Default 를 그리려는 스프라이트로 지정하고 프로퍼티를 그래프의 빈 영역으로 배치
        f. _MainTex 노드의 포트를 드래그하여 Sample Texture 2D 노드를 배치
            => 텍스쳐의 RGB 채널을 분리할 수 있는 포트들이 나타난다.
        g. R 채널을 끌어서 One Minus 노드를 생성
            => R 채널이 존재하는 부분만 검은색으로 보이는 텍스쳐가 미리보기로 보여진다.
            => 보통 흰색은 있다, 검은색은 없다를 표현하므로 R 채널을 One Minus 시켜준 것을 한 번 더 One Minus 시킨다.
            => 다시 반전시키면 R 채널 부분이 흰 색으로 표현된다.
            => 사실 이 작업은 셰이더 최적화를 위해서는 불필요한 과정이지만 어떻게 원하는 색상을 뽑아내고 계산하는지를 보기 위한 과정이다.
        h. G, B 채널도 동일할 방식으로 색을 분리한다.
        i. _PlayerColor 프로퍼티를 그래프에 배치하고 노드 포트를 끌어서 Multiply 노드( B )를 배치한다.
            => 셰이더 그래프의 Multiply 노드에는 일반적으로 두 개의 입력이 있다. 
            => 이러한 입력을 일반적으로 A와 B로 나타낸다.
                => A : 이 입력은 보통 연산의 왼쪽에 위치하며, 주로 기본 값 또는 원본 값으로 사용된다. 
                => 예를 들어, 텍스쳐의 색상, 오브젝트의 밝기 또는 다른 연산의 결과값이 될 수 있다.
                => B : 이 입력은 일반적으로 연산의 오른쪽에 위치하며, 
                => 주로 연산에 사용할 값 또는 수정할 값으로 사용된다. 
                => 예를 들어, A와 B를 Multiply 노드에 연결하면 A의 각 픽셀에 B를 곱하여 결과를 생성
            => A와 B는 종종 서로 다른 데이터 유형을 가질 수 있으며, 연산의 결과도 달라질 수 있다. 
        j. R 채널과 곱해주어 R 채널이 _PlayerColor 의 색상을 갖게 한다.
        k. _PlayerColor 의 포트를 한 번 더 끌어서 Multiply 노드를 추가하여 0.5를 곱하여 색상을 어둡게 해서 그림자 색상을 만든다.
        l. 이렇게 만든 그림자 색상과 B 채널을 곱해주면 그림자 영역이 완성된다.
        m. G 채널(A)과 B 채널(B)을 Multiply 로 곱해주면 흰 색 영역만 추출해줄 수 있다.
        n. 여기서 흰 영역을 완전히 배제해야 하기 때문에 Multiply 노드로 100 을 곱해서 흰 색 영역을 강화해 준다.
        o. 메인 플레이어 색상 부분에서 헬멧의 흰색 영역을 Subtract 노드로 빼주면 완전히 플레이어 색상 영역만 구해낼 수 있다.
        p. 그림자 영역도 똑같은 작업을 해준다.
        q. 이렇게 구해낸 플레이어 색상 영역과 그림자 영역을 Add 노드로 더해준다.
            => 흰 색 영역에 100 을 곱해서 빼줬기 때문에 유리 부분에 검은 구명이 뚫린 것처럼 보인다.
        r. 더한 노드에서 Clamp 노드를 추가하여 텍스쳐의 가장 어두운 색상을 0, 가장 밝은 색상을 1로 제한
        s. 유리의 영역을 채우기 위해 그래프의 빈 영역에 우클릭 하고 Color 노드를 추가한 뒤 헬멧 유리 색상을 지정
        t. Color 노드와 G 채널 노드를 Multiply 해서 색상을 입혀 준다.
        u. 다시 하얀색의 Color 노드를 추가한 뒤 유리의 흰 색 영역과 곱해준 뒤 유리 위에 더한다.
        v. 앞에서 구해둔 플레이어 색상 노드와 유리를 더한다.
        w. 알파값을 처리하기 위해 Sample Texture 2D 노드에서 A 채널을 끌어 Vector4 노드의 W 값으로 넣어준 다음 마지막 결과 노드에 더한다.
        x. 이렇게 나온 결과물을 Sprite Lit Master 노드의 Color 포트에 넣어 준다.
        y. One Minus 노드를 제거한다.
        z. 완성된 셰이더를 저장한다.
    3. 떠다니는 크루원 만들기
        a. 플레이어의 색상 정보를 가지고 있는 클래스를 만든다.
            => 캐릭터의 색상 정보를 열거형으로 만든다.
        b. 색상 정보를 저장하고 있는 정적 리스트 속성을 만든다.
            static List<Color> colors = new List<Color>()
            {
                new Color(1f, 0f, 0f),
                new Color(0.1f, 0.0f, 1f),
                ...
            };
        c. 열거형 Color 를 받아와서 리스트의 색상을 반환해 주는 함수를 만든다.
            public static Color GetColor(EPlayerColor playerColor) { return colors[(int)playerColor]; }
        d. 색상 이름으로 리스트의 색상을 반환해 주는 정적 변수를 만든다.
            public static Color Red { get { return colors[(int)EPlayerColor.Red]; } }
            ...
        e. 크루원이 배경을 떠다니게 할 클래스를 만든다. FloatingCrew
        f. 떠다니는 크루원의 스프라이트와 색상을 지정할 스프라이트 렌더러, 방향, 속도, 회전 속도 속성을 갖는다.
        g. 크루원의 색상을 저장할 속성도 만들어 둔다.
            public EPlayerColor playerColor;
        h. 크루원을 초기화 할 함수를 만든다. public void SetFloatingCrew()
            매개변수로 스프라이트, 색상, 방향, 속도, 회전 속도, 크기를 받는다.
            {
                1. 전달받은 매개변수로 속성값을 초기화 한다.
                2. 스프라이트를 받아와서 색상을 바꿔준다.
                spriteRenderer.sprite = sprite;
                spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
                3. 크루원의 크기를 지정한다.
                transform.localScale = new Vector3(size, size, size);
                4. 크루원 크기에 따라 그리는 순서를 지정한다.
                spriteRenderer.sortingOrder = (int)Mathf.Lerf(1, 23767, size);
            }
        i. 매 프레임 마다 크루원을 이동 시키기 위해 Update() 함수를 만든다.
            {
                1. 매 프레임 마다 이동한다.
                transform.position += direction * floatingSpeed * Time.deltaTime;
                2. 매 프레임 마다 회전 한다.
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, rotateSpeed));
            }
        j. 에디터 상에 배치해 두었던 Floating Crew 객체에 스프라이트를 부착하고 리지드바디와 콜라이더를 부착한다.
            콜라이더는 트리거
            리지드바디의 저항력, 중력 0
            객체를 프리팹화 한다.
        k. 크루원이 돌아다닐 공간을 빈 오브젝트로 만든다. Crew Floater
            콜라이더 부착, 트리거, 사이즈를 화면보다 크게
        l. 크루원을 만들 클래스를 만든다. CrewFloater
            프리팹과 스프라이트 배열(크루원 스프라이트들을 담을 리스트)
            색상의 중복이 발생하지 않도록 불리언 배열 12색상, 소환 간격, 중심으로 부터의 간격 속성
        m. 크루원을 소환하는 함수를 만든다. SpawnFloatingCrew()
            매개변수로 색상과 거리를 받는다.
            크루원이 소환될 때 카메라 영역을 벗어나서 원형으로 생성되게 만들어야 한다.
            {
                1. 0 ~ 360 사이에서 랜덤한 값을 받는다.
                float angle = Random.Range(0, 360);
                2. Sin 과 Cos 함수를 이용하여 Vector를 만들면 중심으로 부터 원형의 방향을 돌아가며 가리키는 벡터를 구할 수 있다.
                Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * distance;
                3. 인스턴스화
                var crew = Instantiate(prefab, spawnPos, Quaternion.identity);
            }
        n. Update() 함수에서 소환 함수를 호출한다.
            {
                1. 딜레이에 맞추어 소환을 진행한다.
                timer -= Time.deltaTime;
                SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            }
        o. 다시 소환 함수로 돌아가서 중복되는 색상의 크루원이 있는지 확인한다.
            {
                1. 불리언 배열을 통해 색상 중복 여부를 확인한다.
                if(!crewState[(int)playerColor])
                2. 크루원이 날아갈 방향과 속도, 회전 속도를 랜덤 값으로 지정해 준다.
                3. 크루원에 접근하여 크루원 초기화 함수를 호출한다.
                crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));
            }
        p. 트리거 탈충 함수를 만들어서 크루원이 생성 지점을 벗어날 경우 지워지도록 한다.
        q. 게임 시작부터 크루원이 날아다니도록 하기 위해 Start() 함수에서 플레이어 색상 종류 수 만큼 반복하면서 소환 함수 호출
            {
                for( int i = 0; i < 12; i++ )
                {
                    1. 소환함수를 호출하되 랜덤한 거리로 소환
                    SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance));
                }
            }

Quaternion.Euler() : 이 함수는 주로 3D 공간에서 오브젝트의 회전을 설정할 때 사용
    => Euler 각은 일반적으로 세 개의 각도로 나타내는데, 
    => 이 각도는 오브젝트가 주어진 축 주위로 얼마나 회전하는지를 나타낸다. 

transform.rotation.eulerAngles : 오브젝트의 현재 회전 각도를 나타내는 속성 
    => 오브젝트의 현재 회전 상태를 읽어올 수 있으며, 
    => 이를 통해 오브젝트의 회전을 조작하거나 상태를 확인하는 데 사용할 수 있다.
    => 이 값은 세 개의 Euler 각도로 표현되며, 각각 X, Y, Z 축 주위의 회전 각도를 나타냅니다.
    => 이 값은 Vector3 형식으로 반환되며, 각 구성 요소는 해당 축 주위의 회전 각도를 나타낸다. 

transform.rotation : 오브젝트의 현재 회전을 나타내는 속성 
    => 이것은 해당 게임 오브젝트가 현재 어떤 방향으로 회전되어 있는지를 나타내는 정보를 포함하고 있다.
    => 다른 오브젝트와 상호작용하거나 오브젝트를 회전시킬 때 유용하게 사용된다. 

transform.Rotate() : 함수는 게임 오브젝트의 회전을 변경하는 메서드 
    => 이 함수를 사용하면 오브젝트를 현재의 회전 값에 상대적으로 회전시킬 수 있다. 

transform.Rotate(Vector3 eulerAngles) : Euler 각도(세 개의 축에 대한 회전 각도)를 매개변수로 받는다. 
    => transform.Rotate(0f, 90f, 0f)을 호출하면 오브젝트가 현재 회전값을 기준으로 y 축을 중심으로 90도 회전한다.
transform.Rotate(Vector3 axis, float angle) : 회전을 수행할 축과 회전 각도를 지정한다. 
    => 축은 원하는 회전 방향을 나타내는 단위 벡터로 지정하며, 
    => 회전 각도는 해당 축을 중심으로 시계 방향( 양수 ) 또는 반시계 방향( 음수 )으로 지정한다.
transform.Rotate(Vector3 eulerAngles, Space relativeTo) : 상대적인 회전을 적용하는데, 
    => Space 매개변수를 사용하여 어느 공간에서 상대적인 회전을 할지 지정한다. 
    => 일반적으로 Space.Self를 사용하면 현재 오브젝트의 로컬 축을 기준으로 회전하고, 
    => Space.World를 사용하면 월드 좌표 공간을 기준으로 회전합니다.

transform.Rotate(): 현재 오브젝트의 회전 값을 변경한다.
    => 주어진 각도만큼 현재 회전값에 누적적으로 회전을 적용한다.
    => 기본적으로 로컬 축을 기준으로 회전한다.
    => 상대적인 회전을 수행할 때 사용된다. 
    => 예를 들어, 현재 회전값을 기준으로 일정 각도만큼 회전하거나, 
        => 특정 축을 중심으로 회전하는 데 사용된다.
Quaternion.Euler(): Euler 각도를 기반으로 새로운 Quaternion을 생성한다.
    => Euler 각도는 피치, 롤 및 요(또는 x, y, z) 회전 값을 사용하여 표현된다.
    => 새로운 Quaternion을 생성하므로, 현재의 회전값을 변경하지 않고 새로운 회전값을 지정한다.
    => 주로 원하는 회전 값을 직접 지정하여 새로운 회전값을 만들 때 사용된다.

차이점을 요약하면, transform.Rotate()는 현재 오브젝트의 회전 값을 변경하고 누적 회전을 수행하며, 
상대적인 회전을 수행할 때 사용된다. 반면에 Quaternion.Euler()는 새로운 회전값을 생성하며, 
현재 회전값을 변경하지 않는다. 이 함수를 사용하여 정확한 회전 값을 만들고 적용할 때 사용된다.

transform.rotation = Quaternion.Euler(0f, 90f, 0f); 이 코드는 매 프레임마다 현재 오브젝트의 회전을 (0도, 90도, 0도)로 설정 
    => 따라서 매 프레임마다 오브젝트가 y 축 주위로 90도씩 회전하게 된다. 
    => 이는 회전이 누적되지 않고 매번 새로운 회전값으로 설정된다.
    => 결과적으로 오브젝트의 회전값은 x: 0, y: 90, z: 0 인 상태가 매 프레임마다 설정된다.
transform.Rotate(0f, 90f, 0f); 이 코드는 매 프레임마다 현재 오브젝트를 현재 회전값을 기준으로 y 축 주위로 90도 회전 
    => 따라서 매 프레임마다 오브젝트가 현재 회전값에 따라 추가적으로 90도씩 회전하게 된다. 
    => 이는 누적 회전을 수행한다. 
*/

/*
#3. 온라인 UI 와 방 만들기 UI 만들기

    1. 온라인 UI 에서 방 만들기 UI 배치
        a. Canvas 의 자식으로 방 만들기 UI 이미지를 만든다.
            => 온라인 버튼 선택 -> 방 만들기 버튼 선택 -> 방 만들기 UI
            => 해상도 변경을 고려하기 위해 앵커를 가득 채운다.
        b. 맵 배너 UI 이미지, 임포스터 수 Text, 최대 인원 수 Text 를 추가한다.
        c. 임포스터 수 선택 버튼과 최대 인원 수 선택 버튼을 만든다.
            => 버튼의 테두리 이미지 알파값을 0으로 조정
            => 버튼들을 상위 빈 오브젝트에 넣고 정렬을 위해 Horizontal Layout Group, Content Size Fitter 컴포넌트를 부착한다.
        d. 기본으로 선택되는 임포스터 수 1과 최대 인원 10의 테두리 이미지의 알파값을 1로 조정
        e. 맵 배너 위에 선택된 최대 인원과 임포스터 수에 따라서 보이는 크루원 이미지 UI를 추가
            => 인원 수 버튼 UI와 마찬가지로 정렬
            => 기존에 만들었던 Sprite Lit Shader Graph 를 넣었던 마테리얼 M_Crew를 넣었을때 크루원 주변에 검은 배경이 생기는 문제가 있다.
            => 버전이 바뀌면서 발생하는 문제로 보여진다.
            => 2D 스프라이트로 만든 크루원은 잘 작동하지만 UI Image로 만든 크루원은 검은 배경이 생긴다.
            => Sprite Lit Shader는 UI에는 제대로 적용이 안되는것으로 보인다.
            => Lit Shader Graph 를 새로 만든다.
            => 기존 쉐이더 그래프에서 만들었던 요소들만 복사붙여넣기 해준다.
            => Lit Shader Graph 에는 Alpha 값을 넣어줄 부분이 안보이기 때문에 Graph Inspector -> Graph Settings -> Alpha Clipping 을 체크해주면 Alpha가 생긴다.
            => 변수들도 연결해준 후 저장 후에 메테리얼에 쉐이더 연결해주고 새로 만든 메테리얼을 넣어준다.
            => 이때 Game view 에서는 문제가 없고, Scene view 에서는 크루 이미지가 모든 다른 UI에 가려져 보이지 않는 문제가 발생한다. 
            => 새로 만든 shader -> Graph Inspector -> Graph Settings -> Surface Type 을 Transparent 로 바꿔준다.

    2. 크루원 이미지 변경 기능 구현
        a. 방 만들기 UI에서 기능을 컨트롤할 CreateRoomUI 스크립트를 만든다.
            => 방 만들기 UI의 속성을 구조체로 만든다.
                => 임포스터 수, 최대 인원 수
                => 이 구조체를 이용해서 새로 만든 방의 데이터를 저장해 두고 새로 만들어진 방에 데이터를 전달한다.
        b. 이미지를 담을 리스트, 버튼을 담을 리스트를 속성으로 만든다.
        c. 구조체 데이터를 속성으로 만든다.
        d. 임포스터, 플레이어 수에 따라 크루원 이미지가 출력되도록 갱신해줄 함수를 만든다.
            UpdateCrewImages
            {
                1. 구조체 속성에 저장되어 있는 임포스터 수를 받아서
                int imposterCount = roomData.imposterCount;
                2. 그 수가 0이 될 때 까지 
                int index = 0;
                while(imposterCount != 0)
                {
                    3. 반복문을 돌며 랜덤한 크루원을 뽑는데, 이때 랜덤한 크루원이 선택되기 전에 배열을 넘을 경우 처음으로 돌아간다.
                    if(index >= roomData.maxPlayerCount) index = 0;
                    4. 크루원의 이미지를 랜덤으로 뽑아 빨간색으로 만든다.
                    if(crewImgs[index].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
                    {
                        crewImgs[index].material.SetColor("PlayerColor", Color.red);
                        imposterCount--;
                    }
                    index++;
                }
                // 5. 설정한 플레이어 수 만큼만 크루원 이미지를 활성화 시킨다.
                for(int i = 0; i < crewImgs.Count; i++)
                {
                    if(i < roomData.maxPlayerCount)
                    {
                        crewImgs[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        crewImgs[i].gameObject.SetActive(false);
                    }
                }
            }
        e. 처음 방 만들기 UI 에 들어왔을 때 초기 값을 지정해 준다.
            Start()
            {
                roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
                UpdateCrewImages();
            }
        f. 테스트를 해보면 10명의 크루원 모드 임포스터 색으로 출력되는 것을 볼 수 있다.
            => 유니티 엔진에서 인스턴스화 하지 않은 오브젝트에 적용된 마테리얼을 수정하면
            => 원본 마테리얼이 변경되는 특성 때문에 모두가 변경된 색상으로 출력되는 것
            => 이 문제를 해결하기 위해 마테리얼을 인스턴스화 하여 원본이 아닌 복제된 마테리얼을 사용해야 한다.
        g. Start 함수에서 crewImgs 에 마테리얼을 Instantitate 함수로 복제된 마테리얼 인스턴스를 만들어서 크루원 이미지들의 마테리얼을 바꿔준다.
            {
                for(int i = 0; i < crewImgs.Count; i++)
                {
                    // 1. 마테리얼을 인스턴스화 하여 복제한 뒤에
                    Material materialInstance = Instantitate(crewImgs[i].material);
                    // 2. 복제한 마테리얼을 크루원에 적용
                    crewImgs[i].material = materialInstance;
                }
            }
        h. UpdateCrewImages 함수에서 크루원의 색상을 바꿔주기 전에 색상을 초기화 하는 작업을 한다.
            {
                for(int i = 0; i < crewImgs.Count; i++)
                {
                    crewImgs[i].material.SetColor("_PlayerColor", Color.white);
                }
            }

    3. 최대 인원수 선택 기능 구현
        a. 최대 인원 수를 변경하는 UpdateMaxPlayerCount() 함수를 만든다.
            => 매개변수로 버튼이 선택될 때 인원 수를 전달 받는다.
        b. 전달 받은 매개변수를 구조체 데이터에 저장하고 이미지 UI를 출력한다.
            {
                1. 매개변수를 데이터에 배정
                roomData.maxPlayerCount = count;

                for(int i = 0; i < maxPlayerCountButtons.Count; i++)
                {
                    if(i == count - 5)
                    {
                        2. 선택된 버튼 이미지 UI 의 알파값을 조정
                        maxPlayerCountButtons[i].image.color = new Color( 1f, 1f, 1f, 1f );
                    }
                    else
                    {
                        maxPlayerCountButtons[i].image.color = new Color( 1f, 1f, 1f, 0f );
                    }
                }
                3. 버튼의 알파 값을 조정한 뒤에 크루원 이미지 출력
                UpdateCrewImages();
            }
        c. 인원 수 버튼마다 함수를 연결하여 매개변수를 지정한다.

    4. 임포스터 수 선택 기능 구현
        a. 임포스터 수를 선택하는 UpdateImposterCount() 함수를 만든다.
            {
                1. 최대 인원과 마찬가지로 매개변수로 값을 받아와서 데이터에 저장한다.
                roomData.imposterCount = count;
                2. 선택된 버튼의 이미지 UI 를 표시
                for(int i = 0; i < imposterCountButton.Count; i++)
                {
                    if(i == count - 1)
                    {
                        imposterCountButton[i].image.color = new Color(1f, 1f, 1f, 1f);
                    }
                    else
                    {
                        imposterCountButton[i].image.color = new Color(1f, 1f, 1f, 0f);
                    }
                }
            }
        b. 임포스터  수에 따라 최대 인원수 제한을 걸도록 한다.
            변수를 만들어서 임포스터 매개변수 값에 따라 제한할 플레이어 인원 수를 저장한다.
            {
                ...
                int limitMaxPlayer = count = 1 ? 4 : count == 2 ? 7 : 9;
                1. 데이터의 플레이어 수가 제한 값보다 작다면 제한 값을 UpdateMaxPlayer() 함수의 매개변수로 전달하여 함수를 호출한다.
                if(roomData.maxPlayerCount < limitMaxPlayer)
                {
                    UpdateMaxPlayerCount(limitMaxPlayer);
                }
                2. 데이터의 플레이어 수가 더 크다면 데이터 값으로 함수를 호출한다.
                else
                {
                    UpdateMaxPlayerCount(roomData.maxPlayerCount);
                }
            }
        c. 제한 값보다 작은 수의 버튼을 비활성화 한다.
            for(int i = 0; i < maxPlayerCountButtons.Count; i++)
            {
                var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
                if(i < limitMaxPlayer - 5)
                {
                    maxPlayerCountButton[i].interactable = false;
                    text.color = Color.gray;
                }
                else
                {
                    maxPlayerCountButtons[i].interactable = true;
                    text.color = color.white;
                }
            }

    5. 온라인 UI 와 방 만들기 UI 연결
        a. 방 만들기 UI 에서 취소 버튼에 온라인 UI 와 방 만들기 UI 오브젝트를 연결하여 활성화/비활성화를 버튼에 등록한다.
        b. 온라인 UI 에서 방 만들기 버튼을 누를 때 사용자 이름을 작성하지 않았을 경우
            입력창에 애니메이션을 등록해 준다.
        c. OnlineUI 스크립트를 만든다.
        d. 사용자 이름을 입력 받을 InputField 와 방 만들기 UI 게임 오브젝트를 속성으로 만든다.
        e. 방 만들기 버튼을 눌렀을 때 실행될 함수를 만든다. OnClickCreateRoomButton()
            {
                1. 입력 받은 값이 있다면 방 만들기 UI 활성화 
                if(nicknameInputField.text != "")
                {
                    createRoomUI.SetActive(true);
                    gameObject.SetActive(false);
                }
                else
                {
                    2. 입력 받은 값이 없다면 애니메이션 출력
                    nicknameInputField.GetComponent<Animator>().SetTrigger("On");
                }
            }
        f. PlayerSetting 클래스에 속성으로 사용자 이름을 만든다.
            public statuc string nickname;
        g. 방 만들기 버튼이 눌렸을 때 입력 받은 사용자 이름을 저장한다.
            PlayerSetting.nickname = nicknameInputField.text;
*/