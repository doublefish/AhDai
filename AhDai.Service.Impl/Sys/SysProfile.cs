using AutoMapper;
using AhDai.Entity.Sys;
using AhDai.Service.Sys.Models;

namespace AhDai.Service.Impl.Sys;

public class SysProfile : Profile
{
	public SysProfile()
	{
		CreateMap<EmployeeInput, Employee>()
			.ForMember(dest => dest.PostIds, opt => opt.MapFrom((src, dest) => Utils.ModelUtils.ToString(src.PostIds)));
		CreateMap<Employee, EmployeeOutput>()
			.ForMember(dest => dest.PostIds, opt => opt.MapFrom((src, dest) => Utils.ModelUtils.ToLongArray(src.PostIds)));

		CreateMap<DictInput, Dict>();
		CreateMap<Dict, DictOutput>();
		CreateMap<DictOutput, DictSimpleOutput>();

		CreateMap<FileInput, File>();
		CreateMap<File, FileOutput>().ForMember(dest => dest.FullPath, opt => opt.MapFrom((src, dest) => MyApp.GetFullPath(src.Path)));

		CreateMap<MenuInput, Menu>();
		CreateMap<Menu, MenuOutput>();

		CreateMap<OrganizationInput, Organization>();
		CreateMap<Organization, OrganizationOutput>();

		CreateMap<PostInput, Post>();
		CreateMap<Post, PostOutput>();

		CreateMap<RoleInput, Role>();
		CreateMap<Role, RoleOutput>();
		CreateMap<RoleOutput, RoleSimpleOutput>();

		CreateMap<TenantInput, Tenant>();
		CreateMap<Tenant, TenantOutput>();

		CreateMap<UserInput, User>();
		CreateMap<User, UserOutput>();

		CreateMap<UserOrgInput, UserOrg>();
		CreateMap<UserOrg, UserOrgOutput>();
	}
}
