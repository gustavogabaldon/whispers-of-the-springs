using UnityEngine;

public class AutoSeagrassGenerator : MonoBehaviour
{
    [Header("Assign your Sand / Ground object (the plane)")]
    public Transform sand; // ← Drag Ground object here

    [Header("Seagrass Settings")]
    public int patchCount = 150;
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
        seaMat.color = new Color(0.25f, 0.8f, 0.45f);
    }

    void GenerateSeagrass()
    {
        // Get size of sand mesh
        MeshRenderer r = sand.GetComponent<MeshRenderer>();
        if (r == null)
        {
            Debug.LogError("❌ The object assigned as Sand has no MeshRenderer. Assign your Ground plane.");
            return;
        }

        Vector3 size = r.bounds.size;
        Vector3 center = r.bounds.center;

        for (int i = 0; i < patchCount; i++)
        {
            Vector3 pos = new Vector3(
                center.x + Random.Range(-size.x / 2, size.x / 2),
                Random.Range(minY, maxY),
                center.z + Random.Range(-size.z / 2, size.z / 2)
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

                SimpleSway sway = blade.AddComponent<SimpleSway>();
                sway.speed = swaySpeed;
                sway.amplitude = swayAmplitude;
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
