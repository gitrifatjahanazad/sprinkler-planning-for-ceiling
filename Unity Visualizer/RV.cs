using System.Collections.Generic;
using UnityEngine;

public class RoomVisualizer : MonoBehaviour
{
    void Start()
    {
        float scale = 0.001f;

        // Ceiling points
        Vector3 a = new Vector3(97500.00f, 34000.00f, 2500.00f) * scale;
        Vector3 b = new Vector3(85647.67f, 43193.61f, 2500.00f) * scale;
        Vector3 c = new Vector3(91776.75f, 51095.16f, 2500.00f) * scale;
        Vector3 d = new Vector3(103629.07f, 41901.55f, 2500.00f) * scale;

        // Create ceiling mesh
        GameObject ceilingObj = new GameObject("Ceiling");
        MeshFilter meshFilter = ceilingObj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = ceilingObj.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        Mesh ceilingMesh = new Mesh();
        ceilingMesh.vertices = new Vector3[] { a, b, c, d };
        ceilingMesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        ceilingMesh.RecalculateNormals();
        meshFilter.mesh = ceilingMesh;

        // Sprinkler positions
        List<Vector3> sprinklers = new List<Vector3>
        {
            new Vector3(97056.88f, 37507.66f, 2500.00f),
            new Vector3(95081.49f, 39039.92f, 2500.00f),
            new Vector3(93106.11f, 40572.19f, 2500.00f),
            new Vector3(91130.72f, 42104.46f, 2500.00f),
            new Vector3(89155.33f, 43636.73f, 2500.00f),
            new Vector3(98589.15f, 39483.04f, 2500.00f),
            new Vector3(96613.76f, 41015.31f, 2500.00f),
            new Vector3(94638.37f, 42547.58f, 2500.00f),
            new Vector3(92662.99f, 44079.85f, 2500.00f),
            new Vector3(90687.60f, 45612.11f, 2500.00f),
            new Vector3(100121.42f, 41458.43f, 2500.00f),
            new Vector3(98146.03f, 42990.70f, 2500.00f),
            new Vector3(96170.64f, 44522.97f, 2500.00f),
            new Vector3(94195.25f, 46055.23f, 2500.00f),
            new Vector3(92219.87f, 47587.50f, 2500.00f)
        };

        // Connection points
        List<Vector3> connections = new List<Vector3>
        {
            new Vector3(97073.80f, 37494.52f, 3056.87f),
            new Vector3(95101.33f, 39024.53f, 3152.88f),
            new Vector3(93128.86f, 40554.54f, 3248.89f),
            new Vector3(91156.40f, 42084.54f, 3344.90f),
            new Vector3(89183.93f, 43614.55f, 3440.92f),
            new Vector3(98560.55f, 39505.22f, 3440.92f),
            new Vector3(96588.08f, 41035.23f, 3344.90f),
            new Vector3(94615.61f, 42565.23f, 3248.89f),
            new Vector3(92643.14f, 44095.24f, 3152.88f),
            new Vector3(90670.67f, 45625.24f, 3056.87f),
            new Vector3(100121.42f, 41458.43f, 3000.00f),
            new Vector3(98146.03f, 42990.70f, 3000.00f),
            new Vector3(96170.64f, 44522.97f, 3000.00f),
            new Vector3(94195.25f, 46055.23f, 3000.00f),
            new Vector3(92219.86f, 47587.50f, 3000.00f)
        };

        // Materials
        Material redMat = new Material(Shader.Find("Standard"));
        redMat.color = Color.red;

        Material greenMat = new Material(Shader.Find("Standard"));
        greenMat.color = Color.green;

        Material blueMat = new Material(Shader.Find("Sprites/Default"));

        // Create sprinklers, connections, and lines
        for (int i = 0; i < sprinklers.Count; i++)
        {
            // Sprinkler
            GameObject spr = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spr.transform.position = sprinklers[i] * scale;
            spr.transform.localScale = Vector3.one * 0.1f;
            spr.GetComponent<Renderer>().material = redMat;

            // Connection
            GameObject conn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            conn.transform.position = connections[i] * scale;
            conn.transform.localScale = Vector3.one * 0.1f;
            conn.GetComponent<Renderer>().material = greenMat;

            // Connection line
            GameObject lineObj = new GameObject("ConnectionLine");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, spr.transform.position);
            lr.SetPosition(1, conn.transform.position);
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = blueMat;
            lr.startColor = Color.blue;
            lr.endColor = Color.blue;
        }

        // Pipes
        List<System.Tuple<Vector3, Vector3>> pipes = new List<System.Tuple<Vector3, Vector3>>
        {
            System.Tuple.Create(new Vector3(98242.11f, 36588.29f, 3000.00f), new Vector3(87970.10f, 44556.09f, 3500.00f)),
            System.Tuple.Create(new Vector3(99774.38f, 38563.68f, 3500.00f), new Vector3(89502.37f, 46531.47f, 3000.00f)),
            System.Tuple.Create(new Vector3(101306.65f, 40539.07f, 3000.00f), new Vector3(91034.63f, 48506.86f, 3000.00f))
        };

        Material yellowMat = new Material(Shader.Find("Sprites/Default"));

        foreach (var pipe in pipes)
        {
            Vector3 p1 = pipe.Item1 * scale;
            Vector3 p2 = pipe.Item2 * scale;

            GameObject pipeObj = new GameObject("Pipe");
            LineRenderer plr = pipeObj.AddComponent<LineRenderer>();
            plr.positionCount = 2;
            plr.SetPosition(0, p1);
            plr.SetPosition(1, p2);
            plr.startWidth = 0.1f;
            plr.endWidth = 0.1f;
            plr.material = yellowMat;
            plr.startColor = Color.yellow;
            plr.endColor = Color.yellow;
        }

