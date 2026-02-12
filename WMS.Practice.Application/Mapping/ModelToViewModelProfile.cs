namespace WMS.Practice.Application.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            MapEmployeeViewModel();
            MapEmployeePropertyViewModel();
            MapCustomerViewModel();
            MapSupplierViewModel();


        }

        #region Person Aggregate Mapping
        public void MapEmployeeViewModel()
        {
            CreateMap<Employee, EmployeeDTO>().ForMember(s => s.EmployeePropertyDTOs, s => s.MapFrom(s => s.Properties));
        }

        public void MapEmployeePropertyViewModel()
        {
            CreateMap<EmployeeProperty, EmployeePropertyDTO>().ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapCustomerViewModel()
        {
            CreateMap<Customer, CustomerDTO>();
        }

        public void MapSupplierViewModel()
        {
            CreateMap<Supplier, SupplierDTO>();
        }

        #endregion
    }
}
