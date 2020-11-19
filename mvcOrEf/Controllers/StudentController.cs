using common;
using dal;
using model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

namespace mvcOrEf.Controllers
{
    public class StudentController : Controller
    {
        UnitofWork unitof = new UnitofWork();
        // GET: Student
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult getstudent(int page, int limit)
        {
            var students = unitof.CreateRespository<Student>().GetEntityListByPage(pageindex: page, pageSize: limit)
                .Select(m => new { m.Id, m.Name, m.Phone, m.Sex, m.CardId, m.EnrollmentDate, m.Address })
                .ToList();
            int count = unitof.CreateRespository<Student>().Getcount();
            return Json(new { code = 0, msg = "", count = count, data = students }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        [Authorize]
        public ActionResult EditStudent(int id)
        {
            var students = unitof.CreateRespository<Student>().GetEntityById(id);
            var classes = unitof.CreateRespository<Grade>().GetEntityList().ToList();
            ViewBag.student = students;
            ViewBag.classes = classes;
            return View("~/Views/Student/edit.cshtml");

        }
        [HttpPost]
        [Authorize]
        public ActionResult EditStudent(Student student)
        {

            var result = unitof.CreateRespository<Student>().Update(student);
            if (result > 0)
            {
                return Json(new { success = true, msg = "修改成功" });
            }
            else
            {
                return Json(new { success = false, msg = "修改失败" });
            }


        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View("~/Views/Student/Add.cshtml");
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            var res = unitof.CreateRespository<Student>().Insert(student);
            return Json(new { msg = res > 0 ? "添加成功" : "添加失败" });
        }

        [HttpPost]
        public ActionResult DelStudent(int id)
        {
            int res = unitof.CreateRespository<Student>().Delete(id);
            if (res > 0)
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, msg = "删除失败" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public ActionResult DatchDelStudent(int[] id)
        {
            var res = unitof.CreateRespository<Student>().BatchDelete(id);
            return Json(new { success = true, msg = "删除成功" });
        }

        public ActionResult InfoStudent(int id)
        {
            var students = unitof.CreateRespository<Student>().GetEntityById(id);
            var classes = unitof.CreateRespository<Grade>().GetEntityList().ToList();
            ViewBag.student = students;
            ViewBag.classes = classes;

            return View("~/Views/Student/Info.cshtml");
        }
        [HttpGet]
        public ActionResult Getclass()
        {

            var grade = unitof.CreateRespository<Grade>().GetEntityList().Select(m => new { m.Id, m.Name });
            return Json(grade, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult QueryStudent(string query, int page, int limit)
        {
            var id = -99;
            System.Text.RegularExpressions.Regex rex =
             new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(query))
            {
                id = int.Parse(query);
            }


            var count = unitof.CreateRespository<Student>().GetEntityList().Where(m => m.Name.IndexOf(query) != -1 || m.Id == id || m.Address.IndexOf(query) != -1).ToList().Count;
            var students = unitof.CreateRespository<Student>().GetEntityListByPage(m => m.Name.IndexOf(query) != -1 || m.Id == id || m.Address.IndexOf(query) != -1, page, limit).Select(m => new { m.Id, m.Name, m.Phone, m.Sex, m.CardId, m.EnrollmentDate, m.Address })
                .ToList();

            return Json(new { code = 0, msg = "", count = count, data = students }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult excelModel()
        {
            var path = Server.MapPath("~/files/model.xls");
            return File(path, "application/vnd.ms-excel");
        }


        [HttpPost]
        //表格的导入
        public ActionResult Export(HttpPostedFileBase file) {
            var path = Server.MapPath("/files");
            var filepath = Path.Combine(path, file.FileName);
            //将文件进行保存
            try
            {
                file.SaveAs(filepath);
                var table = ToExcel.Table(filepath);
                List<Student> students = new List<Student>();
                for (var i = 0; i < table.Rows.Count; i++)
                {
                    var a = table.Rows[i][6].ToString();
                    students.Add(new Student()
                    {
                        Name = table.Rows[i][0].ToString(),
                        GradeId = Convert.ToInt32(table.Rows[i][1].ToString()),
                        Sex = table.Rows[i][2].ToString(),
                        CardId = table.Rows[i][3].ToString(),
                        Phone = table.Rows[i][4].ToString(),
                        Address = table.Rows[i][5].ToString(),
                        EnrollmentDate = Convert.ToDateTime(table.Rows[i][6].ToString())
                    });
                }
                unitof.CreateRespository<Student>().Insert(students);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "发生了异常" });
                throw ex;
            }
            return Json(new { msg="导入成功"});




        }
        
        [HttpGet]

        public ActionResult InfoExcel()
        {
            var path = Server.MapPath("~/files/导出.xls");
            FileStream fs = new FileStream(path,FileMode.Create);
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]{ 
                new DataColumn("姓名"),
                new DataColumn("班级Id"),
                new DataColumn("性别"),
                new DataColumn("身份证"),
                new DataColumn("电话号码"),
                new DataColumn("家庭地址"),
                new DataColumn("入学时间"),

            });
            var list = unitof.CreateRespository<Student>().GetEntityList().Select(m=> new {m.Name,m.GradeId,m.Sex,m.CardId,m.Phone,m.Address,m.EnrollmentDate }).ToList();
            for(var i=0; i < list.Count; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = list[i].Name;
                dataRow[1] = list[i].GradeId;
                dataRow[2] = list[i].Sex;
                dataRow[3] = list[i].CardId;
                dataRow[4] = list[i].Phone;
                dataRow[5] = list[i].Address;
                dataRow[6] = list[i].EnrollmentDate;
                dataTable.Rows.Add(dataRow);
            }

            var work = ToExcel.Excel(dataTable);
            work.Write(fs);
            fs.Close();
            work.Close();

            return File(path, "application/vnd.ms-excel","导出.xls");
        }
    }
}