using UnityEngine;
using System.Runtime.Serialization;

namespace GILES
{
	[System.Serializable]
#pragma warning disable IDE1006
    public class pb_Transform : System.IEquatable<pb_Transform>, ISerializable
	{
#pragma warning restore IDE1006

        // If the matrix needs rebuilt, this will be true.  Used to delay expensive
        // matrix construction til necessary (since t/r/s can change a lot before a
        // matrix is needed).
        private bool dirty = true;

		[SerializeField] private Vector3 _position;
		[SerializeField] private Quaternion _rotation;
		[SerializeField] private Vector3 _scale;

		private Matrix4x4 matrix;

		public Vector3 Position 		{ get { return _position; } set { dirty = true; _position = value; } }
		public Quaternion Rotation 		{ get { return _rotation; } set { dirty = true; _rotation = value; } }
		public Vector3 Scale 			{ get { return _scale; } set { dirty = true; _scale = value; } }

		public static readonly pb_Transform identity = new pb_Transform(Vector3.zero, Quaternion.identity, Vector3.one);

		public pb_Transform()
		{
			this.Position = Vector3.zero;
			this.Rotation = Quaternion.identity;
			this.Scale = Vector3.one;
			this.matrix = Matrix4x4.identity;
			this.dirty = false;
		}

		public pb_Transform(Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.Position 	= position;
			this.Rotation 	= rotation;
			this.Scale		= scale;

			this.matrix 	= Matrix4x4.TRS(position, rotation, scale);
			this.dirty 	= false;
		}

		public pb_Transform(Transform transform)
		{
			this.Position 	= transform.position;
			this.Rotation 	= transform.localRotation;
			this.Scale		= transform.localScale;

			this.matrix 	= Matrix4x4.TRS(Position, Rotation, Scale);
			this.dirty 	= false;
		}

		public pb_Transform(pb_Transform transform)
		{
			this.Position 	= transform.Position;
			this.Rotation 	= transform.Rotation;
			this.Scale		= transform.Scale;

			this.matrix 	= Matrix4x4.TRS(Position, Rotation, Scale);
			this.dirty 	= false;
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("position", (Vector3)_position, typeof(Vector3));
			info.AddValue("rotation", (Quaternion)_rotation, typeof(Quaternion));
			info.AddValue("scale", (Vector3)_scale, typeof(Vector3));
		}

		public pb_Transform(SerializationInfo info, StreamingContext context)
		{
			this._position = (Vector3) info.GetValue("position", typeof(Vector3));
			this._rotation = (Quaternion) info.GetValue("rotation", typeof(Quaternion));
			this._scale = (Vector3) info.GetValue("scale", typeof(Vector3));
			this.dirty = true;
		}

		public void SetTRS(Transform trs)
		{
			this.Position 	= trs.position;
			this.Rotation 	= trs.localRotation;
			this.Scale		= trs.localScale;
			this.dirty 		= true;
		}

		bool Approx(Vector3 lhs, Vector3 rhs)
		{
			return 	Mathf.Abs(lhs.x - rhs.x) < Mathf.Epsilon &&
					Mathf.Abs(lhs.y - rhs.y) < Mathf.Epsilon &&
					Mathf.Abs(lhs.z - rhs.z) < Mathf.Epsilon;
		}

		bool Approx(Quaternion lhs, Quaternion rhs)
		{
			return 	Mathf.Abs(lhs.x - rhs.x) < Mathf.Epsilon &&
					Mathf.Abs(lhs.y - rhs.y) < Mathf.Epsilon &&
					Mathf.Abs(lhs.z - rhs.z) < Mathf.Epsilon &&
					Mathf.Abs(lhs.w - rhs.w) < Mathf.Epsilon;
		}

		public bool Equals(pb_Transform rhs)
		{
			return 	Approx(this.Position, rhs.Position) &&
					Approx(this.Rotation, rhs.Rotation) &&
					Approx(this.Scale, rhs.Scale);
		}

		public override bool Equals(System.Object rhs)
		{
			return rhs is pb_Transform && this.Equals( (pb_Transform) rhs );
		}

		public override int GetHashCode()
		{
			return Position.GetHashCode() ^ Rotation.GetHashCode() ^ Scale.GetHashCode();
		}

		public Matrix4x4 GetMatrix()
		{
			if( !this.dirty )
			{
				return matrix;
			}
			else
			{
				this.dirty = false;
				matrix = Matrix4x4.TRS(Position, Rotation, Scale);
				return matrix;
			}
		}

		public static pb_Transform operator - (pb_Transform lhs, pb_Transform rhs)
		{
            pb_Transform t = new pb_Transform
            {
                Position = lhs.Position - rhs.Position,
                Rotation = Quaternion.Inverse(rhs.Rotation) * lhs.Rotation,
                Scale = new Vector3(lhs.Scale.x / rhs.Scale.x,
                                    lhs.Scale.y / rhs.Scale.y,
                                    lhs.Scale.z / rhs.Scale.z)
            };

            return t;
		}

		public static pb_Transform operator + (pb_Transform lhs, pb_Transform rhs)
		{
            pb_Transform t = new pb_Transform
            {
                Position = lhs.Position + rhs.Position,
                Rotation = lhs.Rotation * rhs.Rotation,
                Scale = new Vector3(lhs.Scale.x * rhs.Scale.x,
                                    lhs.Scale.y * rhs.Scale.y,
                                    lhs.Scale.z * rhs.Scale.z)
            };

            return t;
		}

		public static pb_Transform operator + (Transform lhs, pb_Transform rhs)
		{
            pb_Transform t = new pb_Transform
            {
                Position = lhs.position + rhs.Position,
                Rotation = lhs.localRotation * rhs.Rotation,
                Scale = new Vector3(lhs.localScale.x * rhs.Scale.x,
                                    lhs.localScale.y * rhs.Scale.y,
                                    lhs.localScale.z * rhs.Scale.z)
            };

            return t;
		}

		public static bool operator == (pb_Transform lhs, pb_Transform rhs)
		{
			return System.Object.ReferenceEquals(lhs, rhs) || lhs.Equals(rhs);
		}

		public static bool operator != (pb_Transform lhs, pb_Transform rhs)
		{
			return !(lhs == rhs);
		}

		public Vector3 Up { get { return Rotation * Vector3.up; }	}
		public Vector3 Forward { get { return Rotation * Vector3.forward; }	}
		public Vector3 Right { get { return Rotation * Vector3.right; }	}

		public override string ToString()
		{
			return Position.ToString("F2") + "\n" + Rotation.ToString("F2") + "\n" + Scale.ToString("F2");
		}
	}
}