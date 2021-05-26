using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DTO
{
    class SV
    {
        public string MSSV { get; set; }
        public string NameSV { get; set; }
        public bool Gender { get; set; }
        public DateTime NS { get; set; }
        public int ID_Lop { get; set; }
        public string NameLop { get; set; }
        public SV() { }
        public SV(string ms, string n, bool gender, DateTime ns, int id)
        {
            MSSV = ms;
            NameSV = n;
            Gender = gender;
            NS = ns;
            ID_Lop = id;
        }
        public static bool MSSVCaoThap(object o1, object o2)
        {
            return Convert.ToInt32(((SV)o1).MSSV) < Convert.ToInt32(((SV)o2).MSSV);
        }
        public static bool MSSVThapCao(object o1, object o2)
        {
            return Convert.ToInt32(((SV)o1).MSSV) > Convert.ToInt32(((SV)o2).MSSV);
        }
        public static bool NameZA(object o1, object o2)
        {
            if (string.Compare(((SV)o1).NameSV, ((SV)o2).NameSV) < 0)
                return true;
            else
                return false;
        }
        public static bool NameAZ(object o1, object o2)
        {
            if (string.Compare(((SV)o1).NameSV, ((SV)o2).NameSV) > 0)
                return true;
            else
                return false;
        }
        public static bool NSSort(object o1, object o2)
        {
            if (DateTime.Compare(((SV)o1).NS,((SV)o2).NS) < 0)
            {
                return true;
            }
            else
                return false;
        }
        public static bool LopSort(object o1, object o2)
        {
            if (((SV)o1).ID_Lop>((SV)o2).ID_Lop)
            {
                return true;
            }
            else
                return false;

        }
    }
}
