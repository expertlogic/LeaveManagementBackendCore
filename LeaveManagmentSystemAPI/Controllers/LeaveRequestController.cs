using AutoMapper;
using Domain.Response;
using Infrastructure.Contracts;
using LeaveManagmentSystemAPI.Core;
using LeaveManagmentSystemAPI.Data;
using LeaveManagmentSystemAPI.Extensions;
using LeaveManagmentSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeaveManagmentSystemAPI.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocRepo;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            ILeaveRequestRepository leaveRequestRepo,
            ILeaveTypeRepository leaveTypeRepo,
            ILeaveAllocationRepository leaveAllocRepo,
            IMapper mapper,
            IUserContext userContext,
            UserManager<Employee> userManager
        )
        {
            _leaveRequestRepo = leaveRequestRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _leaveAllocRepo = leaveAllocRepo;
            _mapper = mapper;
            _userContext = userContext;
            _userManager = userManager;
        }



        // GET: LeaveRequestController
         //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            IEnumerable<LeaveRequest> leaveRequests;
            if(_userContext.Profile.UserRole == "Administrator")
            {
                leaveRequests = await _leaveRequestRepo.FindByInclude(X => X.Id > 0, null, "LeaveType");
            }
            else
            {
                leaveRequests = await _leaveRequestRepo.FindByInclude(X => X.RequestingEmployeeId  == _userContext.Profile.UserId, null, "LeaveType");
            }

            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            //var LeaveHistories = _db.LeaveRequests
            //  .Include(q => q.RequestingEmployee)
            //  .Include(q => q.ApprovedBy)
            //  .Include(q => q.LeaveType)
            //  .ToList();

            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(q => q.Approved == true),
                PendingRequests = leaveRequestsModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestsModel
            };
            return Ok(model);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateLeaveRequestVM model)
        {
            ResponseData data = new ResponseData();

            try
            {
                var userId = Request.HttpContext.GetClaimValue("userId");
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var leaveTypes = await _leaveTypeRepo.FindAll();
                var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTypeItems;
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    data.statusCode = "ERROR";
                    data.message = "Start date cannot be further in the future than the end date";
                    return BadRequest(data);
                }

                var employee = await _userManager.FindByEmailAsync(User.Identity.Name);

                var allocation = _leaveAllocRepo.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId);

                int daysRequested = (int)(endDate - startDate).TotalDays;

                //if (daysRequested > allocation.NumberOfDays)
                //{
                //    return Json("ERROR", "You do not have sufficient days for this request!");
                //}

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    isTravelRequired = model.isTravelRequired,
                    RequestComments = model.RequestComments
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);


                 var isSuccess = _leaveRequestRepo.CreateByProc(leaveRequest);
                //await _leaveRequestRepo.AddAsync(leaveRequest);
                //await _leaveRequestRepo.SaveChangesAsync();

                //var isSuccess = leaveRequest.Id > 0;
                if (!isSuccess)
                {
                    data.statusCode = "ERROR";
                    data.message = "Something went wrong with submitting your record";
                    return BadRequest(data);

                }
                data.message = "success";

                data.statusCode = "success";
                data.data = leaveRequest;

                return Ok(data);
            }
            catch (Exception ex)
            {
                data.statusCode = "ERROR";
                data.message = ex.Message;
                return BadRequest(data);

            }
        }



    }
}
