using UnityEngine;
using System.Collections;

namespace GILES
{
    /**
     *	A selection handle for translation, rotation, and scale.
     *  @todo Document and clean up
     */

#pragma warning disable IDE1006
    public class pb_SelectionHandle : pb_MonoBehaviourSingleton<pb_SelectionHandle>
{
#pragma warning restore IDE1006

#region Member

    public static float positionSnapValue = 1f;
	public static float scaleSnapValue = .1f;
	public static float rotationSnapValue = 15f;

    private Transform _trs;
    private Transform Trs { get { if(_trs == null) _trs = gameObject.GetComponent<Transform>(); return _trs; } }

    private Camera _cam;
	private Camera Cam { get { if(_cam == null) _cam = Camera.main; return _cam; } }

    const int MAX_DISTANCE_TO_HANDLE = 15;

	static Mesh _HandleLineMesh = null, _HandleTriangleMesh = null;

	public Mesh ConeMesh;	// Used as translation handle cone caps.
	public Mesh CubeMesh;	// Used for scale handle

	private Material HandleOpaqueMaterial
	{
		get
		{
			return pb_BuiltinResource.GetMaterial(pb_BuiltinResource.mat_HandleOpaque);
		}
	}

	private Material RotateLineMaterial 
	{
		get
		{
			return pb_BuiltinResource.GetMaterial(pb_BuiltinResource.mat_RotateHandle);
		}
	}

	private Material HandleTransparentMaterial
	{
		get
		{
			return pb_BuiltinResource.GetMaterial(pb_BuiltinResource.mat_HandleTransparent);
		}
	}

	private Mesh HandleLineMesh
	{
		get
		{
			if(_HandleLineMesh == null)
			{
				_HandleLineMesh = new Mesh();
				CreateHandleLineMesh(ref _HandleLineMesh, Vector3.one);
			}
			return _HandleLineMesh;
		}
	}

	private Mesh HandleTriangleMesh
	{
		get
		{
			if(_HandleTriangleMesh == null)
			{
				_HandleTriangleMesh = new Mesh();
				 CreateHandleTriangleMesh(ref _HandleTriangleMesh, Vector3.one);
			}
			return _HandleTriangleMesh;
		}
	}

	private Tool tool = Tool.Position;	// Current tool.
		
	private readonly Mesh _coneRight, _coneUp, _coneForward;

	const float CAP_SIZE = .07f;

	public float HandleSize = 90f;

	public Callback onHandleTypeChanged = null;

	private Vector2 mouseOrigin = Vector2.zero;
	public bool DraggingHandle { get; private set; }
	private int draggingAxes = 0;	// In how many directions is the handle able to move
	private Vector3 scale = Vector3.one;
	private pb_Transform handleOrigin = pb_Transform.identity;

	public bool IsHidden { get; private set; }
	public bool InUse() { return DraggingHandle; }

#endregion

#region Initialization

	protected override void Awake()
	{
		_trs = null;
		_cam = null;
		base.Awake();
	}

	void Start()
	{
		pb_SceneCamera.AddOnCameraMoveDelegate(this.OnCameraMove);

		SetIsHidden(true);

	}

	public void SetTRS(Vector3 position, Quaternion rotation, Vector3 scale)
	{
		Trs.position = position;
		Trs.localRotation = rotation;
		Trs.localScale = scale;

		RebuildGizmoMatrix();
	}
#endregion

#region Delegate

	public delegate void OnHandleMoveEvent(pb_Transform transform);
	public event OnHandleMoveEvent OnHandleMove;

	public delegate void OnHandleBeginEvent(pb_Transform transform);
	public event OnHandleBeginEvent OnHandleBegin;

	public delegate void OnHandleFinishEvent();
	public event OnHandleFinishEvent OnHandleFinish;

	void OnCameraMove(pb_SceneCamera cam)
	{
		RebuildGizmoMesh(Vector3.one);
		RebuildGizmoMatrix();
	}
#endregion

#region Update

	class DragOrientation
	{
		public Vector3 origin;
		public Vector3 axis;
		public Vector3 mouse;
		public Vector3 cross;
		public Vector3 offset;
		public Plane plane;

		public DragOrientation()
		{
			origin	= Vector3.zero;
			axis	= Vector3.zero;
			mouse	= Vector3.zero;
			cross	= Vector3.zero;
			offset	= Vector3.zero;
			plane	= new Plane(Vector3.up, Vector3.zero);
		}
	}

	DragOrientation drag = new DragOrientation();
	
	void Update()
	{
		if( IsHidden )
		{
			return;
		}

		if( Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftAlt))
		{
			OnMouseDown();
		}

