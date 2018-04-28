using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Memories
{
    public class ConfigurationUserPreferences
    {
        public static IFormatProvider culture = new System.Globalization.CultureInfo(Configuration.langue.ToString());
        public static DateTime Extract_date(string dateExtract)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo(Configuration.langue.ToString());
        //   string dateExtract = "17-02-1976";
        // Specify exactly how to interpret the string.
        // IFormatProvider culture = new System.Globalization.CultureInfo(Configuration.langue.ToString());

        // Alternate choice: If the string has been input by an end user, you might 
        // want to format it according to the current culture:
        // IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        DateTime dt = DateTime.Parse(dateExtract, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            Debug.WriteLine("Year: {0}, Month: {1}, Day {2}", dt.Year, dt.Month, dt.Day);

            return dt;

        }
    }
}
