using System;
using System.Linq;

namespace Common {
    public static class DateTimeHelper {
        public static DateTime AddWorkDays(this DateTime date, int workingDays) {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0) {
                newDate = newDate.AddDays(direction);
                if (newDate.IsWorkingHoliday() ||
                    (newDate.DayOfWeek != DayOfWeek.Saturday && newDate.DayOfWeek != DayOfWeek.Sunday &&
                     !newDate.IsHoliday())) {
                    workingDays -= direction;
                }
            }
            return newDate;
        }

        public static bool IsHoliday(this DateTime date) {
            // TODO: ЗАПИЛИТЬ С ХРАНЕНИЕМ В БД, КЕШИРОВАНИЕМ, БЛЭКДЖЕКОМ И ... по-человечески
            DateTime[] holidays =
                new DateTime[] {
                    new DateTime(2017, 05, 01),
                    new DateTime(2017, 05, 08),
                    new DateTime(2017, 05, 09),
                    new DateTime(2017, 07, 06),
                    new DateTime(2017, 07, 07),
                    new DateTime(2017, 08, 30),
                    new DateTime(2017, 09, 01),
                    new DateTime(2017, 12, 01),
                    new DateTime(2017, 12, 18),
                    new DateTime(2017, 12, 19)
                };
            return holidays.Contains(date.Date);
        }

        public static bool IsWorkingHoliday(this DateTime date)
        {
            DateTime[] workingHolidays =
                new DateTime[] {
                    // сюда рабочие субботы и воскресенья
                };
            return workingHolidays.Contains(date.Date);
        }
    }
}