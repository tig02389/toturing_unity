using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{

    [SerializeField] // 창에서 쉽게 수정가능
    private float walkSpeed; // 스트립트내에서만 조절

    [SerializeField] // 창에서 쉽게 수정가능
    private float lookSensitivity;// 카메라 민감도

    [SerializeField]
    private float cameraRotationLimit; // 고개 각도 제한
    private float currentCameraRotationX = 0; // 45f: 45도로 보고있음


    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid; // 물리적 몸 구성

    // Use this for initialization
    void Start()
    {
        // theCamera = FindObjectOfType<Camera>(); // 카메라찾기 //하지만 이번에는 드래그로 바로 넣었음
        myRigid = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {//1초당 60회
        Move();
        CameraRotation();
        CharacherRotation();

    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal"); //우측 좌측

        // 왼쪽-1, 아무것도하지않았을때 0, 오른쪽 1

        float _moveDirZ = Input.GetAxisRaw("Vertical"); // 앞 뒤

        // 뒤 -1 , 앞 1

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        // (1,0,0)
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        // (0,0,1)

        // 이동                                          이동값 * 속도
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        //(1,0,0)+(0.0.1) = (1,0,1)  = 2
        // (0.5,0,0.5) = 1
        // normalized : 합이 1이 나오도록 정규화


        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); //Time.deltaTime 없으면 순간이동하는 것처럼 보임
    }

    private void CharacherRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characherRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characherRotationY));
    }

    private void CameraRotation()
    {
        //상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스 Y: 위아래
        float _cameraRotationX = _xRotation * lookSensitivity; //lookSensitivity 으로 확 화면이 바뀌는 것이 아니라 민감도에 따라 속도조절을하면서 바뀔 수 있도록 함
        currentCameraRotationX -= _cameraRotationX;  // 카메라 반전으로 +가아닌 -를 해야지 마우스가 위로갈때 화면이 위로 올라갈 수 있다.
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); // Clamp로 값을 최소 최대로 제한,(값, 최소값, 최대값)

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f); // localEulerAngles: Rotation xyz값임
    }
}
