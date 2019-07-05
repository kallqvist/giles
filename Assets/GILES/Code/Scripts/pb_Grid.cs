/**
 * Renders a grid at 0,0,0.
 */

using UnityEngine;

namespace GILES
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
#pragma warning disable IDE1006
    public class pb_Grid : MonoBehaviour
	{
#pragma warning restore IDE1006
        [Tooltip("This script generates a grid mesh on load.")]

		// Default to 10x10 grid
		public int lines = 10;
		
		// 1m * scale
		public float scale = 1f;
		public Color gridColor = new Color(.5f, .5f, .5f, .6f);
		public Material gridMaterial;

		void Start()
		{
			GetComponent<MeshFilter>().sharedMesh = GridMesh(lines, scale);
			GetComponent<MeshRenderer>().sharedMaterial = gridMaterial;
			GetComponent<MeshRenderer>().sharedMaterial.color = gridColor;

			transform.position = Vector3.zero;
		}

		/**
		 * Builds a grid object in 2d space
		 */
		Mesh GridMesh(int lineCount, float scale)
		{
			float half = (lineCount/2f) * scale;

			lineCount++;	// to make grid lines equal and such

			Vector3[] lines = new Vector3[lineCount * 4];	// 2 vertices per line, 2 * lines per grid
			Vector3[] normals = new Vector3[lineCount * 4];
			Vector2[] uv = new Vector2[lineCount * 4];
			int[] indices = new int[lineCount * 4];

			int n = 0;
			for(int y = 0; y < lineCount; y++)
			{
				indices[n] = n;
				uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
				lines[n++] = new Vector3( y * scale - half, 0f, -half );

				indices[n] = n;
				uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
				lines[n++] = new Vector3( y * scale - half, 0f,  half );

				indices[n] = n;
				uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
				lines[n++] = new Vector3( -half, 0f, y * scale - half );

				indices[n] = n;
				uv[n] = y % 10 == 0 ? Vector2.one : Vector2.zero;
				lines[n++] = new Vector3(  half, 0f, y * scale - half );
			}		

			for(int i = 0; i < lines.Length; i++)
			{
				normals[i] = Vector3.up;
			}

            Mesh tm = new Mesh
            {
                name = "GridMesh",
                vertices = lines,
                normals = normals,
                subMeshCount = 1
            };
            tm.SetIndices(indices, MeshTopology.Lines, 0);
			tm.uv = uv;

			return tm;
		}
	}
}
