using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float speed;

    private HingeJoint2D joint;
    private bool isMovingTowardsUpperLimit;

    public void Start()
    {
        joint = GetComponent<HingeJoint2D>();

        isMovingTowardsUpperLimit = true;
        SetMotorSpeed(speed);
    }

    public void FixedUpdate()
    {
        if (
            isMovingTowardsUpperLimit &&
            joint.limitState == JointLimitState2D.UpperLimit
        )
        {
            isMovingTowardsUpperLimit = false;
            SetMotorSpeed(-speed);
        }
        else if (
            !isMovingTowardsUpperLimit &&
            joint.limitState == JointLimitState2D.LowerLimit
        )
        {
            isMovingTowardsUpperLimit = true;
            SetMotorSpeed(speed);
        }
    }

    private void SetMotorSpeed(float speed)
    {
        JointMotor2D motor = joint.motor;
        motor.motorSpeed = speed;

        joint.motor = motor;
    }
}
