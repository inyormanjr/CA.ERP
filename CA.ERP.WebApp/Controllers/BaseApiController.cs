using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CA.ERP.Domain.UserAgg;
using AutoMapper;
using CA.ERP.Domain.Base;

namespace CA.ERP.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController:ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IUserHelper _userHelper;
        protected readonly IMapper _mapper;
        public BaseApiController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<BaseApiController>>();
            _userHelper = serviceProvider.GetService<IUserHelper>();
            _mapper = serviceProvider.GetService<IMapper>();
        }
        /// <summary>
        /// The users current selected branch.
        /// </summary>
        //public Guid? CurrentUserBranchId { get; set; }
    }
    
}