        // Draw ceiling edges (blue lines)
        Vector3[] verts = new Vector3[] { a, b, c, d };
        for (int i = 0; i < verts.Length; i++)
        {
            GameObject edgeObj = new GameObject("CeilingEdge");
            LineRenderer edgeLR = edgeObj.AddComponent<LineRenderer>();
            edgeLR.positionCount = 2;
            edgeLR.SetPosition(0, verts[i]);
            edgeLR.SetPosition(1, verts[(i + 1) % verts.Length]);
            edgeLR.startWidth = 0.1f;
            edgeLR.endWidth = 0.1f;
            edgeLR.material = blueMat;
            edgeLR.startColor = Color.blue;
            edgeLR.endColor = Color.blue;
        }

        // OFFSET CALCULATION

        float offsetDist = 2500f * scale; // offset amount scaled

        // Helper function to get inset vertex point
        Vector3 GetInsetPoint(Vector3 prev, Vector3 current, Vector3 next, float offset)
        {
            // Calculate edges (direction vectors)
            Vector3 edge1 = (current - prev).normalized;
            Vector3 edge2 = (next - current).normalized;

            // Calculate inward normals (perpendicular to edges)
            Vector3 normal1 = new Vector3(-edge1.y, edge1.x, 0);
            Vector3 normal2 = new Vector3(-edge2.y, edge2.x, 0);

            // Sum of normals gives bisector direction
            Vector3 bisector = (normal1 + normal2).normalized;

            // Angle between edges in radians (divide by 2)
            float angle = Vector3.Angle(edge1, edge2) * Mathf.Deg2Rad / 2f;

            // Distance to move along bisector to achieve offset inset
            float insetDistance = offset / Mathf.Sin(angle);

            return current - bisector * insetDistance;
        }

        // Calculate inset points for all corners
        Vector3 offsetA = GetInsetPoint(verts[3], verts[0], verts[1], offsetDist);
        Vector3 offsetB = GetInsetPoint(verts[0], verts[1], verts[2], offsetDist);
        Vector3 offsetC = GetInsetPoint(verts[1], verts[2], verts[3], offsetDist);
        Vector3 offsetD = GetInsetPoint(verts[2], verts[3], verts[0], offsetDist);

        Vector3[] offsetVerts = new Vector3[] { offsetA, offsetB, offsetC, offsetD };

        // Create purple material for offset rectangle
        Material purpleMat = new Material(Shader.Find("Sprites/Default"));
        purpleMat.color = Color.magenta;

        // Draw offset rectangle edges (purple lines)
        for (int i = 0; i < offsetVerts.Length; i++)
        {
            GameObject offsetEdgeObj = new GameObject("OffsetEdge");
            LineRenderer offLR = offsetEdgeObj.AddComponent<LineRenderer>();
            offLR.positionCount = 2;
            offLR.SetPosition(0, offsetVerts[i]);
            offLR.SetPosition(1, offsetVerts[(i + 1) % offsetVerts.Length]);
            offLR.startWidth = 0.1f;
            offLR.endWidth = 0.1f;
            offLR.material = purpleMat;
            offLR.startColor = Color.magenta;
            offLR.endColor = Color.magenta;
        }

        //grid
        // Grid spacing in world units (scaled)
        float gridSpacing = 2500f * scale;

        // Directions along two edges of the purple rectangle
        Vector3 vecX = offsetB - offsetA; // edge from A to B
        Vector3 vecY = offsetD - offsetA; // edge from A to D

        float lengthX = vecX.magnitude;
        float lengthY = vecY.magnitude;

        Vector3 dirX = vecX.normalized;
        Vector3 dirY = vecY.normalized;

        // Calculate how many grid lines fit along each direction
        int numLinesX = Mathf.FloorToInt(lengthX / gridSpacing);
        int numLinesY = Mathf.FloorToInt(lengthY / gridSpacing);

        // Material for grid lines (orange)
        Material orangeMat = new Material(Shader.Find("Sprites/Default"));
        orangeMat.color = new Color(1f, 0.5f, 0f); // orange

        // Draw lines parallel to vecX (horizontal grid lines)
        // For each step along vecY direction, draw a line from offsetA + t*vecY to offsetB + t*vecY
        for (int i = 0; i <= numLinesY; i++)
        {
            float t = i * gridSpacing;
            Vector3 start = offsetA + dirY * t;
            Vector3 end = offsetB + dirY * t;

            GameObject lineObj = new GameObject("GridLine_H_" + i);
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = orangeMat;
            lr.startColor = orangeMat.color;
            lr.endColor = orangeMat.color;
        }

        // Draw lines parallel to vecY (vertical grid lines)
        // For each step along vecX direction, draw a line from offsetA + t*vecX to offsetD + t*vecX
        for (int i = 0; i <= numLinesX; i++)
        {
            float t = i * gridSpacing;
            Vector3 start = offsetA + dirX * t;
            Vector3 end = offsetD + dirX * t;

            GameObject lineObj = new GameObject("GridLine_V_" + i);
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.material = orangeMat;
            lr.startColor = orangeMat.color;
            lr.endColor = orangeMat.color;
        }


        // Position camera
        Vector3 center = (a + b + c + d) / 4f;
        Camera.main.transform.position = center + new Vector3(0, 0, 20f);
        Camera.main.transform.LookAt(center);

        // Debug log output
        Debug.Log(15);
        foreach (var s in sprinklers)
        {
            Debug.Log($"({s.x:F2}, {s.y:F2}, {s.z:F2})");
        }
        foreach (var conn in connections)
        {
            Debug.Log($"({conn.x:F2}, {conn.y:F2}, {conn.z:F2})");
        }
    }
}
