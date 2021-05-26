using MVC.DAL;
using MVC.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MVC.BLL
{
    class BLL_QLSV
    {
        public delegate bool MyCompare(object o1, object o2);
        private static BLL_QLSV _Instance;
        public static BLL_QLSV Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLL_QLSV();
            return _Instance;
            }
            private set { }
        }
        public BLL_QLSV()
        { }
        public List<SV> GetAllSV_BLL(int ID_Lop)
        {
            return DAL_QLSV.Instance.GetAllSV_DAL(ID_Lop);
        }
        public void AddSV_BLL (SV s)
        {
            DAL_QLSV.Instance.AddSV_DAL(s);
        }
        public List<SV> SortSV_BLL(List<string> LMSSV, MyCompare cmp)
        {
            DAL_QLSV DAL = new DAL_QLSV();
            List<SV> data = DAL.GetAllSV();
            for (int i = 0; i < data.Count - 1; ++i)
            {
                for (int j = i + 1; j < data.Count; ++j)
                {
                    if (cmp(data[i], data[j]))
                    {
                        SV temp = data[i];
                        data[i] = data[j];
                        data[j] = temp;
                    }
                }
            }
            return data;
        }
        public List<LSH> GetAllLSH_BLL()
        {
            return DAL_QLSV.Instance.GetAllLSH_DAL();
        }
        public SV GetSVbyMSSV_BLL(string ms)
        {
            return DAL_QLSV.Instance.GetSVByMSSV_DAL(ms);
        }
        public void DeleteSV_BLL(SV s)
        {
            DAL_QLSV.Instance.DeleteSV_DAL(s);
        }
        public void UpdateSV_BLL(SV s)
        {
            DAL_QLSV.Instance.UpdateSV_DAL(s);
        }
        public List<SV> GetAllSV_BLL()
        {
            return DAL_QLSV.Instance.GetAllSV();
        }
        public SV FindSv(string MSSV)
        {
            foreach (SV k in this.GetAllSV_BLL())
            {
                if (k.MSSV == MSSV)
                    return k;
            }
            return null;
        }
        public List<SV> GetALllSVDGV_BLL(List<string> LMSSV)
        {
            List<SV> data = new List<SV>();
            foreach (string i in LMSSV)
            {
                data.Add(BLL_QLSV.Instance.GetSVbyMSSV_BLL(i));
            }
            return data;
        }
        public List<SV> GetSVByName_BLL(List<string> LMSSV, string Name)
        {
            List<SV> data = new List<SV>();
            foreach (SV i in GetALllSVDGV_BLL(LMSSV))
            {
                if (Name != "")
                {
                    if (i.NameSV.Contains(Name))
                    {
                        data.Add(new SV
                        {
                            NameSV = i.NameSV,
                            MSSV = i.MSSV,
                            Gender = i.Gender,
                            NS = i.NS,
                            ID_Lop = i.ID_Lop
                        });
                    }
                }
            }
            return data;
        }
    }
}
