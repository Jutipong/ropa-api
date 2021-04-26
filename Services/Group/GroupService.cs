using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Utf8Json;
using WebApi.Entities.DdContextTcrb;
using WebApi.Entities.Models;
using WebApi.Models;

namespace WebApi.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly ILogger _logger;
        private readonly DdContextTcrb _context;

        public GroupService(ILogger<MsGroup> logger, DdContextTcrb context)
        {
            _logger = logger;
            _context = context;
        }
        public ResponseModels<MsGroup> Inquiry(List<MsGroup> msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                var nameCondition = msGroups.Select(r => r.Name);
                var result = _context.MsGroup.Where(r => nameCondition.Contains(r.Name)).ToList();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(result)}");

                return new ResponseModels<MsGroup>
                {
                    Success = true,
                    Datas = result
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModels<MsGroup> Create(List<MsGroup> msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                _context.MsGroup.AddRange(msGroups);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(msGroups)}");

                return new ResponseModels<MsGroup>
                {
                    Success = true,
                    Datas = msGroups
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel Update(List<MsGroup> msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                _context.MsGroup.UpdateRange(msGroups);
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
        public ResponseModel Delete(List<MsGroup> msGroups)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msGroups)}");

                _context.MsGroup.RemoveRange(msGroups);
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
    }
}

