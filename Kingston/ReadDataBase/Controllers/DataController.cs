using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadDataBase.Models;
using System.Data.Entity;



namespace ReadDataBase.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;
        public CSVdataContext entity;

        public DataController(CSVdataContext context, ILogger<CSVdataContext> logger)
        {
            entity = context;
            //_logger = logger;
        }

        [HttpGet]
        public List<CSV_Result> Get()
        {
            return entity.CSV_Results.ToList();
        }

        [HttpPost]
        public bool Create(List<CSV_Result> CSV_Results)
        {
            bool result = false;
            using (var tx = entity.Database.BeginTransaction())
            {
                try
                {   
                    
                    entity.RemoveRange(CSV_Results);
                    foreach (var i in CSV_Results)
                    {
                        CSV_Result newItem = new CSV_Result();
                        newItem.num=i.num;
                        newItem.Name = i.Name;
                        newItem.age = i.age;
                        newItem.school = i.school;
                        entity.Add(newItem);
                        entity.SaveChanges();
                    }
                    tx.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    result = false;
                }
            }

            return result;
        }

       
    }
}



   