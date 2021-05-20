using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Entities.DdContextTcrb;
using WebApi.Entities.Models;
using WebApi.Models;

namespace WebApi.Services.ConfigGroupQuestion
{
    public class ConfigGroupQuestionService : IConfigGroupQuestionService
    {
        private readonly ILogger _logger;
        private readonly DdContextTcrb _context;
        private readonly IMapper _mapper;

        public ConfigGroupQuestionService(ILogger<Entities.Models.MsQuestion> logger, DdContextTcrb context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public ResponseModel GetQuestionById(MsGroup msGroup)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                var datas = (from config in _context.ConfigGroupQuestion
                             join question in _context.MsQuestion on config.IdQuestion equals question.IdQuestion
                             where (question.IsActive == true && config.IdGroup == msGroup.IdGroup)
                             select new ConfigGroupQuestionModel
                             {
                                 IdConfigGroupQuestion = config.IdConfigGroupQuestion,
                                 IdGroup = config.IdGroup,
                                 IdQuestion = config.IdQuestion,
                                 Name = question.Name,
                                 CreateBy = config.CreateBy,
                                 CreateDate = config.CreateDate,
                                 IsActive = config.IsActive,
                                 Order = config.Order
                             }).OrderBy(r=> r.Order).ToList();

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

        public ResponseModel Action(ConfigGroupQuestionDto configGroupQuestionDto)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                //delete
                var questionIds = configGroupQuestionDto.Questions
                    .Where(r => r.IdGroup == configGroupQuestionDto.IdGroup && r.IdConfigGroupQuestion != Guid.Empty && r.IdQuestion != Guid.Empty)
                    .Select(r => r.IdQuestion)
                    .ToList();
                var deletes = _context.ConfigGroupQuestion
                    .Where(r => r.IdGroup == configGroupQuestionDto.IdGroup && !questionIds.Contains(r.IdQuestion));
                _context.RemoveRange(deletes);

                //update
                var questionUpdates = configGroupQuestionDto.Questions.Where(r => r.IdConfigGroupQuestion != Guid.Empty && r.IdQuestion != Guid.Empty).ToList();
                var updates = _mapper.Map<List<ConfigGroupQuestionModel>, List<Entities.Models.ConfigGroupQuestion>>(questionUpdates);
                _context.ConfigGroupQuestion.UpdateRange(updates);


                //create
                var questionCreate = configGroupQuestionDto.Questions.Where(r => r.IdConfigGroupQuestion == Guid.Empty).ToList();
                var creates = _mapper.Map<List<ConfigGroupQuestionModel>, List<Entities.Models.ConfigGroupQuestion>>(questionCreate);
                var maxOrder = _context.ConfigGroupQuestion.Where(r => r.IdGroup == configGroupQuestionDto.IdGroup).Max(r=> r.Order) ?? 0;
                creates.ForEach(r =>
                {
                    r.IdGroup = configGroupQuestionDto.IdGroup;
                    r.CreateDate = DateTime.Now;
                    r.CreateBy = "System";
                    r.Order = ++maxOrder;
                });
                _context.AddRange(creates);

                _context.SaveChanges();
                transaction.Commit();
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

        public ResponseModel ActionSort(List<Entities.Models.ConfigGroupQuestion> configGroupQuestions)
        {
            var methodName = MethodBase.GetCurrentMethod().Name;
            try
            {
                _logger.LogInformation($"Start Function => {methodName}");

                _context.ConfigGroupQuestion.UpdateRange(configGroupQuestions);
                _context.SaveChanges();

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
