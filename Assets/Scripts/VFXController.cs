using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeTest : MonoBehaviour
{

    private Volume v;
    private Bloom b;
    private Vignette vg;
    // Start is called before the first frame update
    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out b);
        v.profile.TryGet(out vg);
    }
    private void Test()
    {
        b.scatter.value = 0.1f;
        vg.intensity.value = 0.5f;
    }

}