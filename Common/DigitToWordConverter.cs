using System;

namespace Common {
    public static class DigitToWordConverter {
        private const string Zero = "";
        private const string FirstMale = "";
        private const string FirstFemale = "";
        private const string FirstFemaleAccusative = "";
        private const string FirstMaleGenetive = "";
        private const string SecondMale = "";
        private const string SecondFemale = "";
        private const string SecondMaleGenetive = "";
        private const string SecondFemaleGenetive = "";

        private static readonly string[] MonthNamesGenitive = {
            string.Empty, "", "", "", "",
            "", "", "", "", "",
            "",
            "", ""
        };

        private static readonly string[] MonthNamesNominative = {
            "", "", "", "",
            "", "", "", "", "",
            "",
            "", ""
        };

        private static readonly string[] From3Till19 = {
            string.Empty, "", "", "", "",
            "", "", "", "", "",
            "", "", "", "",
            "", "", "", ""
        };

        private static readonly string[] From3Till19Genetive = {
            string.Empty, "", "", "", "",
            "", "", "", "", "",
            "", "", "",
            "",
            "", "", "",
            ""
        };

        private static readonly string[] Tens = {
            string.Empty, "", "", "", "",
            "", "", "", ""
        };

        private static readonly string[] TensGenetive = {
            string.Empty, "", "", "", ""
            ,
            "", "", "", ""
        };

        private static readonly string[] Hundreds = {
            string.Empty, "", "", "", "",
            "", "", "", "", ""
        };

        private static readonly string[] HundredsGenetive = {
            string.Empty, "", "", "", "",
            "", "", "", "",
            ""
        };

        private static readonly string[] Thousands = {
            string.Empty, "", "", ""
        };

        private static readonly string[] ThousandsAccusative = {
            string.Empty, "", "", ""
        };

        private static readonly string[] Millions = {
            string.Empty, "", "", ""
        };

        private static readonly string[] Billions = {
            string.Empty, "", "", ""
        };

        private static readonly string[] Trillions = {
            string.Empty, "", "", ""
        };

        private static readonly string[] Kzt = {
            string.Empty, "", "", ""
        };

        private static readonly string[] Cent = {
            string.Empty, "", "", ""
        };

        /// <summary>
        ///   07  2004 [+ _year(:)]
        /// </summary>
        /// <param name="date"> </param>
        /// <param name="year"> </param>
        /// <returns> </returns>
        public static string DateToTextLong(DateTime date, string year) {
            return String.Format("{0} {1} {2}",
                date.Day.ToString("D2"),
                MonthName(date.Month, TextCase.Genitive),
                date.Year) + ((year.Length != 0) ? " " : "") + year;
        }

        /// <summary>
        ///   07  2004
        /// </summary>
        /// <param name="date"> </param>
        /// <returns> </returns>
        public static string DateToTextLong(DateTime date) {
            return String.Format("{0} {1} {2}",
                date.Day.ToString("D2"),
                MonthName(date.Month, TextCase.Genitive),
                date.Year.ToString());
        }

        /// <summary>
        ///    2004
        /// </summary>
        /// <param name="date"> </param>
        /// <returns> </returns>
        public static string DateToTextShort(DateTime date) {
            return String.Format("{0} {1}",
                MonthName(date.Month, TextCase.Genitive),
                date.Year.ToString());
        }

        /// <summary>
        ///   II  2004
        /// </summary>
        /// <param name="date"> </param>
        /// <returns> </returns>
        public static string DateToTextQuarter(DateTime date) {
            return NumeralsRoman(DateQuarter(date)) + "  " + date.Year.ToString();
        }

        /// <summary>
        ///   07.01.2004
        /// </summary>
        /// <param name="date"> </param>
        /// <returns> </returns>
        public static string DateToTextSimple(DateTime date) {
            return String.Format("{0:dd.MM.yyyy}", date);
        }

        public static int DateQuarter(DateTime date) {
            return (date.Month - 1) / 3 + 1;
        }

        private static bool IsPluralGenitive(int digits) {
            return digits >= 5 || digits == 0;
        }

        private static bool IsSingularGenitive(int digits) {
            return digits >= 2 && digits <= 4;
        }

        private static int LastDigit(long amount) {
            if (amount >= 100) {
                amount = amount % 100;
            }

            if (amount >= 20) {
                amount = amount % 10;
            }

            return (int)amount;
        }

       

       

        

        /// <summary>
        ///   10 000  67 
        /// </summary>
        /// <param name="_amount"> </param>
        /// <param name="_firstCapital"> </param>
        /// <returns> </returns>
        public static string CurrencyToTxtShort(double _amount, bool _firstCapital) {
            //10 000  67         
            var rublesAmount = (long)Math.Floor(_amount);
            var copecksAmount = ((long)Math.Round(_amount * 100)) % 100;
            var lastRublesDigit = LastDigit(rublesAmount);
            var lastCopecksDigit = LastDigit(copecksAmount);

            var s = String.Format("{0:N0} ", rublesAmount);

            if (IsPluralGenitive(lastRublesDigit)) {
                s += Kzt[3] + " ";
            } else if (IsSingularGenitive(lastRublesDigit)) {
                s += Kzt[2] + " ";
            } else {
                s += Kzt[1] + " ";
            }

            s += String.Format("{0:00} ", copecksAmount);

            if (IsPluralGenitive(lastCopecksDigit)) {
                s += Cent[3] + " ";
            } else if (IsSingularGenitive(lastCopecksDigit)) {
                s += Cent[2] + " ";
            } else {
                s += Cent[1] + " ";
            }

            return s.Trim();
        }

