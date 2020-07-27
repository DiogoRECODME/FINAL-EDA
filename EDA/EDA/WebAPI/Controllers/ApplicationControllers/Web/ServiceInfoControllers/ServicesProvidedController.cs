using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO;
using Recodme.Rd.EDA.WebAPI.Models.HtmlComponents;
using Recodme.Rd.EDA.WebAPI.Models.ServiceInfoVM;
using Recodme.Rd.EDA.WebAPI.Support;

namespace Recodme.Rd.EDA.WebAPI.Controllers.ApplicationControllers.Web.ServiceInfoControllers
{
    [Route("[controller]")]
    public class ServicesProvidedController : Controller
    {
        private readonly ServiceProvidedBO _bo = new ServiceProvidedBO();
        private readonly ServiceBO _serviceBO = new ServiceBO();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-sitemap", Action="Index", Controller="ServicesProvided", Text = "Services provided"}
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

        private async Task<List<ServiceVM>> GetServiceViewModels(List<Guid> ids)
        {
            var filterOperation = await _serviceBO.FilterAsync(x => ids.Contains(x.Id));
            var serviceList = new List<ServiceVM>();

            foreach (var item in filterOperation.Result)
            {
                serviceList.Add(ServiceVM.Parse(item));
            }

            return serviceList;
        }

        private async Task<ServiceVM> GetViewModel(Guid id)
        {
            var getOperation = await _serviceBO.ReadAsync(id);
            return ServiceVM.Parse(getOperation.Result);
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListUndeletedAsync();

            if (!listOperation.Success)
                return OperationErrorBackToIndex(listOperation.Exception);

            var finalList = new List<ServiceProvidedVM>();
            foreach (var item in listOperation.Result)
            {
                finalList.Add(ServiceProvidedVM.Parse(item));
            }

            var serviceList = await GetServiceViewModels(listOperation.Result.Select(x => x.ServiceId).Distinct().ToList());

            ViewData["Title"] = "Services provided";
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            ViewData["Services"] = serviceList;
            return View(finalList);
        }

        #region NEW

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listServiceOperation = await _serviceBO.ListUndeletedAsync();

            if (!listServiceOperation.Success)
                return OperationErrorBackToIndex(listServiceOperation.Exception);

            var serviceList = new List<SelectListItem>();
            foreach (var item in listServiceOperation.Result)
            {
                serviceList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Services = serviceList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "ServicesProvided", Icon = "fa-plus-circle", Text = "New" });

            ViewData["Title"] = "New service provided";
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ServiceProvidedVM vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToServiceProvided();
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

            var getServiceOperation = await _serviceBO.ReadAsync(getOperation.Result.ServiceId);

            if (!getServiceOperation.Success)
                return OperationErrorBackToIndex(getServiceOperation.Exception);

            if (getServiceOperation.Result == null)
                return RecordNotFound();

            var vm = ServiceProvidedVM.Parse(getOperation.Result);

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "ServicesProvided", Icon = "fa-info-circle", Text = "Details" });

            ViewData["Title"] = "Service provided details";
            ViewData["BreadCrumbs"] = crumbs;
            ViewData["Service"] = ServiceVM.Parse(getServiceOperation.Result);
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

            var vm = ServiceProvidedVM.Parse(getOperation.Result);

            var listServiceOperation = await _serviceBO.ListUndeletedAsync();

            if (!listServiceOperation.Success)
                return OperationErrorBackToIndex(listServiceOperation.Exception);

            var serviceList = new List<SelectListItem>();
            foreach (var item in listServiceOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = item.Name };

                if (item.Id == vm.ServiceId)
                    listItem.Selected = true;

                serviceList.Add(listItem);
            }

            ViewBag.Services = serviceList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "ServicesProvided", Icon = "fa-edit", Text = "Edit" });

            ViewData["Title"] = "Edit service provided";
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ServiceProvidedVM vm)
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
                    result = vm.ToServiceProvided(result);

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