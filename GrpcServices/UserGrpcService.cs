using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Voltix.SettingMicroservice.Protos;
using Voltix.UserMicroservice.Models;
using Voltix.UserMicroservice.Protos;
using Voltix.UserMicroservice.Services;


namespace Voltix.UserMicroservice.GrpcServices;

public class UserGrpcService(IUserService userService, SettingProto.SettingProtoClient settingClient) : UserProto.UserProtoBase {
    private readonly IUserService _userService = userService;
    private readonly SettingProto.SettingProtoClient _settingClient = settingClient;

    public override async Task<Empty> ChangePassword(ChangePasswordRequest request, ServerCallContext context) {
        var userModel = await _userService.GetUserAsync(request.UserId) ?? throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

        userModel.PasswordHash = request.PasswordHash;
        await _userService.UpdateUserAsync(userModel);

        return new Empty();
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context) {
        var userModel = await _userService.GetUserAsync(request.Email);
        if (userModel != null) {
            throw new RpcException(new Status(StatusCode.NotFound, "The user with this email already exists"));
        }

        var defaultRoleIdResponse = await _settingClient.GetDefaultRoleIdAsync(new Empty());

        userModel = new UserModel {
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            RegisteredDatetime = DateTime.UtcNow,
            RoleId = defaultRoleIdResponse.DefaultRoleId,
            PasswordHash = request.PasswordHash
        };

        await _userService.CreateUserAsync(userModel);

        return new CreateUserResponse {
            UserId = userModel.Id
        };
    }
}
