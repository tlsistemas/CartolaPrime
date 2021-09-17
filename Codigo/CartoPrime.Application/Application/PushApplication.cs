using AutoMapper;
using CartoPrime.Application.Interfaces;
using CartoPrime.Application.Parameters;
using CartoPrime.Application.ViewModel;
using CartoPrime.Domain.Entities;
using CartoPrime.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TM.Utils.Events;
using TM.Utils.Http.Response;

namespace CartoPrime.Application.Application
{
    public class PushApplication : IPushApplication
    {
        private readonly IPushService _service;
        private readonly ILogger<PushApplication> _logger;
        public PushApplication(ILogger<PushApplication> logger, IPushService service)
        {
            this._service = service;
            this._logger = logger;
        }

        public async Task<BaseResponse<IEnumerable<PushResponse>>> ListPushAsync(PushParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<PushResponse>>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PushNotification, PushResponse>());
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

                //response.Data = _mapper.Map<IEnumerable<PushResponse>>(obj);
                response.Data = mapper.Map<IEnumerable<PushResponse>>(obj);
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

        public async Task<BaseResponse<Boolean>> Remove(string key)
        {
            var response = new BaseResponse<Boolean>();

            try
            {

                var obj = new PushResponse { Key = key };
                if (obj.Id == 0)
                    obj = new PushResponse { Key = key };
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

        public async Task<BaseResponse<PushResponse>> Create(PushRequest paranObj)
        {
            var response = new BaseResponse<PushResponse>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PushNotification, PushResponse>());
            var mapper = config.CreateMapper();

            try
            {
                if (paranObj.FCMToken is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.FCMToken));
                    return response;
                }

                var obj = new PushNotification
                {
                    UserId = paranObj.UserId,
                    Android = paranObj.Android,
                    DeviceBuild = paranObj.DeviceBuild,
                    DeviceVersion = paranObj.DeviceVersion,
                    FCMToken = paranObj.FCMToken,
                    DeviceId = paranObj.DeviceId,
                    //Title = paranObj.Title,
                    //Message = paranObj.Message,
                    //Link = paranObj.Link,
                    //Type = paranObj.Type,
                    //Version = paranObj.Version,
                    //Icon = paranObj.Icon,
                    //Image = paranObj.Image,
                    //CodeType = paranObj.CodeType,
                    //SendDate = paranObj.SendDate,
                };

                var existingObj = _service.GetOneAsync(x => x.DeviceId == paranObj.DeviceId).Result;

                if (existingObj != null)
                {
                    obj.Id = existingObj.Id;
                    obj.UpdateDate = DateTime.Now;
                    _service.Update(obj);
                }
                else
                {
                    obj.InputDate = DateTime.Now;
                    obj.UpdateDate = DateTime.Now;
                    obj.Removed = false;
                    _service.Add(obj);
                }

                response.Data = mapper.Map<PushResponse>(obj);
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
