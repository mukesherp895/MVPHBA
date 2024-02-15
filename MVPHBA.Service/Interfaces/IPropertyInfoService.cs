using MVPHBA.Common;
using MVPHBA.Model.DBModels;
using MVPHBA.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Service.Interfaces
{
    public interface IPropertyInfoService : IService<PropertyInfos>
    {
        Task<EnumData.DBAttempt> Add(PropertyInfoReqVM dto);
        Task<EnumData.DBAttempt> Edit(PropertyInfoReqVM dto);
        Task<EnumData.DBAttempt> Delete(long id);
        Task<List<PropertyInfoListVM>> PropertyInfoList(SearchVM dto);
    }
}
