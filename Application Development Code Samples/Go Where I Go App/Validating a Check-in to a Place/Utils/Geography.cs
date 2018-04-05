using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device;
using System.Device.Location;

namespace GWIG.Web.Utils
{
    public class Geography
    {
        // UTIL: Calculating the distance between two GeoCoordinate LatLng pairs
        public static double getDistance(decimal firstLatitude, decimal firstLongitude, decimal secondLatitude, decimal secondLongitude)
        {
            // GET: Distance between Lat and Lng respectively
            GeoCoordinate firstCoord = new GeoCoordinate((double)firstLatitude, (double)firstLongitude);
            GeoCoordinate secondCoord = new GeoCoordinate((double)secondLatitude, (double)secondLongitude);

            // CALCULATE: Coord distance of LatLng
            return firstCoord.GetDistanceTo(secondCoord);
        }
    }
}
