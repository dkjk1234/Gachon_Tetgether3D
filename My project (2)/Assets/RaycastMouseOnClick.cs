using UnityEngine;

public class RaycastOnMouseClick : MonoBehaviour
{
    public float maxDistance = 10f; // 레이캐스트 최대 거리

    private Camera cam; // 메인 카메라

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; // 메인 카메라 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // 마우스 위치를 기준으로 레이 생성
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance)) // 레이캐스트 실행
            {
                Debug.Log("Hit: " + hitInfo.collider.gameObject.name); // 충돌한 오브젝트의 이름 출력
                // TODO: 레이캐스트 충돌 시 처리할 코드 작성
            }
        }
    }
}