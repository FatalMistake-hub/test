using MVC.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL
{
    class DAL_QLSV
    {
        private static DAL_QLSV _Instance;
        public static DAL_QLSV Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DAL_QLSV();
                return _Instance;
            }
            private set { }
        }
        public DAL_QLSV()
        { }
        public List<SV> GetAllSV()
        {
            List<SV> data = new List<SV>();
            string query = "select  MSSV, NameSV, Gender, NS, LopSH.NameLop, SV.ID_Lop " +
                           "from SV, LopSH where SV.ID_Lop = LopSH.ID_Lop";
            foreach(DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetSV(i));
            }
            return data;
        }
        public void AddSV_DAL(SV s)
        {
            string query = ("Insert into SV(MSSV,NameSV,Gender,NS,ID_Lop) Values(" 
                            + s.MSSV + ", '" 
                            + s.NameSV + "', '" 
                            + s.Gender + "', '" 
                            + s.NS + "', '"
                            + s.ID_Lop + "')");
            DBHelper.Instance.ExcuteDB(query);
        }
        public SV GetSV(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                NameSV = i["NameSV"].ToString(),
                NS = Convert.ToDateTime(i["NS"]),
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                Gender = Convert.ToBoolean(i["Gender"]),
                NameLop = i["NameLop"].ToString()
            };
        }
        public List<LSH> GetAllLSH_DAL()
        {
            List<LSH> data = new List<LSH>();
            string query = "Select * from LopSH";
            if (DBHelper.Instance != null)
                foreach (DataRow i in DBHelper.Instance.GetRecords(query).Rows)
                {
                    data.Add(GetLSH(i));
                }
            return data;
        }
        public LSH GetLSH(DataRow i)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                NameLop = i["NameLop"].ToString()
            };
        }
        public void DeleteSV_DAL(SV s)
        {
            string sql = "Delete SV where MSSV ='" + s.MSSV + "'";
            DBHelper.Instance.ExcuteDB(sql);
        }
        public void UpdateSV_DAL(SV s)
        {
            string sql = ("Update SV Set NameSV='" 
                                + s.NameSV + "',Gender='" 
                                + s.Gender + "',NS='" 
                                + s.NS+ "',ID_Lop='" 
                                + s.ID_Lop + "' where MSSV ='" 
                                + s.MSSV + "'");
            DBHelper.Instance.ExcuteDB(sql);
        }
        public SV GetSVByMSSV_DAL(string ms)
        {
            SV s = new SV();
            string sql = "Select MSSV, NameSV, Gender, NS, LopSH.NameLop, SV.ID_Lop " +
                         "from SV, LopSH where SV.ID_Lop = LopSH.ID_Lop and MSSV = '" + ms + "'";
            return GetSV(DBHelper.Instance.GetRecords(sql).Rows[0]);
        }
        public List<SV> GetAllSV_DAL(int ID_Lop)
        {
            List<SV> data = new List<SV>();
            if (ID_Lop == 0)
            {
                data = GetAllSV();
            }
            else
            {
                foreach (SV i in GetAllSV())
                {
                    if (i.ID_Lop == ID_Lop)
                    {
                        data.Add(new SV
                        {
                            NameSV = i.NameSV,
                            MSSV = i.MSSV,
                            Gender = i.Gender,
                            NS = i.NS,
                            ID_Lop = i.ID_Lop,
                            NameLop = i.NameLop
                        });
                    }
                }
            }
            return data;
        }
    }
}

