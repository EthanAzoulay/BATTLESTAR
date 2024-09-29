using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    // Rotation de correction initiale pour ajuster l'orientation
    private Quaternion correctionQuaternion;

    // Limites de rotation pour chaque axe (en degrés)
    public float minXRotation = -45f;
    public float maxXRotation = 45f;
    public float minYRotation = -45f;
    public float maxYRotation = 45f;
    public float minZRotation = -45f;
    public float maxZRotation = 45f;

    void Start()
    {
        gyroEnabled = EnableGyro();

        // Appliquer une correction de rotation pour aligner les axes Y et Z
        correctionQuaternion = Quaternion.Euler(90, 0f, 0f); // Ajustez selon l'orientation de départ
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
            // Convertir l'orientation du gyroscope en rotation Unity
            Quaternion gyroRotation = GyroToUnity(gyro.attitude);

            // Appliquer la rotation de correction pour aligner les axes correctement
            gyroRotation = correctionQuaternion * gyroRotation;

            // Convertir le quaternion en angles d'Euler
            Vector3 eulerRotation = gyroRotation.eulerAngles;

            // Convertir les angles pour être dans la plage [-180, 180]
            eulerRotation.x = (eulerRotation.x > 180) ? eulerRotation.x - 360 : eulerRotation.x;
            eulerRotation.y = (eulerRotation.y > 180) ? eulerRotation.y - 360 : eulerRotation.y;
            eulerRotation.z = (eulerRotation.z > 180) ? eulerRotation.z - 360 : eulerRotation.z;

            // Appliquer les limites définies pour chaque axe
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, minXRotation, maxXRotation);
            eulerRotation.y = Mathf.Clamp(eulerRotation.y, minYRotation, maxYRotation);
            eulerRotation.z = Mathf.Clamp(eulerRotation.z, minZRotation, maxZRotation);

            // Reconvertir les angles en quaternion
            transform.localRotation = Quaternion.Euler(eulerRotation);
        }
    }

    // Conversion du gyroscope à la rotation Unity avec inversion de l'axe X
    private static Quaternion GyroToUnity(Quaternion q)
    {
        // Inversion de l'axe X et Y pour corriger le comportement
        return new Quaternion(q.x, q.y, -q.z, -q.w); // Ajustement pour l'axe X
    }
}
