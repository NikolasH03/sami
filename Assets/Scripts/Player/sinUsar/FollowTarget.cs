using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    private float CinemachineTargetPitch;
    private float CinemachineTargetYaw;

    private void LateUpdate()
    {
        CameraLogic();
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * rotationSpeed * Time.deltaTime;
    }

    private void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");

        CinemachineTargetPitch = UpdateRotation(CinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        CinemachineTargetYaw += mouseX;

        applyRotations(CinemachineTargetPitch, CinemachineTargetYaw);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private void applyRotations(float pitch, float yaw)
    {
        // Aplicamos la rotación solo en el eje Y para evitar que el personaje gire en X y Z.
        followTarget.rotation = Quaternion.Euler(0f, yaw, 0f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}

