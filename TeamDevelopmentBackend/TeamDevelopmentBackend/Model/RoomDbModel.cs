namespace TeamDevelopmentBackend.Model
{
    public class RoomDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BuildingDbModel Building { get; set; }

    }
}
