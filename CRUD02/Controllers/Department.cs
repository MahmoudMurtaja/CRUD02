using CRUD02.Data;
using CRUD02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD02.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _DB;

        public DepartmentController(ApplicationDbContext DB)
        {
            _DB = DB;

        }
        public IActionResult Index()
        {
            var departments = _DB.Department.Where(x=> !x.isDelete).ToList();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department x)
        {
            _DB.Department.Add(x);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية الاضافة بنجاح";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var department = _DB.Department.Find(Id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(Department x)
        {
            _DB.Department.Update(x);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية التعديل بنجاح";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            var department = _DB.Department.Find(Id);
            if (department == null)
            {
                return NotFound();
            }
            department.isDelete = true;
            _DB.Department.Update(department);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية الحذف بنجاح";
            return RedirectToAction("Index");
        }
        


    }
}
