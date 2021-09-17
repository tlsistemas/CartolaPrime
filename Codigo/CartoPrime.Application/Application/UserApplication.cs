using AutoMapper;
using CartoPrime.Application.Interfaces;
using CartoPrime.Application.ViewModel;
using CartoPrime.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TM.Utils.Events;
using TM.Utils;
using TM.Utils.Http.Response;
using CartoPrime.Application.Parameters;
using System.Linq;
using CartoPrime.Domain.Entities;

namespace CartoPrime.Application.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserService _service;
        private readonly ILogger<UserApplication> _logger;
        public UserApplication(ILogger<UserApplication> logger, IUserService service)
        {
            this._service = service;
            this._logger = logger;
        }

        public async Task<BaseResponse<UserResponse>> AuthenticateAsync(UserTokenRequest user)
        {
            var response = new BaseResponse<UserResponse>();

            try
            {
                if (String.IsNullOrEmpty(user.UserID))
                {
                    response.AddErrors(String.Format(Events.CREATE_PARTICIPANT_ERROR.Message, user.UserID));
                    return response;
                }
                if (String.IsNullOrEmpty(user.AccessKey))
                {
                    response.AddErrors(String.Format(Events.CREATE_PARTICIPANT_ERROR.Message, user.AccessKey));
                    return response;
                }

                var password = user.AccessKey.EncryptSHA256();
                var result = _service.GetOneAsync(x => x.UserName == user.UserID && x.Password == password).Result;
                if (result != null)
                {
                    response.Data = new UserResponse
                    {
                        CPF = result.CPF,
                        Email = result.Email,
                        Nome = result.Nome,
                        Password = result.Password,
                        Key = result.Key,
                        Token = result.Token,
                        UserName = result.UserName,
                        Tipo = result.Tipo
                    };
                }
                else
                {
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            
            return response;
        }

        public async Task<BaseResponse<IEnumerable<UserResponse>>> ListUserAsync(UserParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<UserResponse>>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CartoPrime.Domain.Entities.User, UserResponse>());
                var mapper = config.CreateMapper();
                var obj = await _service.GetByParamsAsync(paran.Filter(), paran.OrderBy, paran.Include);

                response.Count = obj.Count();

                if (paran.Skip.HasValue)
                {
                    obj = obj.Skip(paran.Skip.Value);
                }

                if (paran.Take.HasValue && paran.Take.Value > 0)
                {
                    obj = obj.Take(paran.Take.Value);
                }

                //response.Data = _mapper.Map<IEnumerable<UserResponse>>(obj);
                response.Data = mapper.Map<IEnumerable<UserResponse>>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paran), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<Boolean>> Remove(int key)
        {
            var response = new BaseResponse<Boolean>();

            try
            {

                var obj = new UserResponse { Id = key };
                if (obj.Id == 0)
                    obj = new UserResponse { Id = key };
                var existingObj = _service.GetOneAsync(x => x.Id.Equals(obj.Id)).Result;

                if (existingObj == null)
                {
                    return response;
                }

                _service.Remove(existingObj);

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, key), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        public async Task<BaseResponse<UserResponse>> Create(UserResponse paranObj)
        {
            var response = new BaseResponse<UserResponse>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CartoPrime.Domain.Entities.User, UserResponse>());
            var mapper = config.CreateMapper();

            try
            {
                if (paranObj.UserName is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.UserName));
                    return response;
                }
                if (!Validation.Email(paranObj.Email))
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.UserName));
                    return response;
                }
                if (paranObj.Password is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Password));
                    return response;
                }
                if (paranObj.CPF is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.CPF));
                    return response;
                }

                if (paranObj.Tipo == 0)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Tipo));
                    return response;
                }


                var existingObj = _service.GetOneAsync(x => x.UserName == paranObj.UserName 
                                                        || x.CPF == paranObj.CPF.Replace(".", "").Replace("-","")).Result;

                if (existingObj != null)
                {
                    response.AddError(Events.INVALID_VALUE, "CPF");
                    return response;
                }

                var obj = new User
                {
                    UserName = paranObj.UserName,
                    Password = paranObj.Password.EncryptSHA256(),
                    Nome = paranObj.Nome,
                    CPF = paranObj.CPF,
                    Email = paranObj.Email,
                    Tipo = paranObj.Tipo
                };

                obj.InputDate = DateTime.Now;
                obj.UpdateDate = DateTime.Now;
                obj.Removed = false;
                _service.Add(obj);
                response.Data = mapper.Map<UserResponse>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paranObj), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}
