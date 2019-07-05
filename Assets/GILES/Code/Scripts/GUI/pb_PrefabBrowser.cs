using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GILES.Interface
{
#pragma warning disable IDE1006
    public class pb_PrefabBrowser : MonoBehaviour
	{
#pragma warning restore IDE1006
        List<GameObject> prefabs;

		void Start()
		{
			prefabs = pb_ResourceManager.LoadAll<GameObject>().ToList();

			foreach(GameObject go in prefabs)
			{
				GameObject icon = transform.gameObject.AddChild();
				
				pb_PrefabBrowserItemButton button = icon.AddComponent<pb_PrefabBrowserItemButton>();
				button.asset = go;
				button.Initialize();
			}
		}

	}
}