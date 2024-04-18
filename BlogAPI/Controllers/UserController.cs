using BlogAPI.Const;
using BlogAPI.DTO.User;
using BlogAPI.IRepository;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogAPI.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
    }
}
