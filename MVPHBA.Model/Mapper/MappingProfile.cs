using AutoMapper;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region VMToDB
            CreateMap<UserRegisterReqVM, Users>();
            CreateMap<PropertyInfoReqVM, PropertyInfos>();
            #endregion

            #region DBToVM
            CreateMap<PropertyInfos, PropertyDetailVM>();
            #endregion
        }
    }
}
