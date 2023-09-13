using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomUI : MonoBehaviour
{
    // 에디터에 배치해 놓은 크루원의 UI 이미지, [1,2,3]임포스터 수 버튼, [1~10]크루원 수 버튼
    [SerializeField]
    private List<Image> crewImgs;
    [SerializeField]
    private List<Button> imposterCountButtons;
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    void Start()
    {
        // 방 만들기 UI가 띄워질 때 기본 설정을 하여 표시
        for(int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }

        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }

    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        // 선택된 버튼을 눈에띄게 표시
        for(int i = 0; i < imposterCountButtons.Count; i++)
        {
            if(i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        // 임포스터 수에 따라 최대 인원 수를 제한
        int limitMaxPlayer = count == 1 ? 5 : count == 2 ? 7 : 9;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        // 제한된 크루원 수 만큼 버튼을 비활성화하고 숫자의 색상을 바꾼다.
        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();

            if(i < limitMaxPlayer - 5)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        // 선택된 버튼을 눈에 띄게 표시
        for(int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 5)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        // 바뀐 크루원 수를 업데이트
        UpdateCrewImages();
    }

    void UpdateCrewImages()
    {
        // 가장 먼저 모든 크루원 UI 이미지의 색상을 초기화 해준다.
        for(int i = 0; i < crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColors", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;

        // 임포스터의 수 만큼 랜덤한 UI 이미지의 색상을 바꿔준다.
        while(imposterCount != 0)
        {
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if(crewImgs[idx].material.GetColor("_PlayerColors") 
                != Color.red && Random.Range(0, 5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColors", Color.red);
                imposterCount--;
            }
            idx++;   
        }

        // 선택된 크루원 수 만큼 UI 이미지를 활성화 한다.
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
}

// 방 만들기에 설정된 데이터를 저장할 구조체
public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}