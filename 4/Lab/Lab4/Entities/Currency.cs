using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Lab.Lab4.Entities {
    public class CurrencyAbbreviationToFlag : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string country = (value as string)![..2];

            var flag = $"{string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)))} ({(value as string)!})";
            return flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class Currency {
        [Key]
        public int Cur_ID { get; set; }
        public int? Cur_ParentID { get; set; }
        public string Cur_Code { get; set; }
        public string Cur_Abbreviation { get; set; }
        public string Cur_Name { get; set; }
        public string Cur_Name_Bel { get; set; }
        public string Cur_Name_Eng { get; set; }
        public string Cur_QuotName { get; set; }
        public string Cur_QuotName_Bel { get; set; }
        public string Cur_QuotName_Eng { get; set; }
        public string Cur_NameMulti { get; set; }
        public string Cur_Name_BelMulti { get; set; }
        public string Cur_Name_EngMulti { get; set; }
        public int Cur_Scale { get; set; }
        public int Cur_Periodicity { get; set; }
        public DateTime Cur_DateStart { get; set; }
        public DateTime Cur_DateEnd { get; set; }
    }
}