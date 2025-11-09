using UnityEngine;
using UnityEditor;

public class ManateeBuilder : EditorWindow
{
    [MenuItem("Tools/Create/Low Poly Manatee Pair")]
    public static void CreateManatees()
    {
        GameObject group = new GameObject("ManateePair");

        GameObject manatee1 = CreateManatee("Manatee_1", new Vector3(-3, -4, 0));
        manatee1.transform.parent = group.transform;

        GameObject manatee2 = CreateManatee("Manatee_2", new Vector3(3, -4, 0));
        manatee2.transform.parent = group.transform;

        var swim1 = manatee1.AddComponent<ManateeSwim>();
        swim1.invertForward = true; // important
        swim1.yMin = -6f;
        swim1.yMax = -1.5f;
        swim1.center = null; // world origin bounds
        swim1.xzHalfExtents = new Vector2(40f, 40f);

        var swim2 = manatee2.AddComponent<ManateeSwim>();
        swim2.invertForward = true;
        swim2.yMin = -6f;
        swim2.yMax = -1.5f;
        swim2.center = null;
        swim2.xzHalfExtents = new Vector2(40f, 40f);

        Selection.activeObject = group;
    }

    private static GameObject CreateManatee(string name, Vector3 position)
    {
        GameObject manatee = new GameObject(name);
        manatee.transform.position = position;

        Material greyMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        greyMat.color = new Color(0.72f, 0.75f, 0.77f);

        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        body.name = "Body";
        body.transform.parent = manatee.transform;
        body.transform.localScale = new Vector3(2.2f, 1.2f, 1.1f);
        body.GetComponent<Renderer>().sharedMaterial = greyMat;

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.name = "Head";
        head.transform.parent = body.transform;
        head.transform.localPosition = new Vector3(0, 0, 1.1f);
        head.transform.localScale = new Vector3(1.3f, 0.9f, 1.0f);
        head.GetComponent<Renderer>().sharedMaterial = greyMat;

        GameObject tail = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tail.name = "Tail";
        tail.transform.parent = body.transform;
        tail.transform.localPosition = new Vector3(0, -0.2f, -1.2f);
        tail.transform.localScale = new Vector3(1.1f, 0.1f, 1.3f);
        tail.transform.localRotation = Quaternion.Euler(-12f, 0, 0);
        tail.GetComponent<Renderer>().sharedMaterial = greyMat;

        CreateFin(body.transform, greyMat, new Vector3(-1f, -0.3f, 0.2f), 25f);
        CreateFin(body.transform, greyMat, new Vector3(1f, -0.3f, 0.2f), -25f);

        var sway = body.AddComponent<ManateeSway>();
        sway.tail = tail.transform;
        sway.speed = 0.5f;
        sway.angle = 10f;

        return manatee;
    }

    private static void CreateFin(Transform parent, Material mat, Vector3 pos, float rotZ)
    {
        GameObject fin = GameObject.CreatePrimitive(PrimitiveType.Cube);
        fin.name = "Fin";
        fin.transform.parent = parent;
        fin.transform.localPosition = pos;
        fin.transform.localScale = new Vector3(0.4f, 0.1f, 0.6f);
        fin.transform.localRotation = Quaternion.Euler(0, 0, rotZ);
        fin.GetComponent<Renderer>().sharedMaterial = mat;
    }
}
