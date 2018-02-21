using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class SweepBulletBehaviour : BulletBehaviour
{

    protected new void fire()
    {
//        var quat = Quaternion.AngleAxis(30, new Vector3(0, 0, 1));
//        var vect = quat * transform.up;

        Destroy(gameObject, TIME_TO_LIVE_SEC);
        Rigidbody body = GetComponent<Rigidbody>();

        body.AddForce(RotateZ(transform.up, 30) * -ForwardForce);
    }

    public Vector3 RotateX(Vector3 v, float angle)
    {
        Vector3 result = new Vector3();
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float ty = v.y;
        float tz = v.z;
        result.y = (cos * ty) - (sin * tz);
        result.z = (cos * tz) + (sin * ty);
        result.x = v.x;
        return result;
    }

    public Vector3 RotateY(Vector3 v, float angle)
    {
        Vector3 result = new Vector3();
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float tx = v.x;
        float tz = v.z;
        result.x = (cos * tx) + (sin * tz);
        result.z = (cos * tz) - (sin * tx);
        result.y = v.y;
        return result;
    }

    public Vector3 RotateZ(Vector3 v, float angle)
    {
        Vector3 result = new Vector3();
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float tx = v.x;
        float ty = v.y;
        result.x = (cos * tx) - (sin * ty);
        result.y = (cos * ty) + (sin * tx);
        result.z = v.z;
        return result;
    }
}
