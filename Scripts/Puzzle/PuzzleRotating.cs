using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Puzzle
{

    public class PuzzleRotating : MonoBehaviour
    {
        private bool isRotating = false;
        private float rotationTarget = 0f;
        private float rotationSpeed = 100.0f;

        void Update()
        {
            if (Input.GetMouseButtonDown(1) && !isRotating && IsMouseOver())
            {
                rotationTarget += 90f;

                isRotating = true;
            }

            if (isRotating)
            {
                float step = rotationSpeed * Time.deltaTime;
                float currentRotation = transform.rotation.eulerAngles.z;
                float newRotation = Mathf.MoveTowardsAngle(currentRotation, rotationTarget, step);
                transform.rotation = Quaternion.Euler(0, 0, newRotation);

                if (Mathf.Approximately(newRotation, rotationTarget))
                {
                    isRotating = false;
                    transform.rotation = Quaternion.Euler(0, 0, rotationTarget);
                }
            }
        }

        bool IsMouseOver()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                return hit.collider.gameObject == gameObject;
            }

            return false;
        }
    }
}