using UnityEngine;

public class ObjectSticker : MonoBehaviour
{
    public string objectTag = "Sticky"; // 고정할 오브젝트 태그
    public float stickingForce = 100f; // 고정할 때 가해질 힘
    public float stickingDistance = 0.2f; // 고정 거리
    public bool isStuck = false; // 고정 여부

    private Transform stickyObject; // 고정할 오브젝트의 위치

    // Update is called once per frame
    void Update()
    {
     
        if (!isStuck)
        {
            // 레이캐스트 실행
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.tag == objectTag)
                {
                    // 고정할 오브젝트의 위치 저장
                    stickyObject = hitInfo.collider.transform;
                    isStuck = true;

                    // 고정할 때 가해질 힘 적용
                    Rigidbody rb = stickyObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(transform.forward * stickingForce, ForceMode.Force);
                    }
                }
            }
        }
        else
        {
            // 고정된 상태에서는 고정할 오브젝트를 따라 이동시킴
            transform.position = stickyObject.position;
        }
    }
}
