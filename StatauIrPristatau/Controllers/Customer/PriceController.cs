using StatauIrPristatau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatauIrPristatau.Controllers.Customer
{
    public class PriceController : Controller
    {
        public static Tuple<Order, Parcel> calculatePrice(Order order, Parcel parcel)
        {
            parcel.Size = calculateSize(parcel);
            order.TotalSize = calculateCapacity(order, parcel);
            order.TotalWeigth = calculateWeigth(order, parcel);
            parcel.Car = pickCarForDelivery(parcel);
            double standardParcelPrice = 1;
            double orderPrice = order.TotalPrice;
            if (orderPrice == 0)
            {
                orderPrice = 1;
            }
            double parcelPrice = standardParcelPrice;
            double koefSizeS = 1.65;
            double koefSizeM = 2.15;
            double koefSizeL = 2.98;
            double koefSizeXL = 3.87;
            double koefOrderSizex2 = 1.25;
            double koefOrderSizex3 = 1.45;
            double koefOrderSizex4 = 1.75;
            double koefWeigthx2 = 1.25;
            double koefWeigthx3 = 1.45;
            double koefWeigthx4 = 1.75;
            if (parcel.Size == "S")
            {
                parcelPrice = parcelPrice * koefSizeS;
            }
            if (parcel.Size == "M")
            {
                parcelPrice = parcelPrice * koefSizeM;
            }
            if (parcel.Size == "L")
            {
                parcelPrice = parcelPrice * koefSizeL;
            }
            if (parcel.Size == "XL")
            {
                parcelPrice = parcelPrice * koefSizeXL;
            }
            orderPrice = orderPrice + parcelPrice;
            parcel.Price = parcelPrice;
            if (order.TotalSize / 1000000 > 1 && order.TotalSize / 1000000 <= 2)
            {
                orderPrice = orderPrice * koefOrderSizex2;
            }
            else if (order.TotalSize / 1000000 > 2 && order.TotalSize / 1000000 <= 3)
            {
                orderPrice = orderPrice * koefOrderSizex3;
            }
            else if (order.TotalSize / 1000000 > 3 && order.TotalSize / 1000000 <= 4)
            {
                orderPrice = orderPrice * koefOrderSizex4;
            }
            if (order.TotalWeigth / 550 > 1 && order.TotalWeigth / 550 <= 2)
            {
                orderPrice = orderPrice * koefWeigthx2;
            }
            else if (order.TotalWeigth / 550 > 2 && order.TotalWeigth / 550 <= 3)
            {
                orderPrice = orderPrice * koefWeigthx3;
            }
            else if (order.TotalWeigth / 550 > 3 && order.TotalWeigth / 550 <= 4)
            {
                orderPrice = orderPrice * koefWeigthx4;
            }
            order.TotalPrice = orderPrice;
            return Tuple.Create(order, parcel);
        }

        private static string calculateSize(Parcel parcel)
        {
            double size = 0;
            string sizeChar = "S";
            size = parcel.Height * parcel.Length * parcel.Width;
            if (size <= 21800 && size > 0)
            {
                sizeChar = "S";
            }
            else if (size <= 46200 && size > 21800)
            {
                sizeChar = "M";
            }
            else if (size <= 100000 && size > 46200)
            {
                sizeChar = "L";
            }
            else if (size > 100000)
            {
                sizeChar = "XL";
            }
            return sizeChar;
        }

        private static double calculateCapacity(Order order, Parcel parcel)
        {
            double totalSize = order.TotalSize;
            double parcelSize = parcel.Height * parcel.Length * parcel.Width;
            totalSize += parcelSize;
            return totalSize;
        }
        private static double calculateWeigth(Order order, Parcel parcel)
        {
            double totalWeigth = order.TotalWeigth;
            double parcelWeigth = parcel.Weigth;
            return totalWeigth += parcelWeigth;

        }
        private static string pickCarForDelivery(Parcel parcel)
        {
            string car;
            if (parcel.Size == "S" || parcel.Size == "M" )
            {
                car = "Mini";
            }
            else if (parcel.Size == "L")
            {
                car = "Medium";
            }
            else
            {
                car = "Maxi";
            }
            return car;
        }
    }
}