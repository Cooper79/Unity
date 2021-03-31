using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemBody : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Collider _itemCollider;
    private Item2 _item;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _itemCollider = GetComponent<Collider>();
    }

    public void Init(Mesh mesh, Material material, Item2 item)
    {
        _meshRenderer.material = material;
        _meshFilter.mesh = mesh;
        _itemCollider = gameObject.AddComponent<Collider>();
        _item = item;
    }

    public void OnPickUp(Player_Creature player)
    {
        Destroy(gameObject);
    }
}
