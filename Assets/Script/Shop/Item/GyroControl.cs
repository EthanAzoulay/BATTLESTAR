using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    // Limites de rotation pour chaque axe (en degrés)
    public float minXRotation = -45f;
    public float maxXRotation = 45f;
    public float minYRotation = -45f;
    public float maxYRotation = 45f;
    public float minZRotation = -45f;
    public float maxZRotation = 45f;

    // Rotation de correction initiale
    private Quaternion correctionQuaternion;

    void Start()
    {
        gyroEnabled = EnableGyro();
        correctionQuaternion = Quaternion.Euler(90, 0, 0); // Ajustez les angles selon vos besoins
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }
        return false;
    }

    void Update()
    {
        if (gyroEnabled)
        {
            Quaternion gyroRotation = GyroToUnity(gyro.attitude);
            gyroRotation = correctionQuaternion * gyroRotation; // Appliquer la rotation de correction

            Vector3 euler = gyroRotation.eulerAngles;

            // Convertir les angles pour être dans la plage [-180, 180] pour une limitation correcte
            euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
            euler.y = (euler.y > 180) ? euler.y - 360 : euler.y;
            euler.z = (euler.z > 180) ? euler.z - 360 : euler.z;

            // Appliquer les limites
            euler.x = Mathf.Clamp(euler.x, minXRotation, maxXRotation);
            euler.y = Mathf.Clamp(euler.y, minYRotation, maxYRotation);
            euler.z = Mathf.Clamp(euler.z, minZRotation, maxZRotation);

            // Reconvertir les angles pour l'utiliser dans Quaternion
            transform.localRotation = Quaternion.Euler(euler);
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
