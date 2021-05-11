using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using Utf8Json;
using WebApi.Entities.DdContextTcrb;
using WebApi.Entities.Models;
using WebApi.Models;
using static WebApi.Models.GroupsModel;

namespace WebApi.Services.Group
{
    public class MsGroupService : IMsGroupService
    {
        private readonly ILogger _logger;
        private readonly DdContextTcrb _context;

        public MsGroupService(ILogger<MsGroup> logger, DdContextTcrb context)
        {
            _logger = logger;
            _context = context;
        }
        public ResponseModels<MsGroup> Inquiry(GroupReqModel req)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(req)}");

                var query = _context.MsGroup.Where(r => string.IsNullOrEmpty(req.Filter) || r.Name.Contains(req.Filter));
                var datas = query.OrderByDescending(r => r.CreateDate).Skip((req.Page - 1) * req.RowsPerPage).Take(req.RowsPerPage).ToList();
                var total = query.Count();
                var result = new ResponseModels<MsGroup>
                {
                    Success = true,
                    Datas = datas,
                    Total = total
                };

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(result)}");
                return result;
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel<MsGroup> Create(MsGroupDto msGroup)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroup)}");

                var _group = new MsGroup
                {
                    IdGroup = Guid.NewGuid(),
                    Name = msGroup.Name,
                    CreateBy = "System",
                    CreateDate = DateTime.Now
                };
                _context.MsGroup.Add(_group);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(_group)}");

                return new ResponseModel<MsGroup>
                {
                    Success = true,
                    Datas = _group,
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel Update(MsGroup msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                _context.MsGroup.Update(msGroups);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(msGroups)}");

                return new ResponseModel
                {
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }
        public ResponseModel Delete(MsGroup msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                _context.MsGroup.Remove(msGroups);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(msGroups)}");

                return new ResponseModels<MsGroup>
                {
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel GetAll()
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                var datas = _context.MsGroup
                .Where(r => r.IsActive == true)
                .Select(r => new
                {
                    value = r.IdGroup.ToString(),
                    label = r.Name,
                }).ToList();

                _logger.LogInformation($"Finish Function => {methodName}");

                return new ResponseModel
                {
                    Success = true,
                    Datas = datas
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }
    }
}

