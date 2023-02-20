namespace TeamDevelopmentBackend.Model.DTO
{
    public class BuildingDTOModel
    {
        public Guid id { get; set; }
        public string title { get; set; }

        public BuildingDTOModel(BuildingDbModel building)
        {
            id = building.Id;
            title = building.Name;
        }
    }
}
