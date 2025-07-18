﻿using DigitalService.Domain.BaseModel;
using DigitalService.Domain.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalService.UI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            var userAudit = _context.UserAuditEvents.ToList();

            return View(userAudit);
        }
    }
}