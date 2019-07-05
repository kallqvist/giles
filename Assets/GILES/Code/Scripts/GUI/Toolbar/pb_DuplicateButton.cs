using UnityEngine;
using System.Collections.Generic;

namespace GILES
{
#pragma warning disable IDE1006
    public class pb_DuplicateButton : pb_ToolbarButton
	{
#pragma warning restore IDE1006

        protected override void Start()
		{
			base.Start();

			pb_Selection.AddOnSelectionChangeListener( OnSelectionChanged );
			OnSelectionChanged(null);
		}

		public override string Tooltip { get { return "Duplicate Selection"; } }

		public void DoDuplicate()
		{
			List<GameObject> newObjects = new List<GameObject>();
			List<IUndo> undo = new List<IUndo>() { new UndoSelection() };

			foreach(GameObject go in pb_Selection.GameObjects)
			{
				GameObject inst = (GameObject) pb_Scene.Instantiate(go);
				newObjects.Add(inst);
				undo.Add(new UndoInstantiate(inst));
			}

			Undo.RegisterStates(undo, "Duplicate Object");

			pb_Selection.SetSelection(newObjects);
		}

		private void OnSelectionChanged(IEnumerable<GameObject> go)
		{
			Interactable = pb_Selection.ActiveGameObject != null;
		}
	}
}