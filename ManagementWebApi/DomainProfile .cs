using AutoMapper;
using ManagementWebApi.Database;
using ManagementWebApi.DataModels;
using ManagementWebApi.DataModels.DetailedModels;
using ManagementWebApi.DataModels.ListModels;

namespace ManagementWebApi
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<DbDeviceType, IdNamePair>();
            CreateMap<DbDeviceActionType, IdNamePair>();

            CreateMap<DbEmployee, IdNamePair>();
            CreateMap<DbEmployee, EmployeeL>();
            CreateMap<DbEmployee, EmployeeDt>().ReverseMap();
            CreateMap<DbCert, CertDt>().ReverseMap();
            CreateMap<DbTaxId, TaxId>().ReverseMap();
            CreateMap<DbPassport, Passport>().ReverseMap();

            CreateMap<DbFile, File>();
            CreateMap<DbCert, CertL>().ForMember(x=>x.EmployeeName, x=>x.MapFrom(s=>s.NavEmployee.Name));
            CreateMap<DbDevice, DeviceL>().ForMember(x => x.EmployeeName, x => x.MapFrom(s => s.NavEmployee.Name));
        }
    }
}