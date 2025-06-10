//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Repositories.Models;
//using Repositories.Repositories;
//using Services.Interface;

//namespace Services.Implement
//{
//    public class ClassService : IClassService
//    {
//        private readonly ClassRepository _repository;
//        public ClassService(ClassRepository repository)
//        {
//            _repository = repository;
//        }
//        public async Task<List<Classes>> GetAllClassesAsync()
//        {
//            return await _repository.GetAllClassesAsync();
//        }
//        public async Task<Classes> GetClassByIdAsync(Guid classId)
//        {
//            return await _repository.GetClassByIdAsync(classId);
//        }
//        public async Task<int> CreateClassAsync(Classes classes)
//        {
//            return await _repository.CreateClassAsync(classes);
//        }
//        public async Task<int> UpdateClassAsync(Classes classes)
//        {
//            return await _repository.UpdateClassAsync(classes);
//        }
//        public async Task<int> DeleteClassAsync(Guid classId)
//        {
//            return await _repository.DeleteClassAsync(classId);
//        }
//        public async Task<List<Classes>> SearchClassAsync(string classCode, string className)
//        {
//            return await _repository.SearchClassAsync(classCode, className);
//        }
//    }
//}
