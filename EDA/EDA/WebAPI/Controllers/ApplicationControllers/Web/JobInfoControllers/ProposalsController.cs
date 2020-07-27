using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.WebAPI.Models.HtmlComponents;
using Recodme.Rd.EDA.WebAPI.Models.JobInfoVM;
using Recodme.Rd.EDA.WebAPI.Support;

namespace Recodme.Rd.EDA.WebAPI.Controllers.ApplicationControllers.Web.JobInfoControllers
{
    [Route("[controller]")]
    public class ProposalsController : Controller
    {
        private readonly ProposalBO _bo = new ProposalBO();
        private readonly JobBO _jobBO = new JobBO();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-comments", Action="Index", Controller="Proposals", Text = "Proposals"}
                };
        }

        private IActionResult RecordNotFound()
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Information, "The record was not found.");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationErrorBackToIndex(Exception exception)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, exception);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult OperationSuccess(string message)
        {
            TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Success, message);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<JobVM>> GetJobViewModels(List<Guid> ids)
        {
            var filterOperation = await _jobBO.FilterAsync(x => ids.Contains(x.Id));
            var jobList = new List<JobVM>();

            foreach (var item in filterOperation.Result)
            {
                jobList.Add(JobVM.Parse(item));
            }

            return jobList;
        }

        private async Task<JobVM> GetJobViewModel(Guid id)
        {
            var getOperation = await _jobBO.ReadAsync(id);
            return JobVM.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListUndeletedAsync();

            if (!listOperation.Success)
                return OperationErrorBackToIndex(listOperation.Exception);

            var finalList = new List<ProposalVM>();
            foreach (var item in listOperation.Result)
            {
                finalList.Add(ProposalVM.Parse(item));
            }

            var jobList = await GetJobViewModels(listOperation.Result.Select(x => x.JobId).Distinct().ToList());

            ViewData["Title"] = "Proposals";
            ViewData["Jobs"] = jobList;
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            return View(finalList);
        }

        #region NEW

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listJobOperation = await _jobBO.ListUndeletedAsync();

            if (!listJobOperation.Success)
                return OperationErrorBackToIndex(listJobOperation.Exception);

            var jobList = new List<SelectListItem>();
            foreach (var item in listJobOperation.Result)
            {
                jobList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = (item.Address + " -- STARTING @ " + item.StartDate) });
            }

            ViewBag.Jobs = jobList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Proposals", Icon = "fa-plus-circle", Text = "New" });

            ViewData["Title"] = "New proposal";
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ProposalVM vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToProposal();
                var createOperation = await _bo.CreateAsync(model);

                if (!createOperation.Success)
                    return OperationErrorBackToIndex(createOperation.Exception);

                return OperationSuccess("The record was successfully created.");
            }
            return View(vm);
        }

        #endregion

        #region DETAILS

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return RecordNotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success)
                return OperationErrorBackToIndex(getOperation.Exception);

            if (getOperation.Result == null)
                return RecordNotFound();

            var getJobOperation = await _jobBO.ReadAsync(getOperation.Result.JobId);

            if (!getJobOperation.Success)
                return OperationErrorBackToIndex(getJobOperation.Exception);

            if (getJobOperation.Result == null)
                return RecordNotFound();

            var vm = ProposalVM.Parse(getOperation.Result);

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Proposals", Icon = "fa-info-circle", Text = "Details" });

            ViewData["Title"] = "Proposal details";
            ViewData["BreadCrumbs"] = crumbs;
            ViewData["Job"] = JobVM.Parse(getJobOperation.Result);
            return View(vm);
        }

        #endregion

        #region EDIT

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return RecordNotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);

            if (!getOperation.Success)
                return OperationErrorBackToIndex(getOperation.Exception);

            if (getOperation.Result == null)
                return RecordNotFound();

            var vm = ProposalVM.Parse(getOperation.Result);

            var listJobOperation = await _jobBO.ListUndeletedAsync();

            if (!listJobOperation.Success)
                return OperationErrorBackToIndex(listJobOperation.Exception);

            var jobList = new List<SelectListItem>();
            foreach (var item in listJobOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = (item.Address + " -- STARTING @ " + item.StartDate) };

                if (item.Id == vm.JobId)
                    listItem.Selected = true;

                jobList.Add(listItem);
            }

            ViewBag.Jobs = jobList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Proposals", Icon = "fa-edit", Text = "Edit" });

            ViewData["Title"] = "Edit proposal";
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProposalVM vm)
        {
            if (ModelState.IsValid)
            {
                var getOperation = await _bo.ReadAsync(id);

                if (!getOperation.Success)
                    return OperationErrorBackToIndex(getOperation.Exception);

                if (getOperation.Result == null)
                    return RecordNotFound();

                var result = getOperation.Result;

                if (!vm.CompareToModel(result))
                {
                    result = vm.ToProposal(result);

                    var updateOperation = await _bo.UpdateAsync(result);

                    if (!updateOperation.Success)
                    {
                        TempData["Alert"] = AlertFactory.GenerateAlert(NotificationType.Danger, updateOperation.Exception);
                        return View(vm);
                    }

                    return OperationSuccess("The record was successfully updated.");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region DELETE

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return RecordNotFound();

            var deleteOperation = await _bo.DeleteAsync((Guid)id);

            if (!deleteOperation.Success)
                return OperationErrorBackToIndex(deleteOperation.Exception);

            return OperationSuccess("The record was successfully deleted.");
        }

        #endregion
    }
}