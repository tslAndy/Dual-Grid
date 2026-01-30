using UnityEngine;
using UnityEngine.Tilemaps;

namespace DualGrid
{
    public class DualGridGenerator : MonoBehaviour
    {
        [SerializeField]
        private Tilemap input;

        [SerializeField]
        private DualGridVault vault;

        [SerializeField]
        private Tilemap[] output;

        private void Start()
        {
            BoundsInt bounds = input.cellBounds;
            for (int y = bounds.yMin - 1; y < bounds.yMax + 1; y++)
            for (int x = bounds.xMin - 1; x < bounds.xMax + 1; x++)
                UpdateCell(x, y);
        }

        public Vector3Int WorldToCell(Vector3 pos) => input.WorldToCell(pos);

        public void SetTile(Vector3Int pos, TileBase tile)
        {
            int x = pos.x;
            int y = pos.y;
            foreach (Tilemap tmap in output)
            {
                for (int ty = y - 1; ty < y + 2; ty++)
                for (int tx = x - 1; tx < x + 2; tx++)
                    tmap.SetTile(new Vector3Int(tx, ty), null);
            }
            input.SetTile(pos, tile);
            for (int ty = y - 1; ty < y + 2; ty++)
            for (int tx = x - 1; tx < x + 2; tx++)
                UpdateCell(tx, ty);
        }

        private void UpdateCell(int x, int y)
        {
            int tile_a = GetGroupIndex(input.GetTile(new Vector3Int(x, y + 1)));
            int tile_b = GetGroupIndex(input.GetTile(new Vector3Int(x + 1, y + 1)));
            int tile_c = GetGroupIndex(input.GetTile(new Vector3Int(x, y)));
            int tile_d = GetGroupIndex(input.GetTile(new Vector3Int(x + 1, y)));

            int ab = tile_a == tile_b ? 1 : 0;
            int ac = tile_a == tile_c ? 1 : 0;
            int ad = tile_a == tile_d ? 1 : 0;
            int bc = tile_b == tile_c ? 1 : 0;
            int bd = tile_b == tile_d ? 1 : 0;
            int cd = tile_c == tile_d ? 1 : 0;

            if ((ab & ac & ad) == 1)
                return;

            TileInfo a = new TileInfo(tile_a, ((1 << 3) | (ab << 2) | (ac << 1) | (ad)) - 1);
            TileInfo b = new TileInfo(tile_b, ((ab << 3) | (1 << 2) | (bc << 1) | (bd)) - 1);
            TileInfo c = new TileInfo(tile_c, ((ac << 3) | (bc << 2) | (1 << 1) | (cd)) - 1);
            TileInfo d = new TileInfo(tile_d, ((ad << 3) | (bd << 2) | (cd << 1) | (1)) - 1);

            if (a.id > b.id)
                (a, b) = (b, a);
            if (b.id > c.id)
                (b, c) = (c, b);
            if (c.id > d.id)
                (c, d) = (d, c);
            if (a.id > b.id)
                (a, b) = (b, a);
            if (b.id > c.id)
                (b, c) = (c, b);
            if (a.id > b.id)
                (a, b) = (b, a);

            int i = 0;
            Vector3Int pos = new Vector3Int(x, y);
            if (b.id != -1 && b.id != c.id && (b.id > a.id))
            {
                TileBase tile = vault.groups[b.id].tiles[b.layout];
                output[i++].SetTile(pos, tile);
            }
            if (c.id != -1 && c.id != d.id && (c.id > a.id || c.id > b.id))
            {
                TileBase tile = vault.groups[c.id].tiles[c.layout];
                output[i++].SetTile(pos, tile);
            }
            if (d.id != -1)
            {
                TileBase tile = vault.groups[d.id].tiles[d.layout];
                output[i++].SetTile(pos, tile);
            }
        }

        private int GetGroupIndex(TileBase tile)
        {
            for (int i = 0; i < vault.groups.Length; i++)
            {
                if (vault.groups[i].baseTile == tile)
                    return i;
            }

            return -1;
        }

        private readonly struct TileInfo
        {
            public readonly int id,
                layout;

            public TileInfo(int id, int layout)
            {
                this.id = id;
                this.layout = layout;
            }
        }
    }
}
