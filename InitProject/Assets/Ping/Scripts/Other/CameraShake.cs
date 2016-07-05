using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shake_decay_default = 0.002f;
    public float shake_intensity_default = 0.3f;
    public float shake_decay;
    public float shake_intensity;
    public Transform world;

    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.localPosition = Random.insideUnitSphere * shake_intensity;
            world.localPosition = Random.insideUnitSphere * shake_intensity;
            shake_intensity -= shake_decay * Time.deltaTime;
        }
    }

    public void Shake()
    {
        shake_intensity = shake_intensity_default;
        shake_decay = shake_decay_default;
    }
}