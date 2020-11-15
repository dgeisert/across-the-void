using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IslandBuilder : MonoBehaviour
{
    public Material islandMat;
    public int size = 40;
    public float height = 3;
    public float perlinScale = 100;
    public Color[] colors;

    public GameObject[] beachObjects, plantObjects, rockObjects;

    void Start()
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> verts2 = new List<Vector3>();
        List<Color> vertColors = new List<Color>();
        List<int> tris = new List<int>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float y =
                    Mathf.PerlinNoise(transform.position.x + i * perlinScale, transform.position.z + j * perlinScale) *
                    Mathf.PerlinNoise(transform.position.x + i * perlinScale / 2, transform.position.z + j * perlinScale / 2) *
                    Mathf.PerlinNoise(transform.position.x + i * perlinScale / 8, transform.position.z + j * perlinScale / 8) *
                    height *
                    (1 - 8f * Mathf.Sqrt((Mathf.Pow(Mathf.Abs(i - (float) size / 2f) / (float) size, 2) + Mathf.Pow(Mathf.Abs(j - (float) size / 2f) / (float) size, 2))));

                if(y > -1 && (j == 0 || i == 0 || j == size - 1 || i == size - 1))
                {
                    y = -1;
                }

                verts.Add(new Vector3(i + Random.value, y, j + Random.value));
                if (i > 0 && j > 0 && (
                    verts[(i - 1) * size + j - 1].y > -1 || 
                    verts[(i - 1) * size + j].y > -1 ||
                    verts[i * size + j - 1].y > -1 ||
                    verts[i * size + j].y > -1))
                {
                    
                    Color tri1Color = colors[new int[] {
                        Area(verts[(i - 1) * size + j - 1].y),
                        Area(verts[(i - 1) * size + j].y),
                        Area(verts[i * size + j - 1].y)
                    }.Max()];
                    vertColors.Add(tri1Color);
                    vertColors.Add(tri1Color);
                    vertColors.Add(tri1Color);

                    verts2.Add(verts[(i - 1) * size + j - 1] + Vector3.up * Area(verts[(i - 1) * size + j - 1].y));
                    verts2.Add(verts[(i - 1) * size + j] + Vector3.up * Area(verts[(i - 1) * size + j].y));
                    verts2.Add(verts[i * size + j - 1] + Vector3.up * Area(verts[i * size + j - 1].y));

                    tris.Add(verts2.Count - 3);
                    tris.Add(verts2.Count - 2);
                    tris.Add(verts2.Count - 1);
                    
                    Color tri2Color = colors[new int[] {
                        Area(verts[i * size + j].y),
                        Area(verts[(i - 1) * size + j].y),
                        Area(verts[i * size + j - 1].y)
                    }.Max()];

                    vertColors.Add(tri2Color);
                    vertColors.Add(tri2Color);
                    vertColors.Add(tri2Color);

                    verts2.Add(verts[i * size + j] + Vector3.up * Area(verts[i * size + j].y));
                    verts2.Add(verts[(i - 1) * size + j] + Vector3.up * Area(verts[(i - 1) * size + j].y));
                    verts2.Add(verts[i * size + j - 1] + Vector3.up * Area(verts[i * size + j - 1].y));

                    tris.Add(verts2.Count - 1);
                    tris.Add(verts2.Count - 2);
                    tris.Add(verts2.Count - 3);

                    if (verts[i * size + j].y > -1 && 0.5f < Random.value * Mathf.PerlinNoise(-transform.position.x + i * perlinScale * 2, -transform.position.z + j * perlinScale * 2))
                    {
                        switch (Area(verts[i * size + j].y))
                        {
                            case 1:
                            case 2:
                                GameObject.Instantiate(RandomObject(plantObjects), 
                                    transform.position + verts2[verts2.Count - 3], 
                                    Quaternion.Euler(0, Random.value * 360, 0), 
                                    transform);
                                break;
                            case 3:
                                GameObject.Instantiate(RandomObject(rockObjects), 
                                    transform.position + verts2[verts2.Count - 3], 
                                    Quaternion.Euler(0, Random.value * 360, 0), 
                                    transform);
                                break;
                            default:
                                GameObject.Instantiate(RandomObject(beachObjects), 
                                    transform.position + verts2[verts2.Count - 3], 
                                    Quaternion.Euler(0, Random.value * 360, 0), 
                                    transform);
                                break;
                        }
                    }
                }
            }
        }

        mesh.vertices = verts2.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.colors = vertColors.ToArray();
        mesh.RecalculateNormals();

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = islandMat;
        gameObject.AddComponent<MeshCollider>();
    }

    int Area(float height)
    {
        return Mathf.Clamp((int) ((height + 1f) * 2f), 0, colors.Length - 1);
    }

    GameObject RandomObject(GameObject[] objectList)
    {
        return objectList[Mathf.FloorToInt(Random.value * objectList.Length)];
    }

    void GenerateMesh() { }
}