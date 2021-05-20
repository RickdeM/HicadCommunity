using ISD.BaseTypes;
using ISD.CAD.Base;
using ISD.CAD.Data;
using ISD.Math;

namespace RDM.HicadCommunity
{
	public class SmartPath2D
	{
		public SmartPath2D(Point2D pathStart)
		{
			PathStart = pathStart;
			Path = new Path2D();
		}

		public Point2D PathStart { get; }
		public Path2D Path { get; }

		public bool TestSelfIntersections => Path.TestSelfIntersections;
		public PathProperties PathProperties => Path.PathProperties;
		public Point2D CenterOfGravity => Path.CenterOfGravity;
		public Box3D BoundingBox => Path.BoundingBox;
		public Point2D Start => Path.Start;
		public bool Planar => Path.Planar;
		public Point2D End => Path.End;
		public bool Closed => Path.Closed;
		public ISD.Core.API.Path Implementation => Path.Implementation;
		public bool Empty => Path.Empty;

		public SmartPath2D AddCircle(Point2D first, Point2D second, Point2D third)
		{
			Path.AddCircle(first, second, third);
			return this;
		}

		public SmartPath2D AddCircle(Point2D center, Radius radius)
		{
			Path.AddCircle(center, radius);
			return this;
		}

		public SmartPath2D AddCircleArc(Point2D end, Radius radius, ArcType arcType)
		{
			Path.AddCircleArc(Empty ? PathStart : End, end, radius, arcType);
			return this;
		}

		public SmartPath2D AddClosingSegment()
		{
			Path.AddClosingSegment();
			return this;
		}

		public SmartPath2D AddEllipse(Point2D center, Vector2D xAxis, Vector2D yAxis)
		{
			Path.AddEllipse(center, xAxis, yAxis);
			return this;
		}

		public SmartPath2D AddEllipseArc(Point2D center, Vector2D xAxis, Vector2D yAxis, Point2D end, bool first)
		{
			Path.AddEllipseArc(center, xAxis, yAxis, end, first);
			return this;
		}

		public SmartPath2D AddLineSegment(Vector2D segment)
		{
			Path.AddLineSegment(segment);
			return this;
		}

		public SmartPath2D AddLineSegment(Point2D end)
		{
			if (Empty)
				Path.AddLineSegment(PathStart, end);
			else
				Path.AddLineSegment(end);
			return this;
		}

		public SmartPath2D AddPathSegment(ISD.Core.API.Path aSegment)
		{
			Path.AddPathSegment(aSegment);
			return this;
		}

		public SmartPath2D Chamfer(Angle minimumAngle, Radius radius)
		{
			Path.Chamfer(minimumAngle, radius);
			return this;
		}

		public SmartPath2D Chamfer(NormVector3D normal, Angle minimumAngle, Radius radius, double tolerance)
		{
			Path.Chamfer(normal, minimumAngle, radius, tolerance);
			return this;
		}

		public SmartPath2D Clear()
		{
			Path.Clear();
			return this;
		}

		public IPathInterface Copy() => Path.Copy();

		public CompositeEdge CreateCompositeEdge(AssemblyNode aParent) => Path.CreateCompositeEdge(aParent);

		public CompositeEdge CreateCompositeEdge() => Path.CreateCompositeEdge();

		public Sketch CreateSketch(AssemblyNode aParent) => Path.CreateSketch(aParent);

		public Sketch CreateSketch() => Path.CreateSketch();

		public SmartPath2D Fillet(NormVector3D normal, Angle minimumAngle, Radius radius, double tolerance)
		{
			Path.Fillet(normal, minimumAngle, radius, tolerance);
			return this;
		}

		public SmartPath2D Fillet(Angle minimumAngle, Radius radius)
		{
			Path.Fillet(minimumAngle, radius);
			return this;
		}

		public SmartPath2D Move(Matrix4x4 matrix)
		{
			Path.Move(matrix);
			return this;
		}

		public IPathInterface Split(double par) => Path.Split(par);
	}
}