using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using FoodLoc.Models;
using FoodLocWeb.Data;

namespace FoodLoc.Helpers
{
    public class LocationHelper
    {

        public List<PostViewModel> GetLocationinCertainMileRadius(ApplicationDbContext _context, double lat, double lon, int miles, int noofposts)
        {
            var posts =
                 (from post in _context.Posts
                  let dist = Distance(lat, lon, post.Latitude, post.Longitude, 'M')
                  where dist <= miles
                  orderby dist ascending, post.UploadedDate descending 
                  select new PostViewModel
                  {
                      PhotoTitle = post.PhotoTitle,
                      UploadedDate = post.UploadedDate,
                      Distance = dist,
                      ImageName = post.ImageName,
                      Latitude = post.Latitude,
                      Longitude = post.Longitude,
                      UpoadedBy = post.UpoadedBy

                  }).Take(noofposts);


            //_context.Posts.Where(x =>
            //       Math.Acos(Math.Sin(lat) * Math.Sin(x.Latitude) +
            //                Math.Cos(lat) * Math.Cos(x.Latitude) * Math.Cos(x.Longitude - (lon))) * 6371 <= miles).Take(noofposts).ToList();
            return posts.ToList();
        }
        private double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (Math.Round(dist,2));
        }
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
