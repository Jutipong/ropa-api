using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Entities.DdContextTcrb;
using WebApi.Models;

namespace WebApi.Services.ConfigGroupQuestion
{
    public class ConfigGroupQuestionService : IConfigGroupQuestionService
    {
        private readonly ILogger _logger;
        private readonly DdContextTcrb _context;

        public ConfigGroupQuestionService(ILogger<Entities.Models.MsQuestion> logger, DdContextTcrb context)
        {
            _logger = logger;
            _context = context;
        }

        public ResponseModel GetQuestionById(Entities.Models.MsGroup msGroup)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                var datas = (from config in _context.ConfigGroupQuestion
                             join question in _context.MsQuestion on config.IdQuestion equals question.IdQuestion
                             where (question.IsActive == true && config.IdGroup == msGroup.IdGroup)
                             select new
                             {
                                 question.IdQuestion,
                                 question.Name
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

        public ResponseModel Action(Entities.Models.ConfigGroupQuestionDto configGroupQuestionDtos)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                //delete
                var questionIds = configGroupQuestionDtos.Questions
                    .Where(r => r.IdGroup == configGroupQuestionDtos.IdGroup && r.IdConfigGroupQuestion != Guid.Empty && r.IdQuestion != Guid.Empty)
                    .Select(r => r.IdQuestion)
                    .ToList();
                //var dels = _context.ConfigGroupQuestion
                //    .Where(r => r.IdGroup == configGroupQuestionDtos.IdGroup && !questionIds.Contains(r.IdQuestion));
                //_context.RemoveRange(dels);

                //update
                var questionUpdates = configGroupQuestionDtos.Questions.Where(r => r.IdConfigGroupQuestion != Guid.Empty && r.IdQuestion != Guid.Empty).ToList();

                //var update = _context.ConfigGroupQuestion.UpdateRange();


                //create
                var questionCreate = configGroupQuestionDtos.Questions.Where(r => r.IdConfigGroupQuestion == Guid.Empty).ToList();



                //_context.SaveChanges();
                //transaction.Commit();
                _logger.LogInformation($"Finish Function => {methodName}");

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
    }
}
