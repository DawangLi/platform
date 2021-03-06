﻿using System;

namespace Common
{
    public static class DateTimeLocal
    {

        public static DateTime Now => GetNow();

        public static string NowDate => GetNowDate();
        
        private static string GetNowDate()
        {
            return GetNow().ToString("yyyy/MM/dd");
        }

        private static DateTime GetNow()
        {
            return DateTime.UtcNow.AddHours(8);
        }
    }

}