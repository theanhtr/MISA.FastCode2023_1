using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Misa.FastCode.Common.Const;
using Misa.FastCode.Common.Emum;
using Misa.FastCode.Common.Error;
using Misa.FastCode.Common.Exceptions;
using Misa.FastCode.Common.Resource;
using Misa.FastCode.Dl.Entity;
using Misa.FastCode.Dl.Repository;
using Misa.FastCode.Dl.Repository.Question;
using Misa.FastCode.Dl.Repository.Subject;
using Misa.FastCode.Dl.unitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.FastCode.Bl.Service.Question
{
    public class QuestionService : CRUDBaseService<QuestionEntity, QuestionDto, QuestionUpdateDto, QuestionCreateDto>, IQuestionService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IQuestionRepository _questionRepository;
        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork, IMapper mapper, ISubjectRepository subjectRepository) : base(questionRepository, unitOfWork, mapper)
        {
            _subjectRepository = subjectRepository;
            _questionRepository = questionRepository;
        }

        protected override string GetAssetName()
        {
            return AssetName.Question;
        }

        protected async override Task<List<ValidateError>> CreateValidateAsync(QuestionCreateDto questionCreateDto)
        {
            var listError = new List<ValidateError>();
            var subject = await _subjectRepository.GetAsync(questionCreateDto.subject_id);
            if(subject == null)
            {
                listError.Add(new ValidateError()
                {
                    FieldNameError = "SubjectId",
                    Message = string.Format(ErrorMessage.InvalidError, FieldName.Subject),
                });
            }
            await _unitOfWork.CommitAsync();
            return listError;
        }

        public async Task<IEnumerable<QuestionDto>> FilterAsync(int pageSize, int currentPage, Guid? subjectId, string? textSearch)
        {
            var listError = new List<ValidateError>();
            if (pageSize <= 0)
            {
                listError.Add(new ValidateError()
                {
                    FieldNameError = "pageSize",
                    Message = string.Format(ErrorMessage.PositiveNumberError, FieldName.PageSize),
                });
            }
            // current page lớn hơn 0
            if (currentPage <= 0)
            {
                listError.Add(new ValidateError()
                {
                    FieldNameError = "currentPage",
                    Message = string.Format(ErrorMessage.PositiveNumberError, FieldName.CurrentPage),
                });
            }
            // department tồn tại
            if (subjectId != null)
            {
                var subject = await _subjectRepository.GetAsync((Guid)subjectId);
                if (subject == null)
                {
                    listError.Add(new ValidateError()
                    {
                        FieldNameError = "department_id",
                        Message = string.Format(ErrorMessage.InvalidError, FieldName.DepartmentCode),
                    });
                }
            }
            
            // nếu có lỗi thì throw excception
            if (listError.Count > 0)
            {
                throw new ValidateException()
                {
                    ErrorCode = ErrorCode.DataValidate,
                    Data = listError,
                    UserMessage = ErrorMessage.ValidateFilterError
                };
            }

            var listQuestion = await _questionRepository.FilterAsync(pageSize, currentPage, subjectId, textSearch);

            var result = listQuestion.Select(entity => _mapper.Map<QuestionDto>(entity));

            return result;
        }
    }
}
