using AutoMapper;
using MVPHBA.Common;
using MVPHBA.DataAccess.Interfaces;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;
using MVPHBA.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Service
{
    public class PropertyInfoService : Service<PropertyInfos>, IPropertyInfoService
    {
        private readonly IRepository<PropertyInfos> _propertyInfosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISearchRepository _searchRepository;
        public PropertyInfoService(IRepository<PropertyInfos> propertyInfosRepository, IUnitOfWork unitOfWork, IMapper mapper, ISearchRepository searchRepository) : base(propertyInfosRepository)
        {
            _propertyInfosRepository = propertyInfosRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _searchRepository = searchRepository;
        }

        public async Task<EnumData.DBAttempt> Add(PropertyInfoReqVM dto)
        {
            PropertyInfos entity = _mapper.Map<PropertyInfos>(dto);
            
            if(dto.Image != null) 
            {
                entity.ImagePath = dto.Image.SaveToServer();
            }
            else
            {
                entity.ImagePath = "";
            }
            if(_propertyInfosRepository.Create(entity) == EnumData.DBAttempt.Success) 
            {
                return await _unitOfWork.Commit();
            }
            return EnumData.DBAttempt.Fail;
        }
        public async Task<EnumData.DBAttempt> Delete(long id)
        {
            var entity = _propertyInfosRepository.GetById(id);
            if(_propertyInfosRepository.DeleteObject(entity) == EnumData.DBAttempt.Success)
            {
                return await _unitOfWork.Commit();
            }
            return EnumData.DBAttempt.Fail;
        }

        public async Task<EnumData.DBAttempt> Edit(PropertyInfoReqVM dto)
        {
            var entity = _propertyInfosRepository.GetById(dto.Id);
            entity.Feature = dto.Feature;
            entity.Price = dto.Price;
            entity.Location = dto.Location;
            entity.Description = dto.Description;
            if (dto.Image != null)
            {
                entity.ImagePath = dto.Image.SaveToServer();
            }
            if (_propertyInfosRepository.Update(entity) == EnumData.DBAttempt.Success)
            {
                return await _unitOfWork.Commit();
            }
            return EnumData.DBAttempt.Fail;
        }
        public async Task<List<PropertyInfoListVM>> PropertyInfoList(SearchVM dto)
        {
            return await _searchRepository.PropertyInfoList(dto);
        }
    }
}
