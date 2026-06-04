using System;

namespace AhDai.Core.Utils;

/// <summary>
/// GeoUtil
/// </summary>
public static class GeoUtil
{
    /// <summary>
    /// 地球半径，单位：公里
    /// </summary>
    const double EarthRadiusKm = 6371.0;
    const double DegToRad = Math.PI / 180.0;

    /// <summary>
    /// 计算球面距离
    /// </summary>
    /// <param name="startLongitude"></param>
    /// <param name="startLatitude"></param>
    /// <param name="endLongitude"></param>
    /// <param name="endLatitude"></param>
    /// <returns>距离：公里</returns>
    public static double GetDistance(double startLongitude, double startLatitude, double endLongitude, double endLatitude)
    {
        var dLat = (endLatitude - startLatitude) * DegToRad;
        var dLon = (endLongitude - startLongitude) * DegToRad;

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(startLatitude * DegToRad) * Math.Cos(endLatitude * DegToRad) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    /// <summary>
    /// 计算球面距离
    /// </summary>
    /// <param name="startLongitude"></param>
    /// <param name="startLatitude"></param>
    /// <param name="endLongitude"></param>
    /// <param name="endLatitude"></param>
    /// <returns>距离：公里</returns>
    public static decimal GetDistance(decimal startLongitude, decimal startLatitude, decimal endLongitude, decimal endLatitude)
    {
        var distance = GetDistance((double)startLongitude, (double)startLatitude, (double)endLongitude, (double)endLatitude);
        return (decimal)distance;
    }
}
