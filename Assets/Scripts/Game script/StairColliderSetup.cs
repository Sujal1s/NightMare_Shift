using UnityEngine;
using UnityEngine.Tilemaps;

public class StairColliderSetup : MonoBehaviour
{
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();

        TilemapCollider2D tilemapCollider = gameObject.AddComponent<TilemapCollider2D>();
        tilemapCollider.usedByComposite = true;

        CompositeCollider2D compositeCollider = gameObject.AddComponent<CompositeCollider2D>();
        compositeCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }   
}