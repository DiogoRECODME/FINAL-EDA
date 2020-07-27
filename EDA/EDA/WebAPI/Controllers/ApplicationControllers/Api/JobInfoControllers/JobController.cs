using Microsoft.AspNetCore.Mvc;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.WebAPI.Models.JobInfoVM;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Recodme.Rd.EDA.WebAPI.Controllers.ApplicationControllers.Api.JobInfoControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobBO _bo = new JobBO();

        [NonAction]
        public IActionResult InternalServerError(Exception exception)
        {
            var result = new ObjectResult(exception.Message)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            return result;
        }

        [NonAction]
        public IActionResult NotModified()
        {
            var result = new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.NotModified
            };

            return result;
        }


        #region CREATE

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]JobVM vm)
        {
            var newJob = vm.ToJob();

            var result = await _bo.CreateAsync(newJob);

            if (!result.Success)
                return InternalServerError(result.Exception);

            return Created(Request.Path.Value, null);
        }

        #endregion

        #region READ

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var getResult = await _bo.ReadAsync(id);

            if (!getResult.Success)
                return InternalServerError(getResult.Exception);

            var item = getResult.Result;

            if(item == null)
                return NotFound();

            var vm = JobVM.Parse(item);

            return Ok(vm);
        }

        #endregion

        #region UPDATE

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] JobVM vm)
        {
            var getResult = await _bo.ReadAsync(vm.Id);

            if (!getResult.Success)
                return InternalServerError(getResult.Exception);

            var item = getResult.Result;

            if (item == null)
                return NotFound();

            if (vm.CompareToModel(item))
                return NotModified();

            item = vm.ToJob(item);

            var updateResult = await _bo.UpdateAsync(item);

            if(!updateResult.Success)
                return InternalServerError(updateResult.Exception);          

            return Ok();
        }

        #endregion

        #region DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bo.DeleteAsync(id);

            if (!result.Success)
                return InternalServerError(result.Exception);

            return Ok();
        }

        #endregion

        #region LIST

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var listResult = await _bo.ListUndeletedAsync();

            if (!listResult.Success)
                return InternalServerError(listResult.Exception);

            var list = listResult.Result;
            var finalList = new List<JobVM>();

            foreach (var item in list)
            {
                var vm = JobVM.Parse(item);
                finalList.Add(vm);
            }

            return Ok(finalList);
        }

        #endregion
    }
}