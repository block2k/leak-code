using Application.Common.Exceptions;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using NextFap.Application.Common.Models;
using System.Linq.Expressions;

namespace Application.Services
{
    public class StudentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentReadDto> Create(StudentCreateDto student)
        {
            var studentModel = _mapper.Map<Student>(student);
            await _unitOfWork.StudentRepo.Insert(studentModel, true);

            return _mapper.Map<StudentReadDto>(studentModel);
        }
        public List<StudentReadDto> GetAll()
        {
            var studentListModel = _unitOfWork.StudentRepo.GetAll().ToList();
            return _mapper.Map<List<StudentReadDto>>(studentListModel);
        }
        public StudentReadDto? GetById(int id)
        {
            var studentModel = _unitOfWork.StudentRepo.Get(id);
            if (studentModel == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<StudentReadDto>(studentModel);
        }
        public async Task<int> Update(int id, StudentUpdateDto studentUpdateDto)
        {
            var studentModel = _unitOfWork.StudentRepo.Get(id);
            if (studentModel != null)
            {
                studentModel.FullName = studentUpdateDto.FullName;
                studentModel.Email = studentUpdateDto.Email;
                studentModel.IDCard = studentUpdateDto.IDCard;
                studentModel.Address = studentUpdateDto.Address;
                studentModel.DateOfBirth = studentUpdateDto.DateOfBirth;
                studentModel.DateOfIssue = studentUpdateDto.DateOfIssue;
                studentModel.PlaceOfIssue = studentUpdateDto.PlaceOfIssue;

                _unitOfWork.StudentRepo.Update(studentModel);
            }
            else
            {
                throw new NotFoundException();
            }

            return await _unitOfWork.SaveChange();
        }

        public async Task Delete(int id)
        {
            var studentModel = _unitOfWork.StudentRepo.Get(id);
            if (studentModel != null)
            {
                _unitOfWork.StudentRepo.Remove(studentModel);
                await _unitOfWork.SaveChange();
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public async Task<PaginatedList<Student>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var listAll = _unitOfWork.StudentRepo.GetAll();
            return await PaginatedList<Student>.CreateAsync(listAll, pageNumber, pageSize);
        }
        public List<Student> Find(Expression<Func<Student, bool>> where) => _unitOfWork.StudentRepo.Find(where).ToList();

    }
}
