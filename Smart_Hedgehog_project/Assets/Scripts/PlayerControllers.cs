using UnityEngine;
using System.Collections;

public class PlayerControllers : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpValue = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        /// Получаем компонент CharacterController, который прикреплен к текущему GameObject
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)///Проверяем или объект стоит на земле
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Debug.Log("Input.GetAxis = " + moveDirection);
            /// Преобразование кординат текущего объекта изи локальных в  world space
            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= speed;
            if (Input.GetButton("Jump"))///Проверка на нажатие кнопки прыжка
                /// Тут выставляется высота Y вектора, не положение объекта
                moveDirection.y = jumpValue;

        }
        /// Отнимаем гравитацию 
        //Debug.Log("moveDirection.y = " + moveDirection.y + " / gravity * Time.deltaTime = " + gravity * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }


}