        private static string MakeText(int _digits, string[] _hundreds, string[] _tens, string[] _from3till19,
            string _second, string _first, string[] _power) {
            var s = "";
            var digits = _digits;

            if (digits >= 100) {
                s += _hundreds[digits / 100] + " ";
                digits = digits % 100;
            }
            if (digits >= 20) {
                s += _tens[digits / 10 - 1] + " ";
                digits = digits % 10;
            }

            if (digits >= 3) {
                s += _from3till19[digits - 2] + " ";
            } else if (digits == 2) {
                s += _second + " ";
            } else if (digits == 1) {
                s += _first + " ";
            }

            if (_digits != 0 && _power.Length > 0) {
                digits = LastDigit(_digits);

                if (IsPluralGenitive(digits)) {
                    s += _power[3] + " ";
                } else if (IsSingularGenitive(digits)) {
                    s += _power[2] + " ";
                } else {
                    s += _power[1] + " ";
                }
            }

            return s;
        }

        /// <summary>
        ///     :  (nominative),  (Genitive),   (accusative)
        /// </summary>
        /// <param name="_sourceNumber"> </param>
        /// <param name="_case"> </param>
        /// <param name="_isMale"> </param>
        /// <param name="_firstCapital"> </param>
        /// <returns> </returns>
        public static string NumeralsToTxt(long _sourceNumber, TextCase _case, bool _isMale, bool _firstCapital) {
            var s = "";
            var number = _sourceNumber;
            int remainder;
            var power = 0;

            if ((number >= (long)Math.Pow(10, 15)) || number < 0) {
                return "";
            }

            while (number > 0) {
                remainder = (int)(number % 1000);
                number = number / 1000;

                switch (power) {
                    case 12:
                        s = MakeText(remainder, Hundreds, Tens, From3Till19, SecondMale, FirstMale, Trillions) + s;
                        break;
                    case 9:
                        s = MakeText(remainder, Hundreds, Tens, From3Till19, SecondMale, FirstMale, Billions) + s;
                        break;
                    case 6:
                        s = MakeText(remainder, Hundreds, Tens, From3Till19, SecondMale, FirstMale, Millions) + s;
                        break;
                    case 3:
                        switch (_case) {
                            case TextCase.Accusative:
                                s =
                                    MakeText(remainder, Hundreds, Tens, From3Till19, SecondFemale, FirstFemaleAccusative,
                                        ThousandsAccusative) + s;
                                break;
                            default:
                                s =
                                    MakeText(remainder, Hundreds, Tens, From3Till19, SecondFemale, FirstFemale,
                                        Thousands) + s;
                                break;
                        }
                        break;
                    default:
                        string[] powerArray = { };
                        switch (_case) {
                            case TextCase.Genitive:
                                s =
                                    MakeText(remainder, HundredsGenetive, TensGenetive, From3Till19Genetive,
                                        _isMale ? SecondMaleGenetive : SecondFemaleGenetive,
                                        _isMale ? FirstMaleGenetive : FirstFemale, powerArray) + s;
                                break;
                            case TextCase.Accusative:
                                s =
                                    MakeText(remainder, Hundreds, Tens, From3Till19, _isMale ? SecondMale : SecondFemale,
                                        _isMale ? FirstMale : FirstFemaleAccusative, powerArray) + s;
                                break;
                            default:
                                s =
                                    MakeText(remainder, Hundreds, Tens, From3Till19, _isMale ? SecondMale : SecondFemale,
                                        _isMale ? FirstMale : FirstFemale, powerArray) + s;
                                break;
                        }
                        break;
                }

                power += 3;
            }

            if (_sourceNumber == 0) {
                s = Zero + " ";
            }

            if (s != string.Empty && _firstCapital) {
                s = s.Substring(0, 1).ToUpper() + s.Substring(1);
            }

            return s.Trim();
        }

        public static string NumeralsDoubleToTxt(double sourceNumber, int _decimal, TextCase _case, bool firstCapital) {
            var decNum = (long)Math.Round(sourceNumber * Math.Pow(10, _decimal)) % (long)(Math.Pow(10, _decimal));

            var s = String.Format(" {0}  {1} ",
                NumeralsToTxt((long)sourceNumber, _case, true, firstCapital),
                NumeralsToTxt(decNum, _case, true, false));
            return s.Trim();
        }

        /// <summary>
        ///    -
        /// </summary>
        /// <param name="month">   </param>
        /// <param name="_case"> </param>
        /// <returns> </returns>
        public static string MonthName(int month, TextCase _case) {
            var s = string.Empty;

            if (month > 0 && month <= 12) {
                switch (_case) {
                    case TextCase.Genitive:
                        s = MonthNamesGenitive[month];
                        break;
                }
            }

            return s;
        }

        public static string NumeralsRoman(int number) {
            var s = string.Empty;

            switch (number) {
                case 1:
                    s = "I";
                    break;
                case 2:
                    s = "II";
                    break;
                case 3:
                    s = "III";
                    break;
                case 4:
                    s = "IV";
                    break;
            }

            return s;
        }

        /// <summary>
        ///   
        /// </summary>
        /// <param name="month"> </param>
        /// <returns> </returns>
        public static string MonthToText(int month) {
            return MonthNamesNominative[month - 1];

        }

        /// <summary>
        ///      
        /// </summary>
        /// <param name="number"> 10</param>
        /// <returns></returns>
        public static string NumeralsToTxt(int number) {
            return NumeralsToTxt(number, TextCase.Nominative, true, false);
        }
    }

    internal enum GenitiveType {
        Plural,
        Singular,
        Default
    }

    public enum NumperPart {
        Fractional,
        Floor
    }

    public enum TextCase {
        Nominative /*? ?*/,
        Genitive /*? ?*/,
        Dative /*? ?*/,
        Accusative /*? ?*/,
        Instrumental /*? ?*/,
        Prepositional /* ?  ?*/
    };
}