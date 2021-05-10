using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Utf8Json;
using WebApi.Entities.DdContextTcrb;
using WebApi.Models;

namespace WebApi.Services.MsQuestion
{
    public class MsQuestionService : IMsQuestionService
    {
        private readonly ILogger _logger;
        private readonly DdContextTcrb _context;

        public MsQuestionService(ILogger<Entities.Models.MsQuestion> logger, DdContextTcrb context)
        {
            _logger = logger;
            _context = context;
        }

        public ResponseModels<Entities.Models.MsQuestion> Inquiry(MsQuestionReqModel req)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(req)}");

                var query = _context.MsQuestion.Where(r => string.IsNullOrEmpty(req.Filter) || r.Name.Contains(req.Filter));
                var datas = query.OrderByDescending(r => r.CreateDate).Skip((req.Page - 1) * req.RowsPerPage).Take(req.RowsPerPage).ToList();
                var total = query.Count();
                var result = new ResponseModels<Entities.Models.MsQuestion>
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

        public ResponseModel Create(Entities.Models.MsQuestionDto msQuestionDto)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msQuestionDto)}");

                var _question = new Entities.Models.MsQuestion
                {
                    IdQuestion = Guid.NewGuid(),
                    Name = msQuestionDto.Name,
                    CreateBy = "System",
                    CreateDate = DateTime.Now
                };
                _context.MsQuestion.Add(_question);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(_question)}");

                return new ResponseModel<Entities.Models.MsQuestion>
                {
                    Success = true,
                    Datas = _question,
                };
            }
            catch (Exception ex)
            {
                var messageError = $"Error Function => {methodName}";
                _logger.LogError(ex, messageError);
                throw new ArgumentException(messageError, ex);
            }
        }

        public ResponseModel Update(Entities.Models.MsQuestion msQuestion)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msQuestion)}");

                _context.MsQuestion.Update(msQuestion);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(msQuestion)}");

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
        public ResponseModel Delete(Entities.Models.MsQuestion msQuestion)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}, Parameters => {JsonSerializer.ToJsonString(msQuestion)}");

                _context.MsQuestion.Remove(msQuestion);
                _context.SaveChanges();

                _logger.LogInformation($"Finish Function => {methodName}, Result => {JsonSerializer.ToJsonString(msQuestion)}");

                return new ResponseModels<Entities.Models.MsQuestion>
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