		if( DraggingHandle )
		{
			if( Input.GetKey(KeyCode.LeftAlt) )
				OnFinishHandleMovement();

#if DEBUG
			Vector3 dir = drag.axis * 2f;
			Color col = new Color(Mathf.Abs(drag.axis.x), Mathf.Abs(drag.axis.y), Mathf.Abs(drag.axis.z), 1f);
			float lineTime = 0f;
			Debug.DrawRay(drag.origin, dir * 1f, col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (Trs.up * .1f), col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (Trs.forward * .1f), col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (Trs.right * .1f), col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (-Trs.up * .1f), col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (-Trs.forward * .1f), col, lineTime, false);
			Debug.DrawLine(drag.origin + dir, (drag.origin + dir * .9f) + (-Trs.right * .1f), col, lineTime, false);

			Debug.DrawLine(drag.origin, drag.origin + drag.mouse, Color.red, lineTime, false);
			Debug.DrawLine(drag.origin, drag.origin + drag.cross, Color.black, lineTime, false);
#endif
			}

		if( Input.GetMouseButton(0) && DraggingHandle )
		{
			Vector3 a = Vector3.zero;

			bool valid = false;

			if( draggingAxes < 2 && tool != Tool.Rotate )
			{
				valid = pb_HandleUtility.PointOnLine( new Ray(Trs.position, drag.axis), Cam.ScreenPointToRay(Input.mousePosition), out a, out Vector3 b);
			}
			else
			{
				Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
				if( drag.plane.Raycast(ray, out float hit) )
				{
					a = ray.GetPoint(hit);
					valid = true;
				}
			}

			if( valid )
			{
				drag.origin = Trs.position;

				switch( tool )
				{
					case Tool.Position:
					{
						Trs.position = pb_Snap.Snap(a - drag.offset, positionSnapValue);
					}
					break;

					case Tool.Rotate:
					{
						Vector2 delta = (Vector2)Input.mousePosition - mouseOrigin;
						mouseOrigin = Input.mousePosition;
						float sign = pb_HandleUtility.CalcMouseDeltaSignWithAxes(Cam, drag.origin, drag.axis, drag.cross, delta);
						axisAngle += delta.magnitude * sign;
						Trs.localRotation = Quaternion.AngleAxis(pb_Snap.Snap(axisAngle, rotationSnapValue), drag.axis) * handleOrigin.Rotation;// trs.localRotation;
					}
					break;

					case Tool.Scale:
					{
						Vector3 v;

						if(draggingAxes > 1)
						{
							v = SetUniformMagnitude( ((a - drag.offset) - Trs.position) );
						}
						else
						{
							v = Quaternion.Inverse(handleOrigin.Rotation) * ((a - drag.offset) - Trs.position);
						}

						v += Vector3.one;
						scale = pb_Snap.Snap(v, scaleSnapValue);
						RebuildGizmoMesh( scale );
					}
					break;
				}

                OnHandleMove?.Invoke(GetTransform());

                RebuildGizmoMatrix();
			}
		}

		if( Input.GetMouseButtonUp(0) )
		{
			OnFinishHandleMovement();
		}
	}

	float axisAngle = 0f;

	/**
	 * Sets all the components of a vector to the component with the largest magnitude.
	 */
	Vector3 SetUniformMagnitude(Vector3 a)
	{
		float max = Mathf.Abs(a.x) > Mathf.Abs(a.y) && Mathf.Abs(a.x) > Mathf.Abs(a.z) ? a.x : Mathf.Abs(a.y) > Mathf.Abs(a.z) ? a.y : a.z;

		a.x = max;
		a.y = max;
		a.z = max;

		return a;
	}

	void OnMouseDown()
	{
		scale = Vector3.one;

		drag.offset = Vector3.zero;		

		axisAngle = 0f;

		DraggingHandle = CheckHandleActivated(Input.mousePosition, out Axis plane);

		mouseOrigin = Input.mousePosition;
		handleOrigin.SetTRS(Trs);

		drag.axis = Vector3.zero;
		draggingAxes = 0;

		if( DraggingHandle )
		{
			Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

			if( (plane & Axis.X) == Axis.X )
			{
				draggingAxes++;
				drag.axis = Trs.right;
				drag.plane.SetNormalAndPosition(Trs.right.normalized, Trs.position);
			}

			if( (plane & Axis.Y) == Axis.Y )
			{
				draggingAxes++;
				if(draggingAxes > 1)
					drag.plane.SetNormalAndPosition(Vector3.Cross(drag.axis, Trs.up).normalized, Trs.position);
				else
					drag.plane.SetNormalAndPosition(Trs.up.normalized, Trs.position);
				drag.axis += Trs.up;
			}

			if( (plane & Axis.Z) == Axis.Z )
			{
				draggingAxes++;
				if(draggingAxes > 1)
					drag.plane.SetNormalAndPosition(Vector3.Cross(drag.axis, Trs.forward).normalized, Trs.position);
				else
					drag.plane.SetNormalAndPosition(Trs.forward.normalized, Trs.position);
				drag.axis += Trs.forward;
			}

			if( draggingAxes < 2 )
			{
				if( pb_HandleUtility.PointOnLine(new Ray(Trs.position, drag.axis), ray, out Vector3 a, out Vector3 b) )
					drag.offset = a - Trs.position;

				if( drag.plane.Raycast(ray, out float hit) )
				{
					drag.mouse = (ray.GetPoint(hit) - Trs.position).normalized;
					drag.cross = Vector3.Cross(drag.axis, drag.mouse);
				}
			}
			else
			{
				if( drag.plane.Raycast(ray, out float hit) )
				{
					drag.offset = ray.GetPoint(hit) - Trs.position;
					drag.mouse = (ray.GetPoint(hit) - Trs.position).normalized;
					drag.cross = Vector3.Cross(drag.axis, drag.mouse);
				}
			}

            OnHandleBegin?.Invoke(GetTransform());
        }
	}

