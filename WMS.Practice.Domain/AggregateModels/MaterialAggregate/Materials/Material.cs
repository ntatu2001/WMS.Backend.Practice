namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class Material : Entity, IAggregateModel
    {
        public string MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string MaterialClassId { get; private set; }
        public MaterialClass MaterialClass { get; private set; }
        public List<MaterialProperty> Properties { get; private set; }
        public List<MaterialLot> MaterialLots { get; private set; }
        public List<ReceiptLot> ReceiptLots { get; private set; }
        public List<IssueLot> IssueLots { get; private set; }
        public Material(string materialId, string materialName, string materialClassId)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialClassId = materialClassId;
            Properties = new List<MaterialProperty>();
        }
    }
}
