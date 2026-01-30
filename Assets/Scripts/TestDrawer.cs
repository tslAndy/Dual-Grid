using System.Collections;
using System.Collections.Generic;
using DualGrid;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestDrawer : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private DualGridGenerator dualGrid;

    private TileBase tile;

    public void SetTile(TileBase tile) => this.tile = tile;

    private void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;

        dualGrid.SetTile(dualGrid.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), tile);
    }
}
