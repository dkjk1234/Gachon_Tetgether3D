using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float speed = 5f; // ĸ�� �̵� �ӵ�
    public float jumpForce = 7f; // ĸ�� ���� ��
    public float gravity = 9.81f; // �߷� ���ӵ�
    public float maxVelocityChange = 10f; // �ִ� �ӵ� ��ȭ��
    public bool canJump = true; // ���� ���� ����

    private bool grounded = false; // ���鿡 ��Ҵ��� ����
    private Rigidbody rb; // ĸ���� ���� ����
    private Transform cameraTransform; // ī�޶� Ʈ������
    private Vector2 mouseInput; // ���콺 �Է� ��

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // ĸ���� ���� ���� ������Ʈ ��������
        rb.freezeRotation = true; // ȸ�� ����
        rb.useGravity = false; // �߷� ������� ����
        cameraTransform = Camera.main.transform; // ���� ī�޶��� Ʈ������ ��������
        Debug.Log(cameraTransform.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �̵� �Է�

        float x = Input.GetAxis("Horizontal"); // �¿� �̵� �Է�
        float z = Input.GetAxis("Vertical"); // �յ� �̵� �Է�
        Vector3 moveDirection = new Vector3(x, 0f, z); // �̵� ���� ���� ����
        moveDirection = transform.TransformDirection(moveDirection); // �̵� ���� ���͸� ĸ���� �������� ��ȯ
        moveDirection *= speed; // �̵� ���� ���Ϳ� �̵� �ӵ��� ����

        // ���� �Է�
        if (canJump && grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, CalculateJumpVerticalSpeed(), rb.velocity.z); // ���� ����
        }

        // ���콺 �Է�
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // ī�޶� ȸ��
        cameraTransform.RotateAround(transform.position, Vector3.up, mouseInput.x);
        cameraTransform.RotateAround(transform.position, cameraTransform.right, -mouseInput.y);
        cameraTransform.localEulerAngles = new Vector3(cameraTransform.localEulerAngles.x, 0f, 0f);

        // �߷� ����
        rb.AddForce(new Vector3(0f, -gravity * rb.mass, 0f)); // �߷� �� ����

        // �ӵ� ����
        Vector3 velocity = rb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocityChange, maxVelocityChange);
        velocity.z = Mathf.Clamp(velocity.z, -maxVelocityChange, maxVelocityChange);
        rb.velocity = velocity;

        // �̵� ����
        rb.AddForce(moveDirection, ForceMode.VelocityChange);
        grounded = false; // ���鿡 ���� ����
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // ����� �浹�� ���
        if (collisionInfo.gameObject.tag == "Ground")
        {
            grounded = true; // ���鿡 ����
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        // ���� ���� �ӵ� ���
        return Mathf.Sqrt(2 * jumpForce * gravity);
    }
}
