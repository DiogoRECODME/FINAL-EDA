using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recodme.Rd.EDA.BusinessLayer.BO.ExtraInfoBO;
using Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO;
using Recodme.Rd.EDA.WebAPI.Models.ExtraInfoVM;
using Recodme.Rd.EDA.WebAPI.Models.HtmlComponents;
using Recodme.Rd.EDA.WebAPI.Models.UserInfoVM;
using Recodme.Rd.EDA.WebAPI.Support;

namespace Recodme.Rd.EDA.WebAPI.Controllers.ApplicationControllers.Web.ExtraInfoControllers
{
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly CommentBO _bo = new CommentBO();
        private readonly ClientBO _clientBO = new ClientBO();
        private readonly ProfileBO _profileBO = new ProfileBO();

        private string GetDeleteRef()
        {
            return this.ControllerContext.RouteData.Values["controller"] + "/" + nameof(Delete);
        }

        private List<BreadCrumb> GetCrumbs()
        {
            return new List<BreadCrumb>()
                { new BreadCrumb(){Icon ="fa-home", Action="Index", Controller="Home", Text="Home"},
                  new BreadCrumb(){Icon = "fa-comments", Action="Index", Controller="Comments", Text = "Comments"}
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

        private async Task<List<ClientVM>> GetClientViewModels(List<Guid> ids)
        {
            var filterOperation = await _clientBO.FilterAsync(x => ids.Contains(x.Id));
            var clientList = new List<ClientVM>();

            foreach (var item in filterOperation.Result)
            {
                clientList.Add(ClientVM.Parse(item));
            }

            return clientList;
        }

        private async Task<ClientVM> GetClientViewModel(Guid id)
        {
            var getOperation = await _clientBO.ReadAsync(id);
            return ClientVM.Parse(getOperation.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListUndeletedAsync();

            if (!listOperation.Success)
                return OperationErrorBackToIndex(listOperation.Exception);

            var finalList = new List<CommentVM>();
            foreach (var item in listOperation.Result)
            {
                finalList.Add(CommentVM.Parse(item));
            }

            var clientList = await GetClientViewModels(listOperation.Result.Select(x => x.ClientId).Distinct().ToList());

            ViewData["Title"] = "Comments";
            ViewData["Clients"] = clientList;
            ViewData["BreadCrumbs"] = GetCrumbs();
            ViewData["DeleteHref"] = GetDeleteRef();
            return View(finalList);
        }

        #region NEW

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var listClientOperation = await _clientBO.ListUndeletedAsync();

            if (!listClientOperation.Success)
                return OperationErrorBackToIndex(listClientOperation.Exception);

            var clientList = new List<SelectListItem>();
            foreach (var item in listClientOperation.Result)
            {
                var clientName = await _profileBO.ReadAsync(item.ProfileId);
                clientList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = (clientName.Result.FullName + " -- " + clientName.Result.Country) });
            }

            ViewBag.Clients = clientList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "New", Controller = "Comments", Icon = "fa-plus-circle", Text = "New" });

            ViewData["Title"] = "New comment";
            ViewData["BreadCrumbs"] = crumbs;
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(CommentVM vm)
        {
            if (ModelState.IsValid)
            {
                var model = vm.ToComment();
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

            var getClientOperation = await _clientBO.ReadAsync(getOperation.Result.ClientId);

            if (!getClientOperation.Success)
                return OperationErrorBackToIndex(getClientOperation.Exception);

            if (getClientOperation.Result == null)
                return RecordNotFound();

            var vm = CommentVM.Parse(getOperation.Result);

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Details", Controller = "Comments", Icon = "fa-info-circle", Text = "Details" });

            ViewData["Title"] = "Comment details";
            ViewData["BreadCrumbs"] = crumbs;
            ViewData["Client"] = ClientVM.Parse(getClientOperation.Result);
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

            var vm = CommentVM.Parse(getOperation.Result);

            var listClientOperation = await _clientBO.ListUndeletedAsync();

            if (!listClientOperation.Success)
                return OperationErrorBackToIndex(listClientOperation.Exception);

            var clientList = new List<SelectListItem>();
            foreach (var item in listClientOperation.Result)
            {
                var clientName = await _profileBO.ReadAsync(item.ProfileId);
                var listItem = new SelectListItem() { Value = item.Id.ToString(), Text = (clientName.Result.FullName + " -- " + clientName.Result.Country) };

                if (item.Id == vm.ClientId)
                    listItem.Selected = true;

                clientList.Add(listItem);
            }

            ViewBag.Clients = clientList;

            var crumbs = GetCrumbs();
            crumbs.Add(new BreadCrumb() { Action = "Edit", Controller = "Comments", Icon = "fa-edit", Text = "Edit" });

            ViewData["Title"] = "Edit comment";
            ViewData["BreadCrumbs"] = crumbs;
            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CommentVM vm)
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
                    result = vm.ToComment(result);

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