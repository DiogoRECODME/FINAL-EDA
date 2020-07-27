using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.WebAPI.Models.HtmlComponents;
using Recodme.Rd.EDA.WebAPI.Models.UserInfoVM;
using Recodme.Rd.EDA.WebAPI.Support;

namespace Recodme.Rd.EDA.WebAPI.Controllers.ApplicationControllers.Web.UserInfoControllers
{
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly ClientBO _bo = new ClientBO();
        private readonly ProfileBO _profileBO = new ProfileBO();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-users", Action="Index", Controller="Clients", Text = "Clients"}
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

        private async Task<List<ProfileVM>> GetProfileViewModels(List<Guid> ids)
        {
            var filterOperation = await _profileBO.FilterAsync(x => ids.Contains(x.Id));
            var profileList = new List<ProfileVM>();

            foreach (var item in filterOperation.Result)
            {
                profileList.Add(ProfileVM.Parse(item));
            }

            return profileList;
        }

        private async Task<ProfileVM> GetProfileViewModel(Guid id)
        {
            var getOperation = await _profileBO.ReadAsync(id);
            return ProfileVM.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListUndeletedAsync();

            if (!listOperation.Success)
                return OperationErrorBackToIndex(listOperation.Exception);

            var finalList = new List<ClientVM>();
            foreach (var item in listOperation.Result)
            {
                finalList.Add(ClientVM.Parse(item));
            }

            var profileList = await GetProfileViewModels(listOperation.Result.Select(x => x.ProfileId).Distinct().ToList());

            ViewData["Title"] = "Clients";
            ViewData["Profiles"] = profileList;
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            return View(finalList);
        }

        #region NEW

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listProfileOperation = await _profileBO.ListUndeletedAsync();

            if (!listProfileOperation.Success)
                return OperationErrorBackToIndex(listProfileOperation.Exception);

            var profileList = new List<SelectListItem>();
            foreach (var item in listProfileOperation.Result)
            {
                profileList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = (item.FullName + " -- " + item.Country) });
            }

            ViewBag.Profiles = profileList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Clients", Icon = "fa-plus-circle", Text = "New" });

            ViewData["Title"] = "New client";
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ClientVM vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToClient();
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

            var getProfileOperation = await _profileBO.ReadAsync(getOperation.Result.ProfileId);

            if (!getProfileOperation.Success)
                return OperationErrorBackToIndex(getProfileOperation.Exception);

            if (getProfileOperation.Result == null)
                return RecordNotFound();

            var vm = ClientVM.Parse(getOperation.Result);

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Clients", Icon = "fa-info-circle", Text = "Details" });

            ViewData["Title"] = "Client details";
            ViewData["BreadCrumbs"] = crumbs;
            ViewData["Profile"] = ProfileVM.Parse(getProfileOperation.Result);
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

            var vm = ClientVM.Parse(getOperation.Result);

            var listProfileOperation = await _profileBO.ListUndeletedAsync();

            if (!listProfileOperation.Success)
                return OperationErrorBackToIndex(listProfileOperation.Exception);

            var profileList = new List<SelectListItem>();
            foreach (var item in listProfileOperation.Result)
            {
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = (item.FullName + " -- " + item.Country) };

                if (item.Id == vm.ProfileId)
                    listItem.Selected = true;

                profileList.Add(listItem);
            }

            ViewBag.Profiles = profileList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Clients", Icon = "fa-edit", Text = "Edit" });

            ViewData["Title"] = "Edit client";
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientVM vm)
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
                    result = vm.ToClient(result);

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