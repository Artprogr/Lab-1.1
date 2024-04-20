
namespace Date
{
    public class Date
    {
        private uint year, month, day;

        public Date()
        {
            year = month = day = 0;
        }
        public Date(uint year, uint month, uint day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }

        public void SetYear(uint year)
        {
            this.year = year;
        }
        public void SetMonth(uint month)
        {
            this.month = month;
        }
        public void SetDay(uint day)
        {
            this.day = day;
        }

        public uint GetYear()
        {
            return year;
        }
        public uint GetMonth()
        {
            return month;
        }
        public uint GetDay()
        {
            return day;
        }

        public bool LeapYear()
        {
            return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0;
        }

        public Date AddDays(int days)
        {
            uint[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            day += (uint)days;
            while (day > daysInMonth[month - 1])
            {
                day -= daysInMonth[month - 1];
                month++;
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }
            return this;
        }

        public Date MinusDays(uint days)
        {
            uint[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            while (days > 0)
            {
                if (day > days)
                {
                    day -= (uint)days;
                    days = 0;
                }
                else
                {
                    days -= day;
                    month--;
                    if (month == 0)
                    {
                        month = 12;
                        year--;
                    }
                    day = daysInMonth[month - 1];
                }
            }
            return this;
        }

        public int BetweenDays(Date other)
        {
            uint[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            uint days = 0;
            if (year == other.year && day == other.day)
            {
                if (other.month > month)
                {
                    while (month != other.month)
                    {
                        days += daysInMonth[other.month - 1];
                        month++;
                    }
                    goto start;
                }
                else
                {
                    while (month != other.month)
                    {
                        days += daysInMonth[other.month - 1];
                        other.month++;
                    }
                    goto start;
                }
            }
            if (year == other.year && month == other.month)
            {
                days = day - other.day;
            }
            else
            {
                if (other.year < year)
                {
                    while (year != other.year || month != other.month)
                    {
                        if (other.month == 12)
                        {
                            other.month = 1;
                            other.year++;
                        }
                        else
                        {
                            other.month++;
                        }
                        days += daysInMonth[other.month - 1];
                    }
                    days += other.day - day;
                }
                else
                {
                    while (year != other.year || month != other.month)
                    {
                        if (month == 12)
                        {
                            month = 1;
                            year++;
                        }
                        else
                        {
                            month++;
                        }
                        days += daysInMonth[month - 1];
                    }
                    days += day - other.day;
                }
            }
             start: 
             return Math.Abs((int)days);
        }

        public int Compare(Date other)
        {
            if ((year < other.year) & (month == other.month) & (day == other.day))
            {
                return -1;
            }
            if ((year > other.year) & (month == other.month) & (day == other.day))
            {
                return 1;
            }
            if ((year == other.year) & (month < other.month) & (day == other.day))
            {
                return -1;
            }
            if ((year == other.year) & (month > other.month) & (day == other.day))
            {
                return 1;
            }
            if ((year == other.year) & (month == other.month) & (day < other.day))
            {
                return -1;
            }
            if ((year == other.year) & (month == other.month) & (day > other.day))
            {
                return 1;
            }
            if ((day == other.day) && (month == other.month) && (year == other.year))
            {
                return 0;
            }
            return 2;
        }

        public override string ToString()
        {
            return $"{year}.{month:00}.{day:00}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool a1, a2, a3;
            uint a11, a12, a13;
            string part1, part2, part3;
            Console.WriteLine("Введите дату в формате год.месяц.день: ");
            string input = Console.ReadLine();
            try
            {
                    int l1;
                    l1 = input.Length;
                    part1 = input.Substring(0, l1-6) ;
                    part2 = input.Substring(l1-5, 2);
                    part3 = input.Substring(l1-2, 2);

                    a1 = uint.TryParse(part1, out a11);
                    a2 = uint.TryParse(part2, out a12);
                    a3 = uint.TryParse(part3, out a13);
                    if (a1 & a2 & a3)
                    {
                        Date date = new Date(a11, a12, a13);

                        Console.WriteLine("Введенная дата: {0}", date);
                        Console.WriteLine("Год: {0}", date.GetYear());
                        Console.WriteLine("Месяц: {0}", date.GetMonth());
                        Console.WriteLine("День: {0}", date.GetDay());
                        Console.WriteLine("Является ли введенный год високосным? {0}", date.LeapYear() ? "Да" : "Нет");

                        Console.WriteLine("Добавить сколько дней к дате? ");
                        int daysToAdd;
                        bool a = int.TryParse(Console.ReadLine(), out daysToAdd);
                        if (a)
                        {
                            date.AddDays(daysToAdd);
                            Console.WriteLine("Дата после добавления дней: {0}", date);

                            Console.WriteLine("Вычесть сколько дней из даты? ");
                            uint daysToSubtract;
                            bool a4 = uint.TryParse(Console.ReadLine(), out daysToSubtract);
                            if (a4)
                            {
                                date.MinusDays(daysToSubtract);
                                Console.WriteLine("Дата после вычитания дней: {0}", date);

                                Console.WriteLine("Введите вторую дату в формате год.месяц.день: ");
                                input = Console.ReadLine();
                                int l2;
                                l2 = input.Length;
                                part1 = input.Substring(0, l2 - 6);
                                part2 = input.Substring(l2 - 5, 2);
                                part3 = input.Substring(l2 - 2, 2);
                                Date otherDate = new Date(uint.Parse(part1), uint.Parse(part2), uint.Parse(part3));
                                Date otherDate1 = new Date(uint.Parse(part1), uint.Parse(part2), uint.Parse(part3));

                                Console.WriteLine("Количество дней между двумя датами: {0}", date.BetweenDays(otherDate));

                                if (date.Compare(otherDate1) == -1)
                                {
                                    Console.WriteLine("Первая дата раньше второй");
                                }
                                else if (date.Compare(otherDate1) == 0)
                                {
                                    Console.WriteLine("Даты равны");
                                }
                                else
                                {
                                    Console.WriteLine("Первая дата позже второй");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат");
                        }
                    }
            }
            catch
            {
                Console.WriteLine("Неверный формат");
            }
        }
    }
}