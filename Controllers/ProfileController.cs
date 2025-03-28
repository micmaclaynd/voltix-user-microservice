using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Voltix.Shared.Interfaces.Http;
using Voltix.UserMicroservice.Interfaces.Http;
using Voltix.UserMicroservice.Services;


namespace Voltix.UserMicroservice.Controllers;

[Route("profile")]
[ApiController]
public class ProfileController(IUserService userService) : ControllerBase {
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult> GetProfileAsync([FromHeader(Name = "User-Id")] int userId) {
        var userModel = await _userService.GetUserAsync(userId);
        if (userModel == null) {
            return NotFound(new IError {
                Message = "User not found"
            });
        }

        return Ok(new IProfile {
            Name = userModel.Name,
            Surname = userModel.Surname,
            Patronymic = userModel.Patronymic,
            Description = userModel.Description
        });
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProfileAsync([FromHeader(Name = "User-Id")] int userId, [FromBody] IUpdateProfileRequest request) {
        var userModel = await _userService.GetUserAsync(userId);
        if (userModel == null) {
            return NotFound(new IError {
                Message = "User not found"
            });
        }

        userModel.Name = request.Name;
        userModel.Surname = request.Surname;
        userModel.Patronymic = request.Patronymic;
        userModel.Description = request.Description;

        await _userService.UpdateUserAsync(userModel);

        return Ok();
    }
}
