using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADODemo.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult StudentList()
        {
            String ConStr = "Server=DESKTOP-NSU3C0M\\SQLEXPRESS;Database=StudentDemoDB;User Id =sa;Password=sa123";
            SqlConnection con = new SqlConnection(ConStr);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Table_Student", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            List<Student> st = new List<Student>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Student s = new Student();
                s.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                s.StudentName = dt.Rows[i]["StudentName"].ToString();
                s.EmailId = dt.Rows[i]["EmailId"].ToString();
                s.PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString();
                st.Add(s);
            }
            return View(st);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string StudentName,string EmailId,String PhoneNumber)
        {
            String ConStr = "Server=DESKTOP-NSU3C0M\\SQLEXPRESS;Database=StudentDemoDB;User Id =sa;Password=sa123";
            SqlConnection con = new SqlConnection(ConStr);
            String qry = "Insert Into Table_Student(StudentName,EmailId,PhoneNumber) values ('"+StudentName+"','"+EmailId+"','"+PhoneNumber+"')";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("StudentList");
        }
    }
    public class Student
    {
        public int ID { get; set; }
        public string StudentName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
    }
}