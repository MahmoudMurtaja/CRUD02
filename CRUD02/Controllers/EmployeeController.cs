using CRUD02.Data;
using CRUD02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD02.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _DB;
        
        public EmployeeController(ApplicationDbContext DB)
        {
            _DB = DB;
        }
        
        public IActionResult Index()
        {
            var employee = _DB.Employee.Include(x => x.Department).Include(x => x.EmployeeType).Where(x => !x.isDelete).ToList();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["departmentList"] = new SelectList(_DB.Department.Where(x => !x.isDelete),"Id","Name");
            ViewData["EmployeeTypeList"] = new SelectList(_DB.EmployeeType.Where(x => !x.isDelete),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee x)
        {
            _DB.Employee.Add(x);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية الاضافة بنجاح";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var employee = _DB.Employee.Find(Id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["departmentList"] = new SelectList(_DB.Department.Where(x => !x.isDelete), "Id", "Name");
            ViewData["EmployeeTypeList"] = new SelectList(_DB.EmployeeType.Where(x => !x.isDelete), "Id", "Name");
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee x)
        {
            _DB.Employee.Update(x);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية التعديل بنجاح";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            var employee = _DB.Employee.Find(Id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.isDelete = true;
            _DB.Employee.Update(employee);
            _DB.SaveChanges();
            TempData["msg"] = "تمت عملية الحذف بنجاح";
            return RedirectToAction("Index");
        }

    }
}
