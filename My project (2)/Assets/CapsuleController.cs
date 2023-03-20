using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float speed = 5f; // 캡슐 이동 속도
    public float jumpForce = 7f; // 캡슐 점프 힘
    public float gravity = 9.81f; // 중력 가속도
    public float maxVelocityChange = 10f; // 최대 속도 변화량
    public bool canJump = true; // 점프 가능 여부

    private bool grounded = false; // 지면에 닿았는지 여부
    private Rigidbody rb; // 캡슐의 물리 엔진
    private Transform cameraTransform; // 카메라 트랜스폼
    private Vector2 mouseInput; // 마우스 입력 값

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 캡슐의 물리 엔진 컴포넌트 가져오기
        rb.freezeRotation = true; // 회전 고정
        rb.useGravity = false; // 중력 사용하지 않음
        cameraTransform = Camera.main.transform; // 메인 카메라의 트랜스폼 가져오기
        Debug.Log(cameraTransform.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 이동 입력

        float x = Input.GetAxis("Horizontal"); // 좌우 이동 입력
        float z = Input.GetAxis("Vertical"); // 앞뒤 이동 입력
        Vector3 moveDirection = new Vector3(x, 0f, z); // 이동 방향 벡터 생성
        moveDirection = transform.TransformDirection(moveDirection); // 이동 방향 벡터를 캡슐의 방향으로 변환
        moveDirection *= speed; // 이동 방향 벡터에 이동 속도를 곱함

        // 점프 입력
        if (canJump && grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, CalculateJumpVerticalSpeed(), rb.velocity.z); // 점프 실행
        }

        // 마우스 입력
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // 카메라 회전
        cameraTransform.RotateAround(transform.position, Vector3.up, mouseInput.x);
        cameraTransform.RotateAround(transform.position, cameraTransform.right, -mouseInput.y);
        cameraTransform.localEulerAngles = new Vector3(cameraTransform.localEulerAngles.x, 0f, 0f);

        // 중력 적용
        rb.AddForce(new Vector3(0f, -gravity * rb.mass, 0f)); // 중력 힘 적용

        // 속도 제한
        Vector3 velocity = rb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocityChange, maxVelocityChange);
        velocity.z = Mathf.Clamp(velocity.z, -maxVelocityChange, maxVelocityChange);
        rb.velocity = velocity;

        // 이동 실행
        rb.AddForce(moveDirection, ForceMode.VelocityChange);
        grounded = false; // 지면에 닿지 않음
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // 지면과 충돌한 경우
        if (collisionInfo.gameObject.tag == "Ground")
        {
            grounded = true; // 지면에 닿음
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        // 수직 점프 속도 계산
        return Mathf.Sqrt(2 * jumpForce * gravity);
    }
}