	void OnFinishHandleMovement()
	{
		RebuildGizmoMesh(Vector3.one);
		RebuildGizmoMatrix();

        OnHandleFinish?.Invoke();

        StartCoroutine( SetDraggingFalse() );
	}

	IEnumerator SetDraggingFalse()
	{
		yield return new WaitForEndOfFrame();
		DraggingHandle = false;
	}
#endregion

#region Interface

	Vector2 ScreenToGUIPoint(Vector2 v)
	{
		v.y = Screen.height - v.y;
		return v;
	}

	public pb_Transform GetTransform()
	{
		return new pb_Transform(
			Trs.position,
			Trs.localRotation,
			scale);
	}

	bool CheckHandleActivated(Vector2 mousePosition, out Axis plane)
	{
		plane = (Axis)0x0;

		if( tool == Tool.Position || tool == Tool.Scale )
		{
			float sceneHandleSize = pb_HandleUtility.GetHandleSize(Trs.position);

			// cen
			Vector2 cen = Cam.WorldToScreenPoint( Trs.position );

			// up
			Vector2 up = Cam.WorldToScreenPoint( (Trs.position + (Trs.up + Trs.up * CAP_SIZE * 4f) * (sceneHandleSize * HandleSize)) );

			// right
			Vector2 right = Cam.WorldToScreenPoint( (Trs.position + (Trs.right + Trs.right * CAP_SIZE * 4f) * (sceneHandleSize * HandleSize)) );

			// forward
			Vector2 forward = Cam.WorldToScreenPoint( (Trs.position + (Trs.forward + Trs.forward * CAP_SIZE * 4f) * (sceneHandleSize * HandleSize)) );

			// First check if the plane boxes have been activated

			Vector3 cameraMask = pb_HandleUtility.DirectionMask(Trs, Cam.transform.forward);

			Vector2 p_right = (cen + ((right-cen) * cameraMask.x) * HANDLE_BOX_SIZE);
			Vector2 p_up = (cen + ((up-cen) * cameraMask.y) * HANDLE_BOX_SIZE);
			Vector2 p_forward = (cen + ((forward-cen) * cameraMask.z) * HANDLE_BOX_SIZE);

			// x plane
			if( pb_HandleUtility.PointInPolygon( new Vector2[] {
				cen, p_up,
				p_up, (p_up+p_forward) - cen,
				(p_up+p_forward) - cen, p_forward,
				p_forward, cen
				}, mousePosition ) )
				plane = Axis.Y | Axis.Z;
			// y plane
			else if( pb_HandleUtility.PointInPolygon( new Vector2[] {
				cen, p_right,
				p_right, (p_right+p_forward)-cen,
				(p_right+p_forward)-cen, p_forward,
				p_forward, cen
				}, mousePosition ) )
				plane = Axis.X | Axis.Z;
			// z plane
			else if( pb_HandleUtility.PointInPolygon( new Vector2[] {
				cen, p_up,
				p_up, (p_up + p_right) - cen,
				(p_up + p_right) - cen, p_right,
				p_right, cen
				}, mousePosition ) )
				plane = Axis.X | Axis.Y;
			else
			if(pb_HandleUtility.DistancePointLineSegment(mousePosition, cen, up) < MAX_DISTANCE_TO_HANDLE)
				plane = Axis.Y;
			else if(pb_HandleUtility.DistancePointLineSegment(mousePosition, cen, right) < MAX_DISTANCE_TO_HANDLE)
				plane = Axis.X;
			else if(pb_HandleUtility.DistancePointLineSegment(mousePosition, cen, forward) < MAX_DISTANCE_TO_HANDLE)
				plane = Axis.Z;
			else
				return false;

			return true;
		}
		else
		{
			Vector3[][] vertices = pb_HandleMesh.GetRotationVertices(16, 1f);

			float best = Mathf.Infinity;

			Vector2 cur, prev = Vector2.zero;
			plane = Axis.X;

			for(int i = 0; i < 3; i++)
			{
				cur = Cam.WorldToScreenPoint(vertices[i][0]);

				for(int n = 0; n < vertices[i].Length-1; n++)
				{
					prev = cur;
					cur = Cam.WorldToScreenPoint( handleMatrix.MultiplyPoint3x4(vertices[i][n+1]) );

					float dist = pb_HandleUtility.DistancePointLineSegment(mousePosition, prev, cur);

					if( dist < best && dist < MAX_DISTANCE_TO_HANDLE )
					{
						Vector3 viewDir = (handleMatrix.MultiplyPoint3x4((vertices[i][n] + vertices[i][n+1]) * .5f) - Cam.transform.position).normalized;
						Vector3 nrm = transform.TransformDirection(vertices[i][n]).normalized;

						if(Vector3.Dot(nrm, viewDir) > .5f)
							continue;

						best = dist;

						switch(i)
						{
							case 0: // Y
								plane = Axis.Y; // Axis.X | Axis.Z;
								break;

							case 1:	// Z
								plane = Axis.Z;// Axis.X | Axis.Y;
								break;

							case 2:	// X
								plane = Axis.X;// Axis.Y | Axis.Z;
								break;
						}
					}
				}
			}

			if( best < MAX_DISTANCE_TO_HANDLE + .1f)
			{
				return true;
			}
		}

		return false;
	}
#endregion

#region Render

