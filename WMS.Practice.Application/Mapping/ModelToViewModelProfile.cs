using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;
using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;
using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Mapping
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            MapEmployeeViewModel();
            MapEmployeePropertyViewModel();
            MapEmployeeClassViewModel();
            MapEmployeeClassPropertyViewModel();
            MapCustomerViewModel();
            MapSupplierViewModel();

            MapLocationViewModel();
            MapLocationPropertyViewModel();
            MapWarehouseViewModel();
            MapWarehousePropertyViewModel();

            MapMaterialClassViewModel();
            MapMaterialClassPropertyViewModel();

            MapMaterialSubLotPropertyViewModel();

            MapMaterialViewModel();
            MapMaterialPropertyViewModel();

            MapMaterialLotViewModel();
            MapMaterialLotPropertyViewModel();

            MapReceiptSublotViewModel();
            MapReceiptLotViewModel();
            MapInventoryReceiptEntryViewModel();
            MapInventoryReceiptViewModel();

            MapIssueSublotViewModel();
            MapIssueLotViewModel();
            MapIssueLotPendingViewModel();
            MapInventoryIssueEntryViewModel();
            MapInventoryIssueViewModel();

            MapInventoryLogViewModel();

            MapMaterialLotAdjustmentViewModel();
            MapMaterialSubLotAdjustmentViewModel();

            MapMaterialByWarehouseViewModel();
        }

        private void MapMaterialByWarehouseViewModel()
        {
            CreateMap<Material, MaterialByWarehouseIdDTO>()
                .ForMember(s => s.MaterialId, s => s.MapFrom(s => s.MaterialId))
                .ForMember(s => s.MaterialName, s => s.MapFrom(s => s.MaterialName));
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

        public void MapEmployeeClassViewModel()
        {
            CreateMap<EmployeeClass, EmployeeClassDTO>()
                .ForMember(s => s.Properties, s => s.MapFrom(s => s.Properties));
        }

        public void MapEmployeeClassPropertyViewModel()
        {
            CreateMap<EmployeeClassProperty, EmployeeClassPropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapCustomerViewModel()
        {
            CreateMap<Customer, CustomerDTO>();
        }

        public void MapSupplierViewModel()
        {
            CreateMap<Supplier, SupplierDTO>();
        }

        public void MapLocationViewModel()
        {
            CreateMap<Location, LocationDTO>()
                .ForMember(s => s.LocationPropertyDTOs, s => s.MapFrom(s => s.Properties))
                .ForMember(s => s.MaterialSubLotDTOs, s => s.MapFrom(s => s.MaterialSubLots));
        }

        public void MapLocationPropertyViewModel()
        {
            CreateMap<LocationProperty, LocationPropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapWarehouseViewModel()
        {
            CreateMap<Warehouse, WarehouseDTO>()
                .ForMember(s => s.Locations, s => s.MapFrom(s => s.Locations))
                .ForMember(s => s.Properties, s => s.MapFrom(s => s.Properties));
        }

        public void MapWarehousePropertyViewModel()
        {
            CreateMap<WarehouseProperty, WarehousePropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapMaterialClassViewModel()
        {
            CreateMap<MaterialClass, MaterialClassDTO>()
                .ForMember(s => s.ClassName, s => s.MapFrom(s => s.MaterialClassName))
                .ForMember(s => s.Properties, s => s.MapFrom(s => s.Properties))
                .ForMember(s => s.Materials, s => s.MapFrom(s => s.Materials));
        }

        public void MapMaterialClassPropertyViewModel()
        {
            CreateMap<MaterialClassProperty, MaterialClassPropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapMaterialSubLotPropertyViewModel()
        {
            CreateMap<MaterialSubLot, MaterialSubLotDTO>()
                .ForMember(dest => dest.UnitOfMeasure,
                    opt => opt.MapFrom(src => src.UnitOfMeasure.ToString()))
                .ForMember(dest => dest.SubLotStatus,
                    opt => opt.MapFrom(src => src.SubLotStatus.ToString()));
        }

        public void MapMaterialViewModel()
        {
            CreateMap<Material, MaterialDTO>()
                .ForMember(s => s.Properties, s => s.MapFrom(s => s.Properties))
                .ForMember(s => s.MaterialClassName, s => s.MapFrom(s => s.MaterialClass != null ? s.MaterialClass.MaterialClassName : null));

        }

        public void MapMaterialPropertyViewModel()
        {
            CreateMap<MaterialProperty, MaterialPropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapMaterialLotViewModel()
        {
            CreateMap<MaterialLot, MaterialLotDTO>()
                .ForMember(s => s.SubLots, s => s.MapFrom(s => s.SubLots))
                .ForMember(s => s.Properties, s => s.MapFrom(s => s.Properties))
                .ForMember(s => s.LotStatus, s => s.MapFrom(s => s.LotStatus.ToString()))
                .ForMember(s => s.ExistingQuantity, s => s.MapFrom(s => s.ExistingQuantity));

        }

        public void MapMaterialLotPropertyViewModel()
        {
            CreateMap<MaterialLotProperty, MaterialLotPropertyDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()));
        }

        public void MapReceiptSublotViewModel()
        {
            CreateMap<ReceiptSubLot, ReceiptSubLotDTO>()
                .ForMember(s => s.UnitOfMeasure, s => s.MapFrom(s => s.UnitOfMeasure.ToString()))
                .ForMember(s => s.SubLotStatus, s => s.MapFrom(s => s.LotStatus.ToString()));
        }

        public void MapReceiptLotViewModel()
        {
            CreateMap<ReceiptLot, ReceiptLotDTO>()
                .ForMember(s => s.ReceiptSublots, s => s.MapFrom(s => s.ReceiptSubLots))
                .ForMember(s => s.ReceiptLotStatus, s => s.MapFrom(s => s.LotStatus.ToString()));
        }

        public void MapInventoryReceiptEntryViewModel()
        {
            CreateMap<InventoryReceiptEntry, InventoryReceiptEntryDTO>()
                .ForMember(s => s.ReceiptLot, s => s.MapFrom(s => s.ReceiptLot));
        }

        public void MapInventoryReceiptViewModel()
        {
            CreateMap<InventoryReceipt, InventoryReceiptDTO>()
                .ForMember(s => s.Entries, s => s.MapFrom(s => s.Entries))
                .ForMember(s => s.ReceiptStatus, s => s.MapFrom(s => s.ReceiptStatus.ToString()));
        }

        public void MapIssueSublotViewModel()
        {
            CreateMap<IssueSubLot, IssueSubLotDTO>()
                .ForMember(s => s.MaterialSublot, s => s.MapFrom(s => s.MaterialSubLot));
        }

        public void MapIssueLotViewModel()
        {
            CreateMap<IssueLot, IssueLotDTO>()
                .ForMember(s => s.IssueSublots, s => s.MapFrom(s => s.IssueSubLots))
                .ForMember(s => s.IssueLotStatus, s => s.MapFrom(s => s.LotStatus.ToString()));
        }

        public void MapIssueLotPendingViewModel()
        {
            CreateMap<IssueLot, IssueLotPendingDTO>()
                .ForMember(s => s.IssueSublots, s => s.MapFrom(s => s.IssueSubLots))
                .ForMember(s => s.IssueLotStatus, s => s.MapFrom(s => s.LotStatus.ToString()));
        }

        public void MapInventoryIssueEntryViewModel()
        {
            CreateMap<InventoryIssueEntry, InventoryIssueEntryDTO>()
                .ForMember(s => s.IssueLot, s => s.MapFrom(s => s.IssueLot));
        }

        public void MapInventoryIssueViewModel()
        {
            CreateMap<InventoryIssue, InventoryIssueDTO>()
                .ForMember(s => s.Entries, s => s.MapFrom(s => s.Entries))
                .ForMember(s => s.IssueStatus, s => s.MapFrom(s => s.IssueStatus.ToString()));
        }

        public void MapInventoryLogViewModel()
        {
            CreateMap<InventoryLog, InventoryLogDTO>()
                .ForMember(s => s.TransactionType, s => s.MapFrom(s => s.TransactionType.ToString()));

        }

        public void MapMaterialLotAdjustmentViewModel()
        {
            CreateMap<StockTake, StockTakeLotDTO>()
                .ForMember(s => s.StockTakeSubLots, s => s.MapFrom(s => s.SubLots))
                .ForMember(s => s.Reason, s => s.MapFrom(s => s.Reason.ToString()))
                .ForMember(s => s.Status, s => s.MapFrom(s => s.Status.ToString()))
                .ForMember(s => s.AdjustmentType, s => s.MapFrom(s => s.Type.ToString()));

        }

        public void MapMaterialSubLotAdjustmentViewModel()
        {
            CreateMap<StockTakeSubLot, StockTakeSubLotDTO>();
        }

        #endregion
    }
}
