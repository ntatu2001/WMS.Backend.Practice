namespace WMS.Practice.Application.Queries.EnumQueries
{
    public class GetAllUnitOfMeasureQueryHandler : IRequestHandler<GetAllUnitOfMeasureQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetAllUnitOfMeasureQuery request, CancellationToken cancellationToken)
        {
            var unitOfMeasureNames = Enum.GetNames(typeof(UnitOfMeasure)).ToList();
            if (unitOfMeasureNames is null || unitOfMeasureNames.Count == 0)
                throw new InvalidDataException("Could not retrieve Names of Unit of measure");

            await Task.CompletedTask;
            return unitOfMeasureNames;
        }
    }
}
