using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]
    public float terrainLength = 200f;
    public float segmentLength = 2f;
    public float heightVariation = 5f;
    public float minHeight = -2f;
    public float maxHeight = 10f;

    [Header("Noise Settings")]
    public float noiseScale = 0.1f;
    public float hillSteepness = 2f;
    public AnimationCurve heightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Components")]
    public Material terrainMaterial;
    public EdgeCollider2D edgeCollider;
    public LineRenderer lineRenderer;

    private List<Vector2> terrainPoints = new List<Vector2>();
    private float seed;

    void Start()
    {
        seed = Random.Range(0f, 10000f);
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        terrainPoints.Clear();

        int segmentCount = Mathf.CeilToInt(terrainLength / segmentLength);

        // Generate terrain points
        for (int i = 0; i < segmentCount; i++)
        {
            float x = i * segmentLength;
            float y = CalculateHeight(x);

            terrainPoints.Add(new Vector2(x, y));
        }

        // Set up edge collider
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }
        edgeCollider.points = terrainPoints.ToArray();

        // Set up line renderer for visualization
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = terrainPoints.Count;
            for (int i = 0; i < terrainPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(terrainPoints[i].x, terrainPoints[i].y, 0));
            }
        }
        else
        {
            // Create line renderer if it doesn't exist
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = terrainPoints.Count;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = new Color(0.4f, 0.3f, 0.2f);
            lineRenderer.endColor = new Color(0.4f, 0.3f, 0.2f);

            for (int i = 0; i < terrainPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(terrainPoints[i].x, terrainPoints[i].y, 0));
            }
        }

        // Generate mesh for visual
        GenerateTerrainMesh();
    }

    float CalculateHeight(float x)
    {
        // Use multiple octaves of Perlin noise for varied terrain
        float height = 0f;

        // Base terrain
        height += Mathf.PerlinNoise((x + seed) * noiseScale, seed) * heightVariation;

        // Add hills
        height += Mathf.PerlinNoise((x + seed) * noiseScale * 0.3f, seed + 100f) * heightVariation * 2f;

        // Add some sudden steep hills
        float steepHill = Mathf.PerlinNoise((x + seed) * noiseScale * 0.5f, seed + 200f);
        if (steepHill > 0.6f)
        {
            height += (steepHill - 0.6f) * hillSteepness * 10f;
        }

        // Add small bumps
        height += Mathf.PerlinNoise((x + seed) * noiseScale * 2f, seed + 300f) * 0.5f;

        // Clamp height
        height = Mathf.Clamp(height, minHeight, maxHeight);

        return height;
    }

    void GenerateTerrainMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            if (terrainMaterial != null)
                meshRenderer.material = terrainMaterial;
            else
            {
                meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
                meshRenderer.material.color = new Color(0.3f, 0.5f, 0.2f);
            }
        }

        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Create mesh from terrain points
        float bottomY = minHeight - 10f;

        for (int i = 0; i < terrainPoints.Count; i++)
        {
            vertices.Add(new Vector3(terrainPoints[i].x, terrainPoints[i].y, 0));
            vertices.Add(new Vector3(terrainPoints[i].x, bottomY, 0));
        }

        // Create triangles
        for (int i = 0; i < terrainPoints.Count - 1; i++)
        {
            int topLeft = i * 2;
            int bottomLeft = i * 2 + 1;
            int topRight = (i + 1) * 2;
            int bottomRight = (i + 1) * 2 + 1;

            // First triangle
            triangles.Add(topLeft);
            triangles.Add(topRight);
            triangles.Add(bottomLeft);

            // Second triangle
            triangles.Add(bottomLeft);
            triangles.Add(topRight);
            triangles.Add(bottomRight);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    public void RegenerateTerrain()
    {
        seed = Random.Range(0f, 10000f);
        GenerateTerrain();
    }
}
