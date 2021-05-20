using HiCAD.Data;
using RDM.HicadCommunity.Helpers;
using ISD.CAD.Contexts;
using ISD.CAD.Data;
using ISD.Core.API;
using ISD.Math;
using ISD.Scripting;
using System;
using System.Linq;

namespace RDM.HicadCommunity
{
	public class PathWalker : PropertyChangedHandler
	{
		private TrulyObservableCollection<PathSegment> sections = new TrulyObservableCollection<PathSegment>();
		private CompositeEdgeImpl sketch;

		public PathWalker()
		{
		}

		public bool IsPathClosed => path != null && path.GeometricallyClosed;
		public Path path { get; private set; }

		public TrulyObservableCollection<PathSegment> Sections
		{
			get => sections;
			set
			{
				sections = value;
				NotifyPropertyChanged(nameof(Sections));
			}
		}

		public CompositeEdgeImpl Sketch
		{
			get => sketch;

			set
			{
				if (value.Paths.Count() > 1)
					throw new Exception("3D sketch contains more than one path");
				sketch = value;
				path = value.Paths.First();
				foreach (var line in path.GetEdges())
				{
					Sections.Add(new PathSegment(line));
				}
				try
				{
					var test = new ISD.CAD.Creators.Paths.FromPathOffsetCreator(value.Path, 100, 0.1);
					test.Normal = NormVector3D.E2.Inverted();
					PathGroup pg = new PathGroup();
					pg.AddPathSegment(test.Create());
					pg.CreateCompositeEdge();
				}
				catch (Exception)
				{
				}
				NotifyPropertyChanged(nameof(sketch));
			}
		}

		private static UnconstrainedContext Context => ScriptBase.BaseContext as UnconstrainedContext;

		public class PathSegment : PropertyChangedHandler
		{
			public readonly ISD.Core.API.Edge BaseLine;

			public PathSegment()
			{
			}

			public PathSegment(ISD.Core.API.Edge line)
			{
				BaseLine = line;

				Point3D o = line.StartVertex.Coordinates;
				Point3D x = line.EndVertex.Coordinates;
				Point3D y = new Point3D(o.X, o.Y, o.Z + 1000);

				CoordinateSystem coor = Context.CreateCoordinateSystemOrthonogal(o, x, y);
				BaseLineCoor = coor.Rotate(-90, coor.X, coor.Origin);
				Context.ActiveCoorSys = BaseLineCoor;
			}

			public CoordinateSystem BaseLineCoor { get; private set; }
		}
	}
}