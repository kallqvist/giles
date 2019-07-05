using UnityEngine;
using System.Collections.Generic;

namespace GILES
{
    /**
	 * Manages the current selection.
	 */
#pragma warning disable IDE1006
    public class pb_Selection : pb_MonoBehaviourSingleton<pb_Selection>
	{
#pragma warning restore IDE1006

        protected override void Awake()
		{
			base.Awake();

			Undo.AddUndoPerformedListener( UndoRedoPerformed );
			Undo.AddRedoPerformedListener( UndoRedoPerformed );
		}

		///< Delegate called when a new GameObject is selected.
		/// \sa AddOnSelectionChangeListener
		public event Callback<IEnumerable<GameObject>> OnSelectionChange;

		/**
		 * Event called on objects that were previously selected but now are not.
		 */
		public event Callback<IEnumerable<GameObject>> OnRemovedFromSelection;

		/**
		 * Add a listener for selection changes.
		 */
		public static void AddOnSelectionChangeListener(Callback<IEnumerable<GameObject>> del)
		{
			if(Instance.OnSelectionChange != null)
				Instance.OnSelectionChange += del;
			else
				Instance.OnSelectionChange = del;
		}

		/**
		 * Add a listener for selection removal.
		 */
		public static void AddOnRemovedFromSelectionListener(Callback<IEnumerable<GameObject>> del)
		{
			if(Instance.OnRemovedFromSelection != null)
				Instance.OnRemovedFromSelection += del;
			else
				Instance.OnRemovedFromSelection = del;
		}

		private List<GameObject> _gameObjects = new List<GameObject>();

		/// A list of the currently selected GameObjects.
		public static List<GameObject> GameObjects { get { return Instance._gameObjects; } }

		/**
		 * Clear all objects in the current selection.
		 */
		public static void Clear()
		{			
			if(Instance._gameObjects.Count > 0)
			{
                Instance.OnRemovedFromSelection?.Invoke(Instance._gameObjects);
            }

			int cleared = Instance._Clear();

			if(cleared > 0)
			{
                Instance.OnSelectionChange?.Invoke(null);
            }
		}

		/**
		 * Returns the first gameObject in the selection list.
		 */
		public static GameObject ActiveGameObject
		{
			get
			{
				return Instance._gameObjects.Count > 0 ? Instance._gameObjects[0] : null;
			}
		}

		/**
		 * Clear the selection lists and set them to `selection`.
		 */
		public static void SetSelection(IEnumerable<GameObject> selection)
		{
            Instance.OnRemovedFromSelection?.Invoke(Instance._gameObjects);

            Instance._Clear();

			foreach(GameObject go in selection)
				Instance._AddToSelection(go);

            Instance.OnSelectionChange?.Invoke(selection);
        }

		/**
		 * Clear the selection lists and set them to `selection`.
		 */
		public static void SetSelection(GameObject selection)
		{
			SetSelection(new List<GameObject>() { selection });
		}

		/**
		 * Append `go` to the current selection (doesn't add in the event that it is already in the selection list).
		 */
		public static void AddToSelection(GameObject go)
		{
			Instance._AddToSelection(go);

            Instance.OnSelectionChange?.Invoke(new List<GameObject>() { go });
        }

		/**
		 * Remove an object from the current selection.
		 */
		public static void RemoveFromSelection(GameObject go)
		{
			if(Instance._RemoveFromSelection(go))
			{
                Instance.OnRemovedFromSelection?.Invoke(new List<GameObject>() { go });

                Instance.OnSelectionChange?.Invoke(null);
            }
		}

		private static void UndoRedoPerformed()
		{
            Instance.OnSelectionChange?.Invoke(null);
        }

		/**
		 * Called by code changes to the current selection that should update the pb_SceneEditor in use. 
		 * For example, this is used by the Inspector to update the handles and other scene gizmos when 
		 * changing transform values.
		 */
		public static void OnExternalUpdate()
		{
            Instance.OnSelectionChange?.Invoke(null);
        }

#region Implementation

		private void _InitializeSelected(GameObject go)
		{
			go.AddComponent<pb_SelectionHighlight>();
		}

		private void _DeinitializeSelected(GameObject go)
		{
			pb_SelectionHighlight highlight = go.GetComponent<pb_SelectionHighlight>();

			if(highlight != null)
				pb_ObjectUtility.Destroy(highlight);
		}

		private bool _AddToSelection(GameObject go)
		{
			if(go != null && !_gameObjects.Contains(go))
			{
				_InitializeSelected(go);
				_gameObjects.Add(go);

				return true;
			}
			return false;
		}

		private bool _RemoveFromSelection(GameObject go)
		{
			if(go != null && _gameObjects.Contains(go) )
			{
				pb_ObjectUtility.Destroy(go.GetComponent<pb_SelectionHighlight>());
				_gameObjects.Remove(go);

				return true;
			}

			return false;
		}

		private int _Clear()
		{
			int count = _gameObjects.Count;

			for(int i = 0; i < count; i++)
				_DeinitializeSelected(_gameObjects[i]);
			
			_gameObjects.Clear();

			return count;
		}
#endregion
	}
}
