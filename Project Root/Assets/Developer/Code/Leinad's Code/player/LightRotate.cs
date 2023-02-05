using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoStorage;

public class LightRotate : MonoBehaviour
{
    void Update()
    {
        // Obtener la posición del cursor del mouse en el plano xz
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        // Hacer que el objeto mire hacia la posición del cursor del mouse
        transform.LookAt(mousePosition);

        // Asegurarse de que x siga siendo 90 grados y z siga siendo 0 grados
        transform.rotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, 0f);
    }
}
