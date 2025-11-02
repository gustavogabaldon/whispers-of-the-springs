using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AutoSeagrassGenerator : MonoBehaviour
{
    [Header("Seagrass Settings")]
    public int patchCount = 150;
    public Vector2 areaSize = new Vector2(60, 60);
    public int bladesPerPatch = 10;
    public float bladeHeight = 1.5f;
    public float bladeWidth = 0.1f;
    public float minY = -9f;
    public float maxY = -8.8f;

    [Header("Animation")]
    public float swaySpeed = 1.5f;
    public float swayAmplitude = 0.1f;

    private Material seaMat;

    void Start()
    {
        GenerateMaterial();
        GenerateSeagrass();
    }

    void GenerateMaterial()
    {
        seaMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        seaMat.doubleSidedGI = true;
        seaMat.SetFloat("_Surface", 0); // opaque
        seaMat.color = new Color(0.25f, 0.8f, 0.45f); // seagrass green
    }

    void GenerateSeagrass()
    {
        for (int i = 0; i < patchCount; i++)
        {
            Vector3 pos = new Vector3(
                transform.position.x + Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(minY, maxY),
                transform.position.z + Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );

            GameObject patch = new GameObject("SeagrassPatch_" + i);
            patch.transform.position = pos;
            patch.transform.parent = transform;

            for (int j = 0; j < bladesPerPatch; j++)
            {
                GameObject blade = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blade.transform.parent = patch.transform;
                blade.transform.localPosition = new Vector3(
                    Random.Range(-0.2f, 0.2f),
                    0,
                    Random.Range(-0.2f, 0.2f)
                );
                blade.transform.localScale = new Vector3(bladeWidth, bladeHeight, 1);
                blade.transform.Rotate(0, Random.Range(0, 360), 0);

                Renderer rend = blade.GetComponent<Renderer>();
                rend.sharedMaterial = seaMat;

                blade.AddComponent<SimpleSway>().speed = swaySpeed;
                blade.GetComponent<SimpleSway>().amplitude = swayAmplitude;
            }
        }
    }
}

public class SimpleSway : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float speed = 1.5f;
    private Quaternion baseRot;

    void Start()
    {
        baseRot = transform.rotation;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed + transform.position.x * 0.5f) * amplitude;
        transform.rotation = baseRot * Quaternion.Euler(0, offset * 20f, 0);
    }
}
