using AttendanceTrackerMicroservices.Models;
using AttendanceTrackerMicroservices.Service.IService;
using AttendanceTrackerMicroservices.Utility;
using Microsoft.AspNetCore.Identity.Data;
using static AttendanceTrackerMicroservices.Utility.SD;

namespace AttendanceTrackerMicroservices.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IBaseService baseService, ILogger<AuthService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO assignRoleRequest)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = assignRoleRequest,
                Url = AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequest)
        {
            RequestDTO request = new RequestDTO()
            {
                ApiType = ApiType.POST,
                Data = loginRequest,
                Url = AuthAPIBase + "/api/auth/login"
            };

            _logger.LogInformation("LoginAsync started - attempting login for user: {Username}",
                loginRequest.UserName);

            return await _baseService.SendAsync(request, withBearer: false);
        }

        public async Task<ResponseDTO?> RegistrationAsync(RegistrationRequestDTO registrationRequest)
        {
            RequestDTO request = new RequestDTO()
            {
                ApiType = ApiType.POST,
                Data = registrationRequest,
                Url = AuthAPIBase + "/api/auth/register"
            };

            return await _baseService.SendAsync(request, withBearer: false);
        }

        public async Task<ResponseDTO?> ValidateUser(LoginRequestDTO validateRequest)
        {
            RequestDTO request = new RequestDTO()
            {
                ApiType = ApiType.POST,
                Data = validateRequest,
                Url = AuthAPIBase + "/api/auth/validateUser"
            };

            return await _baseService.SendAsync(request, withBearer: false);
        }
    }
}