	private Matrix4x4 handleMatrix;

	void OnRenderObject()
	{	
		if(IsHidden || Camera.current != Cam)
			return;

		switch(tool)
		{
			case Tool.Position:
			case Tool.Scale:
				HandleOpaqueMaterial.SetPass(0);
				Graphics.DrawMeshNow(HandleLineMesh, handleMatrix);
				Graphics.DrawMeshNow(HandleTriangleMesh, handleMatrix, 1);	// Cones

				HandleTransparentMaterial.SetPass(0);
				Graphics.DrawMeshNow(HandleTriangleMesh, handleMatrix, 0);	// Box
				break;

			case Tool.Rotate:
				RotateLineMaterial.SetPass(0);
				Graphics.DrawMeshNow(HandleLineMesh, handleMatrix);
				break;
		}
	}

	void RebuildGizmoMatrix()
	{
		float handleSize = pb_HandleUtility.GetHandleSize(Trs.position);
		Matrix4x4 scale = Matrix4x4.Scale(Vector3.one * handleSize * HandleSize);

 		handleMatrix = transform.localToWorldMatrix * scale;
 	}

	void RebuildGizmoMesh(Vector3 scale)
	{
		if(_HandleLineMesh == null)
			_HandleLineMesh = new Mesh();

		if(_HandleTriangleMesh == null)
			_HandleTriangleMesh = new Mesh();

		CreateHandleLineMesh(ref _HandleLineMesh, scale);
		CreateHandleTriangleMesh(ref _HandleTriangleMesh, scale);
	}
#endregion

#region Set Functionality

	public void SetTool(Tool tool)
	{
		if(this.tool != tool)	
		{
			this.tool = tool;
			RebuildGizmoMesh(Vector3.one);

            onHandleTypeChanged?.Invoke();
        }
	}

	public Tool GetTool()
	{
		return tool;
	}

	public void SetIsHidden(bool isHidden)
	{
		DraggingHandle = false;
		this.IsHidden = isHidden;

        onHandleTypeChanged?.Invoke();
    }

	public bool GetIsHidden()
	{
		return this.IsHidden;
	}

#endregion

#region Mesh Generation

	const float HANDLE_BOX_SIZE = .25f;

	private void CreateHandleLineMesh(ref Mesh mesh, Vector3 scale)
	{
		switch(tool)
		{
			case Tool.Position:
			case Tool.Scale:
				pb_HandleMesh.CreatePositionLineMesh(ref mesh, Trs, scale, Cam, HANDLE_BOX_SIZE);
				break;

			case Tool.Rotate:
				pb_HandleMesh.CreateRotateMesh(ref mesh, 48, 1f);
				break;

			default:
				return;
		}
	}

	private void CreateHandleTriangleMesh(ref Mesh mesh, Vector3 scale)
	{
		if( tool == Tool.Position )
			pb_HandleMesh.CreateTriangleMesh(ref mesh, Trs, scale, Cam, ConeMesh, HANDLE_BOX_SIZE, CAP_SIZE);
		else if( tool == Tool.Scale )
			pb_HandleMesh.CreateTriangleMesh(ref mesh, Trs, scale, Cam, CubeMesh, HANDLE_BOX_SIZE, CAP_SIZE);
	}

#endregion
}
}
