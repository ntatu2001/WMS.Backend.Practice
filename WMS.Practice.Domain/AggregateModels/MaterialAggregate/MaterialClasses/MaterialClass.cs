namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialClass : Entity, IAggregateModel
    {
        public string MaterialClassId { get; set; }
        public string MaterialClassName { get; set; }
        public List<Material> Materials { get; set; }
        public List<MaterialClassProperty> Properties { get; set; }
        public MaterialClass(string materialClassId, string materialClassName)
        {
            MaterialClassId = materialClassId;
            MaterialClassName = materialClassName;
            Properties = new List<MaterialClassProperty>();
        }
    }
}
