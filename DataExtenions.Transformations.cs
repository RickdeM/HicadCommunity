/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using ISD.BaseTypes;
using ISD.CAD.Data;
using ISD.Math;

namespace RDM.HicadCommunity
{
	public static partial class DataExtenions
	{
		/// <summary>
		/// Rotate a Node
		/// </summary>
		/// <param name="coor">Node to be rotated</param>
		/// <param name="angle">Angle in degrees</param>
		/// <param name="vector">Rotating vector (angle is CCW, rotates from X-axis to Y-axis), default axis is Z</param>
		/// <param name="point">Rotating point, default Point is 0,0,0</param>
		/// <returns></returns>
		public static CoordinateSystem Rotate(this CoordinateSystem coor, Angle angle, NormVector3D? vector = null, Point3D? point = null)
		{
			var tfs = new Transformation();
			tfs.SetRotation(point ?? new Point3D(), vector ?? NormVector3D.E3, angle);
			coor.Transform(tfs);
			return coor;
		}

		/// <summary>
		/// Rotate a Node
		/// </summary>
		/// <param name="node">Node to be rotated</param>
		/// <param name="angle">Angle in degrees</param>
		/// <param name="vector">Rotating vector (angle is CCW, rotates from X-axis to Y-axis), default axis is Z</param>
		/// <param name="point">Rotating point, default Point is 0,0,0</param>
		/// <returns></returns>
		public static T Rotate<T>(this T node, Angle angle, NormVector3D? vector = null, Point3D? point = null) where T : Node
		{
			// Create Transformation
			var tfs = new Transformation();
			tfs.SetRotation(point ?? new Point3D(), vector ?? NormVector3D.E3, angle);
			// Transform the Node
			tfs.Apply(node);
			// Return the node
			return node;
		}

		/// <summary>
		/// Move a node given two points
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		/// <returns></returns>
		public static T Move<T>(this T node, Point3D start, Point3D end) where T : Node => node.Move(new Vector3D(start, end));

		/// <summary>
		/// Move a node by the given vector
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="vec">Vector used for transformation</param>
		/// <returns></returns>
		public static T Move<T>(this T node, Vector3D vec) where T : Node
		{
			// Move the Node
			new Transformation(vec).Apply(node);
			// Return the Node
			return node;
		}

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start WorkingPlane for transformation</param>
		/// <param name="end">End WorkingPlane for transformation</param>
		/// <returns></returns>
		public static T Move<T>(this T node, WorkingPlane start, WorkingPlane end) where T : Node => node.Move(start.CoordinateSystem, end.CoordinateSystem);

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for transformation</param>
		/// <returns></returns>
		public static T Move<T>(this T node, CoordinateSystem start, WorkingPlane end) where T : Node => node.Move(start, end.CoordinateSystem);

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for transformation</param>
		/// <returns></returns>
		public static T Move<T>(this T node, WorkingPlane start, CoordinateSystem end) where T : Node => node.Move(start.CoordinateSystem, end);

		/// <summary>
		/// Move a Node using CoordinationSystems
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for transformation</param>
		/// <returns></returns>
		public static T Move<T>(this T node, CoordinateSystem start, CoordinateSystem end) where T : Node
		{
			// Move the Node
			start.BaseTransformation(end).Apply(node);
			// Return the Node
			return node;
		}

		/// <summary>
		/// Move the CoordinateSystem
		/// </summary>
		/// <param name="coor">Coor to be moved</param>
		/// <param name="vec">Vector for transformation</param>
		/// <returns></returns>
		public static CoordinateSystem Move(this CoordinateSystem coor, Vector3D vec)
		{
			coor.Move(new Transformation(vec));
			return coor;
		}

		/// <summary>
		/// Move the CoordinateSystem
		/// </summary>
		/// <param name="coor">Coor to be moved</param>
		/// <param name="firstPoint">First point for Vector creation</param>
		/// <param name="secondPoint">Second point for Vector creation</param>
		/// <returns></returns>
		public static CoordinateSystem Move(this CoordinateSystem coor, Point3D firstPoint, Point3D secondPoint) => coor.Move(new Vector3D(firstPoint, secondPoint));

		/// <summary>
		/// Move the CoordinateSystem
		/// </summary>
		/// <param name="coor">Coor to be moved</param>
		/// <param name="endPoint">End point for Vector creation, start=0,0,0</param>
		/// <returns></returns>
		public static CoordinateSystem Move(this CoordinateSystem coor, Point3D endPoint) => coor.Move(new Vector3D(new Point3D(), endPoint));
	}
}