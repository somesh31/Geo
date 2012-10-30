﻿using System.Collections.Generic;
using System.Linq;
using Geo.Interfaces;

namespace Geo.Geometries
{
    public class GeometryCollection : GeometryCollectionBase<IGeometry>, IGeoJsonGeometry
    {
        public GeometryCollection()
        {
        }

        public GeometryCollection(IEnumerable<IGeometry> geometries) : base(geometries)
        {
        }

        public GeometryCollection(params IGeometry[] geometries) : base(geometries)
        {
        }

        public override string ToWktString()
        {
            return BuildWktString<IWktGeometry>("GEOMETRYCOLLECTION", geometry => geometry.ToWktString());
        }

        public string ToGeoJson()
        {
            return SimpleJson.SerializeObject(this.ToGeoJsonObject());
        }

        public object ToGeoJsonObject()
        {
            return new Dictionary<string, object>
            {
                { "type", "GeometryCollection" },
                { "geometries", Geometries.Cast<IGeoJsonGeometry>().Select(x => x.ToGeoJsonObject()).ToArray() }
            };
        }

        #region Equality methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(GeometryCollection left, GeometryCollection right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            return !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);
        }

        public static bool operator !=(GeometryCollection left, GeometryCollection right)
        {
            return !(left == right);
        }

        #endregion
    }
}