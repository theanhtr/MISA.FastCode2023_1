using AutoMapper;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.Repository.Answer;
using Misa.FastCode.Dl.Repository.Question;
using Misa.FastCode.Dl.unitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Answer
{
    public class AnswerService : CRUDBaseService<AnswerEntity, AnswerDto, AnswerUpdateDto, AnswerCreateDto>, IAnswerService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        public AnswerService(IAnswerRepository answerRepository, IUnitOfWork unitOfWork, IMapper mapper, IQuestionRepository questionRepository) : base(answerRepository, unitOfWork, mapper)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        protected override string GetAssetName()
        {
            return AssetName.Answer;
        }

        protected async override Task<List<ValidateError>> CreateValidateAsync(AnswerCreateDto answerCreateDto)
        {
            var listError = new List<ValidateError>();
            var question = await _questionRepository.GetAsync(answerCreateDto.question_id);
            if(question == null )
            {
                listError.Add(new ValidateError()
                {
                    FieldNameError = "QuestionId",
                    Message = string.Format(ErrorMessage.InvalidError, FieldName.Question),
                });
            }
            await _unitOfWork.CommitAsync();
            return listError;
        }

        public async Task<IEnumerable<AnswerDto>> GetListByQuestionIdAsync(Guid questionId)
        {
            var listError = new List<ValidateError>();
            var question = await _questionRepository.GetAsync(questionId);
            if (question == null)
            {
                listError.Add(new ValidateError()
                {
                    FieldNameError = "QuestionId",
                    Message = string.Format(ErrorMessage.InvalidError, FieldName.Question),
                });
            }
            if(listError.Count > 0)
            {
                throw new ValidateException()
                {
                    ErrorCode = ErrorCode.DataValidate,
                    Data = listError,
                    UserMessage = ErrorMessage.ValidateFilterError
                };
            }

            var listAnswer = await _answerRepository.GetListByQuestionIdAsync(questionId);
            var result = listAnswer.Select(entity => _mapper.Map<AnswerDto>(entity));
            return result;
        }
    }
}
