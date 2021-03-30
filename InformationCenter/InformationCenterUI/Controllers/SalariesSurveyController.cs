using InformationCenterUI.HttpClients;
using InformationCenterUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InformationCenterUI.Controllers
{
    public class SalariesSurveyController : Controller
    {
        readonly SurveyClient client;

        public SalariesSurveyController(SurveyClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSalaryRecord()
        {
            return View(new SalaryRecord());
        }

        [HttpPost]
        public async Task<IActionResult> Add(SalaryRecord record)
        {
            await this.client.AddSalaryRecord(record);
            return View("Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetSalaryRecords()
        {
            List<SalaryRecord> salaries = await this.client.GetSalaryRecords();
            return View("GetSalaryRecords", salaries);
        }

        public IActionResult GetSalaryRecordById()
        {
            return View(new SalaryRecord());
        }

        [HttpGet]
        public async Task<IActionResult> GetSalaryRecordById(SalaryRecord record)
        {
            record = await this.client.GetSalaryRecordById(record.Id);
            return View("ViewSalaryRecordById", record);
        }

        public IActionResult DeleteSalaryRecordById()
        {
            return View(new SalaryRecord());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSalaryRecordById(SalaryRecord record)
        {
            await this.client.DeleteSalaryRecordById(record.Id);
            return View("Success");
        }
    }
}
