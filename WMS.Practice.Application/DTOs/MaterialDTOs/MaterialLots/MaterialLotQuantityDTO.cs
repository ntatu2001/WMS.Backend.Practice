namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialLots
{
    public class MaterialLotQuantityDTO
    {
        public string? LotNumber { get; set; }
        public double? AvailableQuantity { get; set; }
        public MaterialLotQuantityDTO()
        {
        }

        public MaterialLotQuantityDTO(string? lotNumber, double? availableQuantity)
        {
            LotNumber = lotNumber;
            AvailableQuantity = availableQuantity;
        }
    }
}
